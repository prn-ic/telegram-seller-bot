using TelegramSellerBot.Core.Common;

namespace TelegramSellerBot.Core.Entities
{
    public class SubscriptionStatus : BaseEntity<SubscriptionStatuses>
    {
        public string? Name { get; set; }
    }
}
