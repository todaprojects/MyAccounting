using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyAccounting.Application.Dtos;

namespace MyAccounting.Application.Services
{
    public interface ITransactionService
    {
        Task<TransactionDto> GetByIdAsync(Guid id);

        Task<IEnumerable<TransactionDto>> GetAllAsync();

        Task<Guid> CreateAsync(TransactionDto transactionDto);
    }
}