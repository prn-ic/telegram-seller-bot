using AutoMapper;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Application.MapperProfiles
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Subscription, SubscriptionDto>().ReverseMap();
            CreateMap<SubscriptionHistory, SubscriptionHistoryDto>().ReverseMap();
            CreateMap<SubscriptionStatus, SubscriptionStatusDto>().ReverseMap();
            CreateMap<TelegramBot, TelegramBotDto>().ReverseMap();
            CreateMap<TelegramBotDuration, TelegramBotDurationDto>().ReverseMap();
            CreateMap<TelegramBotDurationAvailability, TelegramBotDurationAvailabilityDto>()
                .ReverseMap();
        }
    }
}
