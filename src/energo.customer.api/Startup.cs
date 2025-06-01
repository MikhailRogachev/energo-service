using AutoMapper;
using energo.customer.api.Dto;
using energo.customer.api.Profiles;
using energo.customer.api.Validators;
using energo.domain.Models;
using energo.infrastructure.Interfaces;
using energo.infrastructure.Producer;
using energo.infrastructure.Settings;
using FluentValidation;

namespace energo.customer.api;

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

        // validators
        services.AddSingleton<IValidator<CustomerDto>, CustomerValidator>();

        // add services
        services.AddScoped<IProducerService<Customer>, ProducerService<Customer>>();

        // add automapper
        services.AddSingleton(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerProfile>();
        }).CreateMapper());

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
