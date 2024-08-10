using System.Text.Json.Serialization;
using TelegramSellerBot.Core.Common;

namespace TelegramSellerBot.Core.Entities
{
    public class TelegramBotDurationAvailability : BaseEntity<int>
    {
        public TelegramBotDuration? Duration { get; set; }
        public TelegramBot? Service { get; set; }
        public decimal Cost { get; set; }

        [JsonIgnore]
        public TelegramServiceDurations DurationId { get; set; }

        [JsonIgnore]
        public Guid ServiceId { get; set; }
    }
}
