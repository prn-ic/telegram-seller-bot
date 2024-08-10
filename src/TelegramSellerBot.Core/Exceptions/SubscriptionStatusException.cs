namespace TelegramSellerBot.Core.Exceptions
{
    public class SubscriptionStatusException : Exception
    {
        public SubscriptionStatusException(string statusName)
            : base(string.Format("Invalid subscription status. The {0} already it", statusName)) { }
    }
}
