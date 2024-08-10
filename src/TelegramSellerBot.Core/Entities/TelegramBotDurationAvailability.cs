using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class TelegramBotDurationAvailability : BaseEntity<int>
    {
        public TelegramBotDuration? Duration { get; set; }
        public TelegramBot? Service { get; set; }
        public decimal Cost { get; private set; }

        public TelegramServiceDurations DurationId { get; set; }

        public Guid ServiceId { get; set; }

        public TelegramBotDurationAvailability(
            Guid serviceId,
            TelegramServiceDurations durationId,
            decimal cost
        )
        {
            DurationId = durationId;
            ServiceId = serviceId;
            Cost = cost;
        }

        public TelegramBotDurationAvailability(
            TelegramBotDuration? duration,
            TelegramBot? service,
            decimal cost,
            TelegramServiceDurations durationId,
            Guid serviceId
        )
        {
            Duration = duration;
            Service = service;
            ExceptionExtension.ThrowIfLessThanZero(cost);
            Cost = cost;
            DurationId = durationId;
            ServiceId = serviceId;
        }

        public void SetCost(decimal cost)
        {
            ExceptionExtension.ThrowIfLessThanZero(cost);
            Cost = cost;
        }
    }
}
