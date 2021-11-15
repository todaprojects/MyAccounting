using System;
using System.Linq;
using System.Threading.Tasks;
using MyAccounting.Domain.Entities;

namespace MyAccounting.Application.Common.Interfaces
{
    public interface ITransactionRepository
    {
        IQueryable<Transaction> GetByIdAsync(Guid id);

        IQueryable<Transaction> GetAllAsync();

        Task CreateAsync(Transaction transaction);
    }
}