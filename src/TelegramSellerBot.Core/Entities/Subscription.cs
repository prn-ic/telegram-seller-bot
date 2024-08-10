using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class Subscription : BaseEntity<Guid>
    {
        public string? TelegramUserId { get; private set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public TelegramBot? Service { get; set; }
        public TelegramBotDuration? Duration { get; set; }
        public SubscriptionStatus? Status { get; set; }

        public SubscriptionStatuses StatusId { get; set; }

        public TelegramServiceDurations DurationId { get; set; }

        public Subscription(string? telegramUserId, TelegramBot service, TelegramServiceDurations durationId)
        {
            ExceptionExtension.ThrowIfStringRangeIsInvalid(telegramUserId, 1);
            TelegramUserId = telegramUserId;
            Service = service;
            ExceptionExtension.ThrowIsValueNull(durationId);
            DurationId = durationId;
            ModifiedAt = CreatedAt;
            StatusId = SubscriptionStatuses.Active;
        }

        public Subscription(
            string? telegramUserId,
            DateTime createdAt,
            DateTime? modifiedAt,
            TelegramBot? service,
            TelegramBotDuration? duration,
            SubscriptionStatus? status,
            SubscriptionStatuses statusId,
            TelegramServiceDurations durationId
        )
        {
            ExceptionExtension.ThrowIfStringRangeIsInvalid(telegramUserId, 1);
            TelegramUserId = telegramUserId;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            Service = service;
            Duration = duration;
            Status = status;
            ExceptionExtension.ThrowIsValueNull(statusId);
            StatusId = statusId;
            ExceptionExtension.ThrowIsValueNull(durationId);
            DurationId = durationId;
        }

        public DateTime GetExpirationDate()
        {
            if (ModifiedAt is null)
                ModifiedAt = CreatedAt;

            return ModifiedAt.Value.AddHours((int)DurationId);
        }

        public void SetTelegramUserId(string? value)
        {
            ExceptionExtension.ThrowIfStringRangeIsInvalid(value, 1);
            TelegramUserId = value;
        }

        public int GetRemainingDays() => (GetExpirationDate() - CreatedAt).Days;
    }
}
