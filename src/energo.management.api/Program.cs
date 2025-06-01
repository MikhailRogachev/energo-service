using energo.domain.Models;
using energo.infrastructure.Consumer;
using energo.infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// get options
builder.Services.AddOptions<ConsumingBrokerSettings>()
           .Bind(builder.Configuration.GetSection(nameof(ConsumingBrokerSettings)));

// run service
builder.Services.AddHostedService<ConsumerService<Customer>>();

var app = builder.Build();
app.Run();
