using Confluent.Kafka;
using energo.infrastructure.Interfaces;
using energo.infrastructure.Serializers;
using energo.infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace energo.infrastructure.Producer;

public class Producer<T>(
    IOptions<BrokerSettings> options
    ) : IProducer<T> where T : class
{
    private ProducerConfig GetConfig => new ProducerConfig { BootstrapServers = options.Value.BootstrapServers };

    public async Task ProduceAsync(T value, string topic)
    {
        using (var producer = new ProducerBuilder<string, T>(GetConfig)
            .SetValueSerializer(new CustomValueSerializer<T>())
            .Build())
        {
            var message = new Message<string, T>
            {
                Value = value,
                Key = typeof(T).Name
            };
            await producer.ProduceAsync(topic, message);
        }
    }
}
