using Dg.Pays.Domain.Entities;
using Dg.Pays.Domain.ValueObjects;

namespace Dg.Pays.Domain.Tests.Entities
{
    public class TransactionTests
    {
        [Fact]
        public void Transaction_ValidCardNumber_ShouldSetResponseCodeTo00()
        {
            // Arrange
            var cardNumber = new CardNumber("4111111111111111");
            var cardHolderName = "John Doe";
            var amount = 100.00m;
            var expiryDate = new CardValidExpiryDate("1225"); // MMYY

            // Act
            var transaction = new Transaction(cardHolderName, cardNumber, amount, expiryDate);

            // Assert
            Assert.Equal("00", transaction.ResponseCode); // Expect "00" for valid card
        }

        [Fact]
        public void Transaction_InvalidCardNumber_ShouldSetResponseCodeToMinus1()
        {
            // Arrange
            var cardNumber = new CardNumber("4111111111111112"); // Invalid Luhn
            var cardHolderName = "John Doe";
            var amount = 100.00m;
            var expiryDate = new CardValidExpiryDate("1225"); // MMYY

            // Act
            var transaction = new Transaction(cardHolderName, cardNumber, amount, expiryDate);

            // Assert
            Assert.Equal("-1", transaction.ResponseCode); // Expect "-1" for invalid card
        }

        [Fact]
        public void Transaction_ValidExpiryDate_ShouldCreateTransaction()
        {
            // Arrange
            var cardNumber = new CardNumber("4111111111111111");
            var cardHolderName = "John Doe";
            var amount = 100.00m;
            var expiryDate = new CardValidExpiryDate("1225"); // MMYY, valid future expiry

            // Act
            var transaction = new Transaction(cardHolderName, cardNumber, amount, expiryDate);

            // Assert
            Assert.Equal(expiryDate, transaction.ExpiryDate);
        }

        [Fact]
        public void Transaction_ShouldGenerateTransactionCode()
        {
            // Arrange
            var cardNumber = new CardNumber("4111111111111111");
            var cardHolderName = "John Doe";
            var amount = 100.00m;
            var expiryDate = new CardValidExpiryDate("1225"); // MMYY

            // Act
            var transaction = new Transaction(cardHolderName, cardNumber, amount, expiryDate);

            // Assert
            Assert.Equal(12, transaction.TransactionId.Length); // Ensure transaction code is 12 characters long
        }
    }
}
