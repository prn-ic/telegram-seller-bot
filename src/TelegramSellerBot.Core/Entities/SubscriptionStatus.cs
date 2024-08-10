using System.ComponentModel.DataAnnotations;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class SubscriptionStatus : BaseEntity<SubscriptionStatuses>
    {
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public string? Name { get; set; }
    }
}
