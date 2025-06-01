namespace energo.infrastructure.Interfaces;

public interface IProducerService<T>
{
    Task ProduceAsync(T value, string topic, string trancactionType);
}
