using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.UnitTests
{
    public class TelegramBotTests
    {
        [Fact]
        public void InitializeModel_WithRightParameters_ShouldBeOk()
        {
            // Arrange
            string? name = "asdf";
            string? description = "asdf";
            string? telegramBotLink = "sadf";

            // Act
            var result = new TelegramBot(name, description, telegramBotLink);

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("asdf", "", "")]
        [InlineData("", "asdf", "")]
        [InlineData("", "", "asdf")]
        [InlineData("as", "asdf", "asdf")]
        public void InitializeModel_WithInvalidParameters_ThrowsInvalidRequestException(string name, string description, string telegramBotLink)
        {
            // Arrange
            
            // Act
            var func = () => 
            {
                var actual = new TelegramBot(name, description, telegramBotLink);
            };

            // Assert
            Assert.Throws<InvalidRequestException>(func);
        }

        [Theory]
        [InlineData("")]
        [InlineData("as")]
        public void SetName_WithInvalidParameter_ThrowsInvalidRequestException(string value)
        {
            // Arrange
            string? description = "asdf";
            string? telegramBotLink = "sadf";
            
            // Act
            var func = () => 
            {
                var actual = new TelegramBot(value, description, telegramBotLink);
            };

            // Assert
            Assert.Throws<InvalidRequestException>(func);
        }

        
        [Theory]
        [InlineData("")]
        [InlineData("as")]
        public void SetDescription_WithInvalidParameter_ThrowsInvalidRequestException(string value)
        {
            // Arrange
            string? name = "asdf";
            string? telegramBotLink = "sadf";
            
            // Act
            var func = () => 
            {
                var actual = new TelegramBot(name, value, telegramBotLink);
            };

            // Assert
            Assert.Throws<InvalidRequestException>(func);
        }

        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void SetTelegramBotLink_WithInvalidParameter_ThrowsInvalidRequestException(string value)
        {
            // Arrange
            string? name = "asdf";
            string? description = "sadf";
            
            // Act
            var func = () => 
            {
                var actual = new TelegramBot(name, description, value);
            };

            // Assert
            Assert.Throws<InvalidRequestException>(func);
        }
    }
}