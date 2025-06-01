namespace energo.infrastructure.Interfaces;

public interface IProducer<T>
{
    Task ProduceAsync(T value, string topic);
}
