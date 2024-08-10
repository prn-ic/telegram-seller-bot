using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Core.Dtos
{
    public class SubscriptionDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }    
        public DateTime? ModifiedAt { get; set; }    
        public TelegramBot? Service { get; set; }
        public TelegramBotDuration? Duration { get; set; }
        public SubscriptionStatus? Status { get; set; }
    }
}