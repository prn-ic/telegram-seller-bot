namespace TelegramSellerBot.Core.Entities
{
    public class BaseEntity<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}