using Dg.Pays.Domain.Exceptions;
using Dg.Pays.Domain.ValueObjects;

namespace Dg.Pays.Tests.ValueObjects
{
    public class CardValidExpiryDateTests
    {
        [Fact]
        public void CardValidExpiryDate_ValidExpiryDate_ShouldCreateInstance()
        {
            // Arrange
            var expiryDate = "0630"; // Valid MMYY (December 2025)

            // Act
            var cardValidExpiryDate = new CardValidExpiryDate(expiryDate);

            // Assert
            Assert.Equal(expiryDate, cardValidExpiryDate.ExpiryDate);
        }

        [Fact]
        public void CardValidExpiryDate_ExpiredCard_ShouldThrowInvalidCardExpirationException()
        {
            // Arrange
            var expiredDate = "1118"; // Expired date in MMYY format (May 2020)

            // Act & Assert
            var exception = Assert.Throws<InvalidCardExpirationException>(() => new CardValidExpiryDate(expiredDate));
            Assert.Equal("The expiry date is invalid or the card has expired.", exception.Message);
        }

        [Fact]
        public void CardValidExpiryDate_InvalidExpiryDateFormat_ShouldThrowInvalidCardExpirationException()
        {
            // Arrange
            var invalidFormatDate = "2030"; // Incorrect format (YYYY instead of MMYY)

            // Act & Assert
            var exception = Assert.Throws<InvalidCardExpirationException>(() => new CardValidExpiryDate(invalidFormatDate));
            Assert.Equal("The expiry date is invalid or the card has expired.", exception.Message);
        }

        [Fact]
        public void CardValidExpiryDate_InvalidMonth_ShouldThrowInvalidCardExpirationException()
        {
            // Arrange
            var invalidMonth = "1530"; // Invalid month (13th month does not exist)

            // Act & Assert
            var exception = Assert.Throws<InvalidCardExpirationException>(() => new CardValidExpiryDate(invalidMonth));
            Assert.Equal("The expiry date is invalid or the card has expired.", exception.Message);
        }
    }
}
