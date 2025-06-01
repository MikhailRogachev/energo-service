using energo.data.Extensions;
using inventory_consumer.Services;

namespace inventory_consumer;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // add database
        services.AddDatabase(_configuration);



        // add logging
        services.AddLogging();

        // add services
        services.AddHostedService<ConsumerService>();
    }
}
