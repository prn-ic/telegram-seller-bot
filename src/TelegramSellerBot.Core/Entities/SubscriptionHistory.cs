using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class SubscriptionHistory : BaseEntity<Guid>
    {
        public Subscription? Subscription { get; set; }
        public DateTime CreatedAt { get; set; }
        public TelegramBot? Service { get; set; }
        public SubscriptionStatus? Status { get; set; }
        public decimal Cost { get; private set; }
        public SubscriptionStatuses StatusId { get; set; }

        public SubscriptionHistory(
            Subscription subscription,
            TelegramBot service,
            SubscriptionStatus status,
            decimal cost,
            SubscriptionStatuses statusId,
            DateTime? createdAt
        )
        {
            Subscription = subscription;
            CreatedAt = createdAt is null ? DateTime.UtcNow : (DateTime)createdAt;
            Service = service;
            Status = status;
            ExceptionExtension.ThrowIfLessThanZero(cost);
            Cost = cost;
            StatusId = statusId;
        }

        public void SetCost(decimal cost)
        {
            ExceptionExtension.ThrowIfLessThanZero(cost);
            Cost = cost;
        }
    }
}
