using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using TelegramSellerBot.Core.Repositories;
using TelegramSellerBot.Persistense.Data;
using TelegramSellerBot.Persistense.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyInjection
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

            services.AddScoped<ISubscriptionHistoryRepository, SubscriptionHistoryRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ITelegramBotDurationAvailabilityRepository, TelegramBotDurationAvailabilityRepository>();
            services.AddScoped<ITelegramBotRepository, TelegramBotRepository>();

            return services;
        }
    }
}
