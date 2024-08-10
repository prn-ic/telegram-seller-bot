using System.ComponentModel.DataAnnotations;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Requests
{
    public class UpdateSubscriptionRequest
    {
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public Guid SubscriptionId { get; set; }
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public TelegramServiceDurations Duration { get; set; }
    }
}