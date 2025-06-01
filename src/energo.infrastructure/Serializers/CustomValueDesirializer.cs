using Confluent.Kafka;
using System.Text.Json;

namespace energo.infrastructure.Serializers;

public class CustomValueDesirializer<T> : IDeserializer<T> where T : class
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<T>(data)!;
    }
}
