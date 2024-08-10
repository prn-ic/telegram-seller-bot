using TelegramSellerBot.Core.Common;

namespace TelegramSellerBot.Core.Dtos
{
    public class SubscriptionStatusDto
    {
        public SubscriptionStatuses Id { get; set; }
        public string? Name { get; set; }
    }
}
