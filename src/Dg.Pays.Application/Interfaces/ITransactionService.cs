using Dg.Pays.Domain.Entities;

namespace Dg.Pays.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> ProcessTransactionAsync(string cardHolderName, string cardNumber, decimal amount, string expiryDate);
        Task<Transaction?> GetTransactionByIdAsync(string transactionId);
        Task<IEnumerable<Transaction>> GetTransactionsByFiltersAsync(DateTime? startDate, DateTime? endDate, decimal? minAmount, decimal? maxAmount);
    }
}
