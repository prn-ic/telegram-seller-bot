using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Core.Dtos
{
    public class SubscriptionHistoryDto
    {
        public Guid Id { get; set; }
        public SubscriptionDto? Subscription { get; set; }
        public TelegramBotDto? Service { get; set; }
        public SubscriptionStatusDto? Status { get; set; }
        public decimal Cost { get; set; }
    }
}
