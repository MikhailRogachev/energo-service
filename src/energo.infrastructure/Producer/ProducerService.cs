using Confluent.Kafka;
using energo.infrastructure.Interfaces;
using energo.infrastructure.Serializers;
using energo.infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace energo.infrastructure.Producer;

public class ProducerService<T>(
    IOptions<BrokerSettings> options
    ) : IProducerService<T> where T : class
{
    private ProducerConfig GetConfig => new ProducerConfig { BootstrapServers = options.Value.BootstrapServers };

    public async Task ProduceAsync(T value, string topic, string transactionType)
    {
        using (var producer = new ProducerBuilder<string, T>(GetConfig)
            .SetValueSerializer(new CustomValueSerializer<T>())
            .Build())
        {
            var message = new Message<string, T>
            {
                Value = value,
                Key = transactionType
            };
            await producer.ProduceAsync(topic, message);
        }
    }
}
