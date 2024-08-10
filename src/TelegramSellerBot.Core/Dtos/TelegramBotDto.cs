namespace TelegramSellerBot.Core.Dtos
{
    public class TelegramBotDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? TelegramBotLink { get; set; }
    }
}