using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Repositories;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Repositories
{
    public class SubscriptionHistoryRepository
        : GeneralRepository<SubscriptionHistory, Guid>,
            ISubscriptionHistoryRepository
    {
        public SubscriptionHistoryRepository(AppDbContext context)
            : base(context) { }
    }
}
