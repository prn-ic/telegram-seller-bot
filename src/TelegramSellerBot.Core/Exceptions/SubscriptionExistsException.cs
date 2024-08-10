namespace TelegramSellerBot.Core.Exceptions
{
    public class SubscriptionExistsException : Exception
    {
        public SubscriptionExistsException()
            : base("Subscription already exists") { }
    }
}
