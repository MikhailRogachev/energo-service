using Confluent.Kafka;
using energo.infrastructure.Interfaces;
using energo.infrastructure.Serializers;
using energo.infrastructure.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace energo.infrastructure.Consumer;


public class ConsumerService<T> : BackgroundService where T : class
{
    private readonly BrokerSettings _brokerSettings;
    private readonly IServiceFactory _serviceFactory;
    private readonly ILogger<ConsumerService<T>> _logger;

    public ConsumerService(
        IOptions<BrokerSettings> options,
        IServiceFactory serviceFactory,
        ILogger<ConsumerService<T>> logger
        )
    {
        _brokerSettings = options.Value;
        _serviceFactory = serviceFactory;
        _logger = logger;
    }

    private string Topic => _brokerSettings.Topic;

    private ConsumerConfig GetConfig => new ConsumerConfig
    {
        BootstrapServers = _brokerSettings.BootstrapServers,
        GroupId = _brokerSettings.GroupId,
        AutoOffsetReset = AutoOffsetReset.Earliest
    };

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        using (var consumer = new ConsumerBuilder<string, T>(GetConfig)
           .SetValueDeserializer(new CustomValueDesirializer<T>())
           .Build())
        {
            _logger.LogInformation("Start Consuming {topic}", Topic);

            consumer.Subscribe(Topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Request for topic {topic}.", Topic);

                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    RunProcess(consumeResult.Message.Value, consumeResult.Message.Key);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error processing Kafka message: {msg}", ex.Message);
                }

                await Task.Delay(TimeSpan.FromMilliseconds(_brokerSettings.ConsumPeriodMSec), stoppingToken);
            }

            consumer.Close();
        }
    }

    private void RunProcess(T message, string transactionType)
    {
        _logger.LogInformation("Received message with key: {key}", transactionType);

        try
        {
            var handlerItem = _serviceFactory.GetServiceInstance(transactionType);

            if (handlerItem.Instance == null)
            {
                _logger.LogError("The service for the event {en} was not identified", transactionType);
                return;
            }

            _logger.LogDebug("The eventHandler {eh} has been identified", handlerItem.Instance.GetType());

            var @event = Activator.CreateInstance(handlerItem.InstanceType!, new[] { message });
            var method = handlerItem.Instance.GetType().GetMethod("HandleAsync", new Type[] { handlerItem.InstanceType! });

            method!.Invoke(handlerItem.Instance, new object[] { @event! });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
    }
}