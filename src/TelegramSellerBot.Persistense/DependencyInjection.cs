using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNpgsqlPersistense(
            this IServiceCollection services,
            IConfiguration configuration,
            string migrationAssemblyName
        )
        {
            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseSnakeCaseNamingConvention();
                o.UseNpgsql(
                    configuration.GetConnectionString("TelegramIdentity"),
                    b => b.MigrationsAssembly(migrationAssemblyName)
                );
            });

            return services;
        }
    }
}
