using Dg.Pays.Application.Services;
using Dg.Pays.Domain.Entities;
using Dg.Pays.Domain.Exceptions;
using Dg.Pays.Domain.Repositories;
using Dg.Pays.Domain.ValueObjects;
using Moq;

namespace Dg.Pays.Tests.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _transactionService = new TransactionService(_mockTransactionRepository.Object);
        }

        [Fact]
        public async Task ProcessTransactionAsync_ValidInput_ShouldCreateAndSaveTransaction()
        {
            // Arrange
            var cardHolderName = "Ali Veli";
            var cardNumber = "4111111111111111";
            var amount = 100.00m;
            var expiryDate = "1225";

            // Act
            var transaction = await _transactionService.ProcessTransactionAsync(cardHolderName, cardNumber, amount, expiryDate);

            // Assert
            Assert.NotNull(transaction);
            Assert.Equal(cardHolderName, transaction.CardHolderName);
            Assert.Equal(cardNumber, transaction.CardNumber.Value);
            Assert.Equal(amount, transaction.Amount);
            Assert.Equal("00", transaction.ResponseCode);

            _mockTransactionRepository.Verify(x => x.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Fact]
        public async Task ProcessTransactionAsync_InvalidExpiryDate_ShouldThrowInvalidCardExpirationException()
        {
            // Arrange
            var cardHolderName = "Ali Veli";
            var cardNumber = "4111111111111111";
            var amount = 100.00m;
            var invalidExpiryDate = "0520";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidCardExpirationException>(() => _transactionService.ProcessTransactionAsync(cardHolderName, cardNumber, amount, invalidExpiryDate));
            Assert.Equal("The expiry date is invalid or the card has expired.", exception.Message);

            _mockTransactionRepository.Verify(x => x.AddAsync(It.IsAny<Transaction>()), Times.Never);
        }

        [Fact]
        public async Task GetTransactionBynIdAsync_ShouldReturnTransaction()
        {
            // Arrange
            var transactionId = "abc123456789";
            var expectedTransaction = new Transaction("Ali Veli", new CardNumber("4111111111111111"), 100.00m, new CardValidExpiryDate("1225"));

            _mockTransactionRepository.Setup(x => x.GetTransactionByIdAsync(transactionId))
                .ReturnsAsync(expectedTransaction);

            // Act
            var result = await _transactionService.GetTransactionByIdAsync(transactionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTransaction.TransactionId, result.TransactionId);

            _mockTransactionRepository.Verify(x => x.GetTransactionByIdAsync(transactionId), Times.Once);
        }

        [Fact]
        public async Task GetTransactionsByFiltersAsync_ShouldReturnFilteredTransactions()
        {
            // Arrange
            var startDate = new DateTime(2023, 01, 01);
            var endDate = new DateTime(2023, 12, 31);
            var minAmount = 50.00m;
            var maxAmount = 200.00m;

            var expectedTransactions = new List<Transaction>
            {
                new("Ali Veli", new CardNumber("4111111111111111"), 100.00m, new CardValidExpiryDate("1225")),
                new("Ali Veli", new CardNumber("5555555555554444"), 150.00m, new CardValidExpiryDate("1125"))
            };

            _mockTransactionRepository.Setup(x => x.GetTransactionsByFiltersAsync(startDate, endDate, minAmount, maxAmount))
                .ReturnsAsync(expectedTransactions);

            // Act
            var result = await _transactionService.GetTransactionsByFiltersAsync(startDate, endDate, minAmount, maxAmount);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            // Verify that GetTransactionsByFiltersAsync was called once
            _mockTransactionRepository.Verify(x => x.GetTransactionsByFiltersAsync(startDate, endDate, minAmount, maxAmount), Times.Once);
        }
    }
}
