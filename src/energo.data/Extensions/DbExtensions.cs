using energo.data.Persistency;
using energo.domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace energo.data.Extensions;

public static class DbExtensions
{
    public static string GetConnection(this DbSettings dbSettings)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = dbSettings.Host,
            Port = dbSettings.Port,
            Username = dbSettings.Username,
            Password = dbSettings.Password,
            Database = dbSettings.Database
        };

        return builder.ToString();
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfig = new DbSettings();
        configuration.GetSection(nameof(DbSettings)).Bind(dbConfig);


        var connectionString = dbConfig!.GetConnection();

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
        });

        return services;
    }

    public static async Task MigrateDatabase(IHost host, ILogger logger)
    {
        await using var scope = host.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;

        try
        {
            logger.LogInformation("Migrating DB");

            var context = services.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            logger.LogInformation("Migration is done");

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred creating the DB.");
            throw;
        }
    }
}
