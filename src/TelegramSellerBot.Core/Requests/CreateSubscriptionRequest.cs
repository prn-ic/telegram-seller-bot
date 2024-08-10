using System.ComponentModel.DataAnnotations;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Requests
{
    public class CreateSubscriptionRequest
    {
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public string? TelegramUserId { get; set; }
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public Guid TelegramServiceId { get; set; }
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public TelegramServiceDurations Duration { get; set; }
    }
}