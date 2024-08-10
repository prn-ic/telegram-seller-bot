using TelegramSellerBot.Core.Common;

namespace TelegramSellerBot.Core.Requests
{
    public class UpdateSubscriptionRequest
    {
        public Guid SubscriptionId { get; set; }
        public TelegramServiceDurations Duration { get; set; }
    }
}