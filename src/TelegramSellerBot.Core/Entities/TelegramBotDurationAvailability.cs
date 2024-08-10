using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class TelegramBotDurationAvailability : BaseEntity<int>
    {
        public TelegramBotDuration? Duration { get; set; }
        public TelegramBot? Service { get; set; }
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public decimal Cost { get; set; }

        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public TelegramServiceDurations DurationId { get; set; }

        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public Guid ServiceId { get; set; }
    }
}
