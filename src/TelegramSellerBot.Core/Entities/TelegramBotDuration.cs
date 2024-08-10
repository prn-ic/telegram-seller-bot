using System.ComponentModel.DataAnnotations;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class TelegramBotDuration : BaseEntity<TelegramServiceDurations>
    {
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public string? Name { get; private set; }
    }
}