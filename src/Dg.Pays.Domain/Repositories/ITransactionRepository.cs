using Dg.Pays.Domain.Entities;

namespace Dg.Pays.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddAsync(Transaction transaction);
        Task<Transaction?> GetTransactionByIdAsync(string id);
        Task<IEnumerable<Transaction>> GetTransactionsByFiltersAsync(DateTime? startDate, DateTime? endDate, decimal? minAmount, decimal? maxAmount);
    }
}
