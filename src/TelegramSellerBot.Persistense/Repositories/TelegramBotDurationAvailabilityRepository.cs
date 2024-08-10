using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Repositories;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Repositories
{
    public class TelegramBotDurationAvailabilityRepository : GeneralRepository<TelegramBotDurationAvailability, int>, ITelegramBotDurationAvailablilityRepository
    {
        public TelegramBotDurationAvailabilityRepository(AppDbContext context) : base(context)
        {
        }
    }
}