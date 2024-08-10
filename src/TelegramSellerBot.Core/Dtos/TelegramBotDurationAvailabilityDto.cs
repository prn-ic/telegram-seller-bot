namespace TelegramSellerBot.Core.Dtos
{
    public class TelegramBotDurationAvailabilityDto
    {
        public int Id { get; set; }
        public TelegramBotDurationDto? Duration { get; set; }
        public TelegramBotDto? Service { get; set; }
        public decimal Cost { get; set; }
    }
}