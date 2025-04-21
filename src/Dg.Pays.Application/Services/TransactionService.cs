using Dg.Pays.Application.Interfaces;
using Dg.Pays.Domain.Entities;
using Dg.Pays.Domain.Repositories;
using Dg.Pays.Domain.ValueObjects;

namespace Dg.Pays.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        public async Task<Transaction> ProcessTransactionAsync(string cardHolderName, string cardNumber, decimal amount, string expiryDate)
        {
            var cardNumberValueObject = new CardNumber(cardNumber);
            var cardValidExpiryDateValueObject = new CardValidExpiryDate(expiryDate);

            var transaction = new Transaction(cardHolderName, cardNumberValueObject, amount, cardValidExpiryDateValueObject);
            await _transactionRepository.AddAsync(transaction);

            return transaction;
        }

        public async Task<Transaction?> GetTransactionByIdAsync(string id)
        {
            return await _transactionRepository.GetTransactionByIdAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByFiltersAsync(DateTime? startDate, DateTime? endDate, decimal? minAmount, decimal? maxAmount)
        {
            return await _transactionRepository.GetTransactionsByFiltersAsync(startDate, endDate, minAmount, maxAmount);
        }
    }
}
