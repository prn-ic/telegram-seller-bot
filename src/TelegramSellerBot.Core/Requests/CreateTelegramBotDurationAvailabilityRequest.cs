using System.ComponentModel.DataAnnotations;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Requests
{
    public class CreateTelegramBotDurationAvailabilityRequest
    {
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public TelegramServiceDurations Duration { get; set; }
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public Guid ServiceId { get; set; }
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public decimal Cost { get; set; }
    }
}
