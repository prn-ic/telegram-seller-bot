using System.ComponentModel.DataAnnotations;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class TelegramBot : BaseEntity<Guid>
    {
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        [MinLength(3, ErrorMessageResourceType = typeof(InvalidRequestException))]
        [MaxLength(100, ErrorMessageResourceType = typeof(InvalidRequestException))]
        public string? Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        [MaxLength(300, ErrorMessageResourceType = typeof(InvalidRequestException))]
        public string? Description { get; set; } = "No Description provided";
        [Required(ErrorMessageResourceType = typeof(InvalidRequestException))]
        public string? TelegramBotLink { get; set; }
        public ICollection<TelegramBotDurationAvailability> Availabilities { get; set; } 
            = new List<TelegramBotDurationAvailability>();
    }
}