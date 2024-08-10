using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class Subscription : BaseEntity<Guid>
    {
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public string? TelegramUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public TelegramBot? Service { get; set; }
        public TelegramBotDuration? Duration { get; set; }
        public SubscriptionStatus? Status { get; set; }

        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public SubscriptionStatuses StatusId { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public TelegramServiceDurations DurationId { get; set; }

        public DateTime GetExpirationDate()
        {
            if (ModifiedAt is null)
                ModifiedAt = CreatedAt;

            return ModifiedAt.Value.AddHours((int)DurationId);
        }

        public int GetRemainingDays() => (GetExpirationDate() - CreatedAt).Days;
    }
}
