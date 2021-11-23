using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyAccounting.Domain.Entities;

namespace MyAccounting.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Transaction> Transactions { get; set; }
        
        Task<int> SaveChangesAsync();
    }
}