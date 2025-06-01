using energo.domain.Contracts.Constants;
using energo.domain.Contracts.Interfaces;
using energo.domain.Models;

namespace energo.data.Events.CustomerEvents;

public class AddCustomerEvent : IEvent
{
    public Customer? Customer { get; set; }
    public string Name => CustomerTransactionType.AddCustomer;

    public AddCustomerEvent(Customer customer)
    {
        Customer = customer;
    }
}
