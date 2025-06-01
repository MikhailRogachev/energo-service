namespace energo.domain.Contracts.Interfaces;

public interface IEventHandler<T>
{
    Task HandleAsync(T @event);
}
