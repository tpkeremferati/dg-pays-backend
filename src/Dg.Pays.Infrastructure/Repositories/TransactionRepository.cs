using Dg.Pays.Domain.Entities;
using Dg.Pays.Domain.Repositories;
using Dg.Pays.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dg.Pays.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction?> GetTransactionByIdAsync(string id)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(s => s.TransactionId == id);

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByFiltersAsync(DateTime? startDate, DateTime? endDate, decimal? minAmount, decimal? maxAmount)
        {
            var query = _context.Transactions.AsQueryable();

            // Filter by date range using Timestamp field
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(s => s.Timestamp >= startDate && s.Timestamp <= endDate);
            }

            // Filter by amount range if provided
            if (minAmount.HasValue && maxAmount.HasValue)
            {
                query = query.Where(s => s.Amount >= minAmount && s.Amount <= maxAmount);
            }

            return await query.ToListAsync();
        }
    }
}
