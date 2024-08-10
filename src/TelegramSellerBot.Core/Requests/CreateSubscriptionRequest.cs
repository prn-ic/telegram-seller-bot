using TelegramSellerBot.Core.Common;

namespace TelegramSellerBot.Core.Requests
{
    public class CreateSubscriptionRequest
    {
        public string? TelegramUserId { get; set; }
        public Guid TelegramServiceId { get; set; }
        public TelegramServiceDurations Duration { get; set; }
    }
}