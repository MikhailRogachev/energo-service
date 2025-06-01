using energo.data.Events.CustomerEvents;
using energo.domain.Contracts.Interfaces;
using Microsoft.Extensions.Logging;

namespace energo.infrastructure.EventHandlers.CustomerHandlers;

public class CustomerHandler(
    ICustomerRepository repository,
    ILogger<CustomerHandler> logger
    ) : IEventHandler<AddCustomerEvent>
{
    public async Task HandleAsync(AddCustomerEvent @event)
    {
        logger.LogInformation("Start Event {event}", @event.Name);

    }
}
