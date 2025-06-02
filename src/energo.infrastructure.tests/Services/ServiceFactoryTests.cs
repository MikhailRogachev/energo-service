using energo.domain.Events.CustomerEvents;
using energo.infrastructure.EventHandlers.CustomerHandlers;
using Moq;
using ruby_outbox_infrastructure.Services;

namespace energo.infrastructure.tests.Services;

public class ServiceFactoryTests
{

    [Theory, MemberData(nameof(GetNameToResolve))]
    public void GetHandlerTypeByEventName(string eventName, Type eventHandler)
    {
        var serviceFactory = new ServiceFactory(new Mock<IServiceProvider>().Object);
        var response = serviceFactory.TryGetEventType(eventName);

        Assert.Equal(eventHandler, response);
    }

    [Theory, MemberData(nameof(GetTypesToResolve))]
    public void GetHandlerTypeByEvent(Type eventType, Type eventHandler)
    {
        var serviceFactory = new ServiceFactory(new Mock<IServiceProvider>().Object);
        var response = serviceFactory.TryResolveEventHandler(eventType);

        Assert.Equal(eventHandler, response);
    }

    public static TheoryData<string, Type> GetNameToResolve()
    {
        return new TheoryData<string, Type>
        {
            { "AddCustomerEvent", typeof(AddCustomerEvent) }
        };
    }

    public static TheoryData<Type, Type> GetTypesToResolve()
    {
        return new TheoryData<Type, Type>
        {
            { typeof(AddCustomerEvent), typeof(CustomerHandler) }
        };
    }
}
