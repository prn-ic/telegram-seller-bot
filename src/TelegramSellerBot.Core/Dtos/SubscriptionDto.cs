using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Exceptions;

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

        public DateTime GetExpirationDate()
        {
            if (ModifiedAt is null)
                ModifiedAt = CreatedAt;

            return ModifiedAt.Value.AddHours((int)Duration!.Id);
        }
        public int GetRemainingDays() => (GetExpirationDate() - CreatedAt).Days;
    }
}
