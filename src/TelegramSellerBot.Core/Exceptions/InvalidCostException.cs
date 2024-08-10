namespace TelegramSellerBot.Core.Exceptions
{
    public class InvalidCostException : Exception
    {
        public InvalidCostException(string cost) 
            : base("The cost has invalid value: " + cost) { }
    }
}
