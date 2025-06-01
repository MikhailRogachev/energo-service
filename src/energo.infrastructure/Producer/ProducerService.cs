using Confluent.Kafka;
using energo.infrastructure.Interfaces;
using energo.infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace energo.infrastructure.Producer;

public class ProducerService : IProducerService
{
    private readonly ILogger<ProducerService> _logger;
    private readonly IProducer<string, string> _producer;

    public ProducerService(
        ILogger<ProducerService> logger,
        IOptions<BrokerSettings> options
        )
    {
        var config = new ProducerConfig { BootstrapServers = options.Value.BootstrapServers };

        _logger = logger;
        _producer = new ProducerBuilder<string, string>(config)
            .Build();
    }

    public async Task ProduceAsync(string topic, string message)
    {
        _logger.LogDebug("The message is sending by {name}, topic {topic}", nameof(ProduceAsync), topic);

        var msg = new Message<string, string> { Value = message, Key = "Key" };
        await _producer.ProduceAsync(topic, msg);
    }

    public async Task SendAsync(string topic, object obj)
    {
        var msg = new Message<string, string> { Value = obj.ToString(), Key = obj.GetType().Name };
        await _producer.ProduceAsync(topic, msg);
    }
}
