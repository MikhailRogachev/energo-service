using energo.domain.Events.CustomerEvents;
using energo.domain.Models;
using ruby_testhelper.Attributes;

namespace energo.data.tests.Events;

public class CustomerEventsTests
{
    [Theory, AutoMock]
    public void AddCustomerEvent_Create_Test(
        Customer customer
        )
    {
        var instance = Activator.CreateInstance(typeof(AddCustomerEvent), new[] { customer }) as AddCustomerEvent;

        Assert.Equal(customer.Id, instance!.Customer!.Id);
        Assert.Equal(customer.Name, instance!.Customer!.Name);
    }

}
