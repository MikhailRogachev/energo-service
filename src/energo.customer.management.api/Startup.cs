using energo.data.Repository;
using energo.domain.Contracts.Interfaces;
using energo.domain.Models;
using energo.infrastructure.Consumer;
using energo.infrastructure.Interfaces;
using energo.infrastructure.Settings;
using ruby_outbox_infrastructure.Services;

namespace energo.customer.management.api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // options
        services.AddOptions<BrokerSettings>()
            .Bind(_configuration.GetSection(nameof(BrokerSettings)));

        // services
        services.AddSingleton<IServiceFactory, ServiceFactory>();
        services.AddSingleton<ICustomerRepository, CustomerRepository>();

        // run service
        services.AddHostedService<ConsumerService<Customer>>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
