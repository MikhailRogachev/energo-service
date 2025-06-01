namespace energo.infrastructure.Interfaces;

public interface IProducerService
{
    Task SendAsync(string topic, object obj);
    Task ProduceAsync(string topic, string message);
}
