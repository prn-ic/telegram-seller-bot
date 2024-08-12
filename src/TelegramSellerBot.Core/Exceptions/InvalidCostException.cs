namespace TelegramSellerBot.Core.Exceptions
{
    public class InvalidCostException : DomainException
    {
        public InvalidCostException(string cost) 
            : base("The cost has invalid value: " + cost) { }
    }
}
