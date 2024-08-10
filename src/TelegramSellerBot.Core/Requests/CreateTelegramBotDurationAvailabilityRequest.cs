using TelegramSellerBot.Core.Common;

namespace TelegramSellerBot.Core.Requests
{
    public class CreateTelegramBotDurationAvailabilityRequest
    {
        public TelegramServiceDurations Duration { get; set; }
        public Guid ServiceId { get; set; }
        public decimal Cost { get; set; }
    }
}
