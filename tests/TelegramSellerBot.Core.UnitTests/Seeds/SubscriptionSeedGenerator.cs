using System.Collections;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Core.UnitTests.Seeds
{
    public class SubscriptionSeedGenerator
    {
        public static IEnumerable<object[]> GetInvalidParameters()
        {
            yield return new object[] { null, null, null };
            yield return new object[] { "", null, null };
            yield return new object[] { null, SubscriptionStatuses.Active, null };
            yield return new object[] { null, null, TelegramServiceDurations.HalfYear };
        }
    }
}
