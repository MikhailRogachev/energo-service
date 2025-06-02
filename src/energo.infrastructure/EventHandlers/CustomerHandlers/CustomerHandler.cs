using energo.domain.Contracts.Interfaces;
using energo.domain.Events.CustomerEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace energo.infrastructure.EventHandlers.CustomerHandlers;

public class CustomerHandler : IEventHandler<AddCustomerEvent>
{
    private readonly ILogger<CustomerHandler> _logger;
    private readonly ICustomerRepository _repository;


    [ActivatorUtilitiesConstructor]
    public CustomerHandler(IServiceProvider serviceProvider)
    {
        _repository = serviceProvider.GetRequiredService<ICustomerRepository>();
        _logger = serviceProvider.GetRequiredService<ILogger<CustomerHandler>>();
    }

    public async Task HandleAsync(AddCustomerEvent @event)
    {
        _logger.LogInformation("Start Event {event}", @event.Name);
    }
}
