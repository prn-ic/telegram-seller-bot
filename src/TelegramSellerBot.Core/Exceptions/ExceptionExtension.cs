namespace TelegramSellerBot.Core.Exceptions
{
    public static class ExceptionExtension
    {
        public static void ThrowIfStringRangeIsInvalid(
            this string? value,
            int minLength = 0,
            int maxLength = int.MaxValue
        )
        {
            if (value is null || value.Length < minLength || value.Length > maxLength)
            {
                throw new InvalidRequestException(
                    string.Format(
                        "The field is invalid length or null. Min: {0}, Max: {1}",
                        maxLength,
                        minLength
                    )
                );
            }
        }

        public static void ThrowIsValueNull<T>(this T? value)
        {
            if (value is null)
            {
                throw new InvalidRequestException("The field is required");
            }
        }

        public static void ThrowIfLessThanZero(this decimal value)
        {
            if (value < 0)
            {
                throw new InvalidCostException("The value must be greater than zero");
            }
        }
    }
}
