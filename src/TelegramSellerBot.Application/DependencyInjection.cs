using Microsoft.Extensions.DependencyInjection;
using TelegramSellerBot.Application.Services;
using TelegramSellerBot.Core.Contracts;

namespace TelegramSellerBot.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
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
