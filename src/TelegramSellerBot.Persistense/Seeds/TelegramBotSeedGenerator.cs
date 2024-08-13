using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Persistense.Seeds
{
    public class TelegramBotSeedGenerator
    {
        public static IEnumerable<TelegramBot> GenerateSimpleBots(int count = 8)
        {
            var result = new List<TelegramBot>();

            for (int i = 0; i < count; i++)
            {
                result.Add(
                    new TelegramBot(
                        "Some service #" + (i + 1),
                        "some description, none more",
                        "https://google.com"
                    )
                );
            }

            return result;
        }
    }
}
