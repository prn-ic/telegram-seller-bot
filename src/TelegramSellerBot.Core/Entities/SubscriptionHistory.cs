using System.Text.Json.Serialization;
using TelegramSellerBot.Core.Common;

namespace TelegramSellerBot.Core.Entities
{
    public class SubscriptionHistory : BaseEntity<Guid>
    {
        public Subscription? Subscription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public TelegramBot? Service { get; set; }
        public SubscriptionStatus? Status { get; set; }
        public decimal Cost { get; set; }

        [JsonIgnore]
        public SubscriptionStatuses StatusId { get; set; }
    }
}