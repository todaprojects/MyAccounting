using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyAccounting.Application.Common.Interfaces;
using MyAccounting.Domain.Entities;
using MyAccounting.Infrastructure.Persistence;

namespace MyAccounting.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IQueryable<Transaction> GetByIdAsync(Guid id)
        {
            return _context.Transactions.AsNoTracking()
                .Where(t => t.Id == id);
        }

        public IQueryable<Transaction> GetAllAsync()
        {
            return _context.Transactions.AsNoTracking();
        }

        public async Task CreateAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}