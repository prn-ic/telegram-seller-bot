using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class SubscriptionHistory : BaseEntity<Guid>
    {
        public Subscription? Subscription { get; set; }
        public DateTime CreatedAt { get; set; }
        public TelegramBot? TelegramBot { get; set; }
        public SubscriptionStatus? Status { get; set; }
        public decimal Cost { get; private set; }
        public SubscriptionStatuses StatusId { get; set; }

        public SubscriptionHistory() { }
        public SubscriptionHistory(
            Subscription subscription,
            TelegramBot service,
            SubscriptionStatus status,
            decimal cost,
            SubscriptionStatuses statusId,
            DateTime? createdAt
        )
        {
            Id = Guid.NewGuid();
            Subscription = subscription;
            CreatedAt = createdAt is null ? DateTime.UtcNow : (DateTime)createdAt;
            TelegramBot = service;
            Status = status;
            ExceptionExtension.ThrowIfLessThanZero(cost);
            Cost = cost;
            StatusId = statusId;
        }
        public SubscriptionHistory(
            Subscription subscription,
            TelegramBot service,
            decimal cost,
            SubscriptionStatuses statusId,
            DateTime? createdAt
        )
        {
            Subscription = subscription;
            CreatedAt = createdAt is null ? DateTime.UtcNow : (DateTime)createdAt;
            TelegramBot = service;
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
