using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyAccounting.Application.Common.Interfaces;
using MyAccounting.Application.Dtos;
using MyAccounting.Domain.Entities;

namespace MyAccounting.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IApplicationDbContext _context;
        
        private readonly IMapper _mapper;

        public TransactionService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<TransactionDto> GetByIdAsync(Guid id)
        {
            var transaction = await _context.Transactions
                .AsNoTracking()
                .Where(t => t.Id == id)
                .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return transaction;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            var transactions = await _context.Transactions
                .AsNoTracking()
                .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            return transactions;
        }

        public async Task<Guid> CreateAsync(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction.Id;
        }
    }
}