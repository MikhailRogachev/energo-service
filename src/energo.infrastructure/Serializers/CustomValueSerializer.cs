using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace energo.infrastructure.Serializers;

public class CustomValueSerializer<T> : ISerializer<T> where T : class
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, typeof(T)));
    }
}
