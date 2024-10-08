namespace TelegramSellerBot.Core.Exceptions
{
    public class InvalidRequestException : DomainException
    {
        public InvalidRequestException() : base() { }
        public InvalidRequestException(string text) : base(text) { }
        public InvalidRequestException(string text, Exception inner) : base(text, inner) { }
    }
}