using Confluent.Kafka;

namespace inventory_producer.Services;

public class __ProducerService
{
    private readonly IConfiguration _configuration;
    private readonly IProducer<Null, string> _producer;

    public __ProducerService(IConfiguration configuration)
    {
        _configuration = configuration;

        var config = new ProducerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceAsync(string topic, string message)
    {
        var kafkamessage = new Message<Null, string> { Value = message };
        await _producer.ProduceAsync(topic, kafkamessage);
    }
}
