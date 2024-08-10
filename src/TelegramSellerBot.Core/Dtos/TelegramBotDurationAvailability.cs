namespace TelegramSellerBot.Core.Dtos
{
    public class TelegramBotDurationAvailability
    {
        public int Id { get; set; }
        public TelegramBotDurationDto? Duration { get; set; }
        public TelegramBotDto? Service { get; set; }
        public decimal Cost { get; set; }
    }
}