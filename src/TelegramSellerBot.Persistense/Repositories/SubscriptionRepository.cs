using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Repositories;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Repositories
{
    public class SubscriptionRepository
        : GeneralRepository<Subscription, Guid>,
            ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext context)
            : base(context) { }
    }
}
