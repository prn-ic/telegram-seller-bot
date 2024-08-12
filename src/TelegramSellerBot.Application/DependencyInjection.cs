using Microsoft.Extensions.DependencyInjection;
using TelegramSellerBot.Application.MapperProfiles;
using TelegramSellerBot.Application.Services;
using TelegramSellerBot.Core.Contracts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppMappingProfile));

            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<
                ITelegramBotDurationAvailabilityService,
                TelegramBotDurationAvailabilityService
            >();
            services.AddScoped<ITelegramBotService, TelegramBotService>();

            return services;
        }
    }
}
