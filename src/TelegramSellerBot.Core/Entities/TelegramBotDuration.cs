using TelegramSellerBot.Core.Common;

namespace TelegramSellerBot.Core.Entities
{
    public class TelegramBotDuration : BaseEntity<TelegramServiceDurations>
    {
        public string? Name { get; private set; }
    }
}