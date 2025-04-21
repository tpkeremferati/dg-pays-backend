using Dg.Pays.Domain.ValueObjects;

namespace Dg.Pays.Domain.Tests.ValueObjects
{
    public class CardSchemeTypeTests
    {
        [Fact]
        public void FromCardNumber_ValidVisaCard_ShouldReturnVisa()
        {
            // Arrange
            var cardNumber = "4111111111111111";

            // Act
            var cardScheme = CardSchemeType.FromCardNumber(cardNumber);

            // Assert
            Assert.Equal("Visa", cardScheme.Name);
        }

        [Fact]
        public void FromCardNumber_ValidMasterCard_ShouldReturnMastercard()
        {
            // Arrange
            var cardNumber = "5555555555554444";

            // Act
            var cardScheme = CardSchemeType.FromCardNumber(cardNumber);

            // Assert
            Assert.Equal("Mastercard", cardScheme.Name);
        }
    }
}
