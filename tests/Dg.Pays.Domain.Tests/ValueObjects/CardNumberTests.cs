using Dg.Pays.Domain.ValueObjects;

namespace Dg.Pays.Domain.Tests.ValueObjects
{
    public class CardNumberTests
    {
        [Fact]
        public void IsValidCard_ValidCardNumber_ReturnsTrue()
        {
            // Arrange
            var cardNumber = new CardNumber("4111111111111111");

            // Act
            var result = cardNumber.IsValidCard();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidCard_InvalidCardNumber_ReturnsFalse()
        {
            // Arrange
            var cardNumber = new CardNumber("4111111111111112");

            // Act
            var result = cardNumber.IsValidCard();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void MaskedCardNumber_ShouldMaskCorrectly()
        {
            // Arrange
            var cardNumber = new CardNumber("4111111111111111");

            // Act
            var maskedCard = cardNumber.Masked;

            // Assert
            Assert.Equal("411111******1111", maskedCard);
        }

        [Fact]
        public void MaskedCardNumber_ShortCardNumber_ShouldNotMask()
        {
            // Arrange
            var cardNumber = new CardNumber("12345678");

            // Act
            var maskedCard = cardNumber.Masked;

            // Assert
            Assert.Equal("12345678", maskedCard);  // No masking for short numbers
        }
    }
}
