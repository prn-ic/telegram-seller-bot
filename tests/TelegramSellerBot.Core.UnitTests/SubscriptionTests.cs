using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Exceptions;
using TelegramSellerBot.Core.UnitTests.Seeds;

namespace TelegramSellerBot.Core.UnitTests
{
    public class SubscriptionTests
    {
        [Fact]
        public void InitializeModel_WithRightModel_ShouldBeOk()
        {
            // Arrange
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

            // Act
            Subscription subscription = new Subscription(
                telegramUserId,
                createdAt,
                modifiedAt,
                service,
                duration,
                status,
                statusId,
                durationid
            );

            // Assert
            Assert.True(true);
        }

        [Theory]
        [MemberData(
            nameof(SubscriptionSeedGenerator.GetInvalidParameters),
            MemberType = typeof(SubscriptionSeedGenerator)
        )]
        public void InitializeModel_HasInvalidValues_ThrowsInvalidRequestException(
            string telegramId,
            SubscriptionStatuses status,
            TelegramServiceDurations duration
        )
        {
            // ArrangeGuid
            DateTime createdAt = DateTime.UtcNow;
            DateTime? modifiedAt = DateTime.UtcNow;
            TelegramBot service = new TelegramBot("Test", "Test", "TelegramBotLink");
            TelegramBotDuration durationModel = new TelegramBotDuration()
            {
                Id = TelegramServiceDurations.Year,
                Name = TelegramServiceDurations.Year.ToString()
            };
            SubscriptionStatus statusModel = new SubscriptionStatus()
            {
                Id = SubscriptionStatuses.Active,
                Name = SubscriptionStatuses.Active.ToString(),
            };

            // Act
            var func = () =>
            {
                var subscription = new Subscription(
                    telegramId,
                    createdAt,
                    modifiedAt,
                    service,
                    durationModel,
                    statusModel,
                    status,
                    duration
                );
            };

            // Assert
            Assert.Throws<InvalidRequestException>(func);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ChangeTelegramId_WithInvalidValues_ThrowsInvalidRequestException(string? value)
        {
            // Arrange
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
            Subscription subscription = new Subscription(
                telegramUserId,
                createdAt,
                modifiedAt,
                service,
                duration,
                status,
                statusId,
                durationid
            );

            // Act
            var func = () =>
            {
                subscription.SetTelegramUserId(value);
            };

            // Assert
            Assert.Throws<InvalidRequestException>(func);
        }

        [Theory]
        [InlineData("10.08.2024", TelegramServiceDurations.Week, "17.08.2024")]
        [InlineData("10.08.2024", TelegramServiceDurations.TwoWeek, "24.08.2024")]
        [InlineData("10.08.2024", TelegramServiceDurations.Month, "10.09.2024")]
        [InlineData("10.08.2024", TelegramServiceDurations.ThreeMonth, "11.11.2024")]
        [InlineData("10.08.2024", TelegramServiceDurations.Year, "10.08.2025")]
        public void GetExpirationDate(
            string createdAtAsString,
            TelegramServiceDurations durationId,
            string expected
        )
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string? telegramUserId = "1234567890";
            DateTime createdAt = DateTime.Parse(createdAtAsString);
            DateTime? modifiedAt = createdAt;
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
            Subscription subscription = new Subscription(
                telegramUserId,
                createdAt,
                modifiedAt,
                service,
                duration,
                status,
                statusId,
                durationId
            );
            DateTime exceptedDate = DateTime.Parse(expected);

            // Act
            DateTime actual = subscription.GetExpirationDate();

            // Assert
            Assert.Equal(exceptedDate, actual);
        }

        [Theory]
        [InlineData("10.08.2024", TelegramServiceDurations.Week, "7")]
        [InlineData("10.08.2024", TelegramServiceDurations.TwoWeek, "14")]
        [InlineData("10.08.2024", TelegramServiceDurations.Month, "31")]
        [InlineData("10.08.2024", TelegramServiceDurations.ThreeMonth, "93")]
        [InlineData("10.08.2024", TelegramServiceDurations.Year, "365")]
        public void GetRemainingDays(
            string createdAtAsString,
            TelegramServiceDurations durationId,
            string expected
        )
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string? telegramUserId = "1234567890";
            DateTime createdAt = DateTime.Parse(createdAtAsString);
            DateTime? modifiedAt = createdAt;
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
            Subscription subscription = new Subscription(
                telegramUserId,
                createdAt,
                modifiedAt,
                service,
                duration,
                status,
                statusId,
                durationId
            );

            // Act
            int actual = subscription.GetRemainingDays();

            // Assert
            Assert.Equal(int.Parse(expected), actual);
        }
    }
}
