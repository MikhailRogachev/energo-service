using Confluent.Kafka;
using energo.infrastructure.Serializers;
using energo.infrastructure.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace energo.infrastructure.Consumer;


public class Consumer<T> : BackgroundService where T : class
{
    private readonly BrokerSettings _settings;
    private readonly ILogger<Consumer<T>> _logger;

    public Consumer(
        IOptions<ConsumingBrokerSettings> options,
        ILogger<Consumer<T>> logger
        )
    {
        var settings = options.Value.Settings?.FirstOrDefault(p => p.Topic == typeof(T).Name);
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _logger = logger;
    }

    private string Topic => _settings.Topic;

    private ConsumerConfig GetConfig => new ConsumerConfig
    {
        BootstrapServers = _settings.BootstrapServers,
        GroupId = _settings.GroupId,
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
                    var message = consumeResult.Message.Value;
                    var key = consumeResult.Message.Key;

                    _logger.LogInformation("Received inventory update: {msg}, key: {key}", message.ToString(), key);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error processing Kafka message: {msg}", ex.Message);
                }

                await Task.Delay(TimeSpan.FromMilliseconds(_settings.ConsumPeriodMSec), stoppingToken);
            }

            consumer.Close();
        }
    }
}