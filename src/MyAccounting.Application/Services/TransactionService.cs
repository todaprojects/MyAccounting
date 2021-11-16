using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAccounting.Application.Common.Interfaces;
using MyAccounting.Application.Dtos;
using MyAccounting.Domain.Entities;

namespace MyAccounting.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }
        
        public async Task<TransactionDto> GetByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id)
                .FirstOrDefaultAsync();

            return _mapper.Map<TransactionDto>(transaction);;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync()
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task CreateAsync(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            
            await _transactionRepository.CreateAsync(transaction);
        }
    }
}