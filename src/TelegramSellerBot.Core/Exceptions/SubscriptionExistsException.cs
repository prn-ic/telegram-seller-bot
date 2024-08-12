namespace TelegramSellerBot.Core.Exceptions
{
    public class SubscriptionExistsException : DomainException
    {
        public SubscriptionExistsException()
            : base("Subscription already exists") { }
    }
}
