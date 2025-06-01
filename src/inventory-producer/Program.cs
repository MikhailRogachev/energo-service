using energo.infrastructure.Interfaces;
using energo.infrastructure.Producer;
using energo.infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

// add options
builder.Services.AddOptions<BrokerSettings>()
            .Bind(builder.Configuration.GetSection(nameof(BrokerSettings)));

// add services

builder.Services.AddSingleton<IProducerService, ProducerService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
