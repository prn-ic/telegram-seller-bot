namespace TelegramSellerBot.Core.Exceptions
{
    public class SubscriptionStatusException : DomainException
    {
        public SubscriptionStatusException(string statusName)
            : base(string.Format("Invalid subscription status. The {0} already it", statusName)) { }
    }
}
