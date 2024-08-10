using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class SubscriptionHistory : BaseEntity<Guid>
    {
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public Subscription? Subscription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public TelegramBot? Service { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public SubscriptionStatus? Status { get; set; }

        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public decimal Cost { get; set; }

        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public SubscriptionStatuses StatusId { get; set; }
    }
}