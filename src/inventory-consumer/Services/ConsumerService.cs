
using Confluent.Kafka;

namespace inventory_consumer.Services;

public class ConsumerService : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<ConsumerService> _logger;

    public ConsumerService(
        IConfiguration configuration,
        ILogger<ConsumerService> logger)
    {
        _logger = logger;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = "InventoryConsumerGruop",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("InventoryUpdate");

        _logger.LogInformation("The consumer is started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Request.");
            ProcessKafkaMessage(stoppingToken);
            Task.Delay(TimeSpan.FromMilliseconds(500), stoppingToken);
        }

        _consumer.Close();
    }

    public void ProcessKafkaMessage(CancellationToken stoppingToken)
    {
        try
        {
            var consumeResult = _consumer.Consume(stoppingToken);
            var message = consumeResult.Message.Value;
            var key = consumeResult.Message.Key;

            _logger.LogInformation("Received inventory update: {msg}, key: {key}", message, key);

        }
        catch (Exception ex)
        {
            _logger.LogError("Error processing Kafka message: {msg}", ex.Message);
        }
    }
}
