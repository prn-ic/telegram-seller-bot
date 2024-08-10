using System.ComponentModel.DataAnnotations;

namespace TelegramSellerBot.Core.Entities
{
    public class TelegramBot : BaseEntity<Guid>
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? TelegramBotLink { get; set; }
        public ICollection<TelegramBotDurationAvailability> Availabilities { get; set; } 
            = new List<TelegramBotDurationAvailability>();
    }
}