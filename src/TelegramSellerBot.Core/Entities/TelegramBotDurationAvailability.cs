using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class TelegramBotDurationAvailability : BaseEntity<int>
    {
        public TelegramBotDuration? Duration { get; set; }
        public TelegramBot? TelegramBot { get; set; }
        public decimal Cost { get; private set; }
        public Guid TelegramBotId { get; set; }
        public TelegramServiceDurations DurationId { get; set; }

        public TelegramBotDurationAvailability() { }

        public TelegramBotDurationAvailability(
            TelegramBot service,
            TelegramServiceDurations durationId,
            decimal cost
        )
        {
            TelegramBot = service;
            DurationId = durationId;
            Cost = cost;
        }

        public TelegramBotDurationAvailability(
            Guid serviceId,
            TelegramServiceDurations durationId,
            decimal cost
        )
        {
            TelegramBotId = serviceId;
            DurationId = durationId;
            Cost = cost;
        }

        public TelegramBotDurationAvailability(
            TelegramBotDuration? duration,
            TelegramBot? service,
            decimal cost,
            TelegramServiceDurations durationId
        )
        {
            Duration = duration;
            TelegramBot = service;
            ExceptionExtension.ThrowIfLessThanZero(cost);
            Cost = cost;
            DurationId = durationId;
        }

        public void SetCost(decimal cost)
        {
            ExceptionExtension.ThrowIfLessThanZero(cost);
            Cost = cost;
        }
    }
}
