using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Persistense.Seeds
{
    public class TelegramBotDurationAvailabilityGenerator
    {
        public static IEnumerable<TelegramBotDurationAvailability> GenerateDurationsByExistedBots(
            IEnumerable<TelegramBot> bots
        )
        {
            var result = new List<TelegramBotDurationAvailability>();

            int id = 0;

            foreach (var bot in bots)
            {
                result.Add(
                    new TelegramBotDurationAvailability(bot.Id, TelegramServiceDurations.Week, 100)
                    {
                        Id = ++id
                    }
                );
                result.Add(
                    new TelegramBotDurationAvailability(
                        bot.Id,
                        TelegramServiceDurations.HalfYear,
                        200
                    )
                    {
                        Id = ++id
                    }
                );
                result.Add(
                    new TelegramBotDurationAvailability(bot.Id, TelegramServiceDurations.Year, 3100)
                    {
                        Id = ++id
                    }
                );
            }

            return result;
        }
    }
}
