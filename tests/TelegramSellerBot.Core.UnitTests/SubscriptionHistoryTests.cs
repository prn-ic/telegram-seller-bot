using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.UnitTests
{
    public class SubscriptionHistoryTests
    {
        [Fact]
        public void InitializeModel_WithRightParameters_ShouldBeOk()
        {
            // Arrange
            Subscription subscription = GenerateSubscription();
            DateTime createdAt = DateTime.Now;
            TelegramBot telegramBot = new("1234", "asdfasd", "https://t.me/smth");
            SubscriptionStatus subscriptionStatus = new SubscriptionStatus()
            {
                Id = SubscriptionStatuses.Active,
                Name = "test"
            };
            decimal cost = 100;

            // Act
            SubscriptionHistory history =
                new(
                    subscription,
                    telegramBot,
                    subscriptionStatus,
                    cost,
                    SubscriptionStatuses.Active,
                    createdAt
                );

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-7922816251426433759)]
        public void InitializeModel_CostLessThanZero_ThrowsInvalidCostException(decimal value)
        {
            // Arrange
            Subscription subscription = GenerateSubscription();
            DateTime createdAt = DateTime.Now;
            TelegramBot telegramBot = new("1234", "asdfasd", "https://t.me/smth");
            SubscriptionStatus subscriptionStatus = new SubscriptionStatus()
            {
                Id = SubscriptionStatuses.Active,
                Name = "test"
            };

            // Act
            var func = () =>
            {
                SubscriptionHistory result =
                    new(
                        subscription,
                        telegramBot,
                        subscriptionStatus,
                        value,
                        SubscriptionStatuses.Active,
                        createdAt
                    );
            };
            // Assert
            Assert.Throws<InvalidCostException>(func);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-7922816251426433759)]
        public void SetCost_CostLessThanZero_ThrowsInvalidCostException(decimal value)
        {
            // Arrange
            Subscription subscription = GenerateSubscription();
            DateTime createdAt = DateTime.Now;
            TelegramBot telegramBot = new("1234", "asdfasd", "https://t.me/smth");
            SubscriptionStatus subscriptionStatus = new SubscriptionStatus()
            {
                Id = SubscriptionStatuses.Active,
                Name = "test"
            };
            decimal cost = 100;
            SubscriptionHistory history =
                new(
                    subscription,
                    telegramBot,
                    subscriptionStatus,
                    cost,
                    SubscriptionStatuses.Active,
                    createdAt
                );

            // Act
            var func = () =>
            {
                history.SetCost(value);
            };
            // Assert
            Assert.Throws<InvalidCostException>(func);
        }

        private Subscription GenerateSubscription()
        {
            Guid id = Guid.NewGuid();
            string? telegramUserId = "1234567890";
            DateTime createdAt = DateTime.UtcNow;
            DateTime? modifiedAt = DateTime.UtcNow;
            TelegramBot service = new TelegramBot("Test", "Test", "TelegramBotLink");
            TelegramBotDuration duration = new TelegramBotDuration()
            {
                Id = TelegramServiceDurations.Year,
                Name = TelegramServiceDurations.Year.ToString()
            };
            SubscriptionStatus status = new SubscriptionStatus()
            {
                Id = SubscriptionStatuses.Active,
                Name = SubscriptionStatuses.Active.ToString(),
            };
            SubscriptionStatuses statusId = SubscriptionStatuses.Active;
            TelegramServiceDurations durationid = TelegramServiceDurations.Year;
            return new Subscription(
                telegramUserId,
                createdAt,
                modifiedAt,
                service,
                duration,
                status,
                statusId,
                durationid
            );
        }
    }
}
