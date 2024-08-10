using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Core.UnitTests
{
    public class TelegramBotDurationAvailabilityTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(792281625142643)]
        public void InitializeModel_WithValidParameters_ShouldBeOk(decimal cost)
        {
            // Arrange
            TelegramBotDuration duration = new ()
            {
                Id = Common.TelegramServiceDurations.Year,
                Name = Common.TelegramServiceDurations.Year.ToString()
            };
            TelegramBot service = new("test", "test", "test");
            service.Id = Guid.NewGuid();
            TelegramServiceDurations durationId = TelegramServiceDurations.Year;
            Guid serviceId = service.Id;
            
            // Act
            TelegramBotDurationAvailability actual = new(duration, service, cost, durationId, serviceId);
            
            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(792281625142643)]
        public void SetCost_WithValidParameter_ShouldBeOk(decimal value)
        {
            // Arrange
            TelegramBotDuration duration = new ()
            {
                Id = Common.TelegramServiceDurations.Year,
                Name = Common.TelegramServiceDurations.Year.ToString()
            };
            TelegramBot service = new("test", "test", "test");
            service.Id = Guid.NewGuid();
            TelegramServiceDurations durationId = TelegramServiceDurations.Year;
            Guid serviceId = service.Id;
            decimal cost = 10;
            TelegramBotDurationAvailability availability = new(duration, service, cost, durationId, serviceId);
            
            // Act
            availability.SetCost(value);

            // Assert
            Assert.True(true);
        }
    }
}