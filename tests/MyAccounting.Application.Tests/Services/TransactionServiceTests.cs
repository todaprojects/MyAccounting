using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MyAccounting.Application.Common.Interfaces;
using MyAccounting.Application.Common.Mapping;
using MyAccounting.Application.Dtos;
using MyAccounting.Application.Services;
using MyAccounting.Domain.Entities;
using MyAccounting.Domain.ValueObjects;
using Xunit;

namespace MyAccounting.Application.Tests.Services
{
    public class TransactionServiceTests : DbContextFixture
    {
        private readonly IMapper _mapper;

        private readonly IApplicationDbContext _context;
        
        public TransactionServiceTests()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();

            _context = DbContext;
        }
        
        [Fact]
        public async Task GetByIdAsync_GivenExistingTransactionId_ReturnsExistingTransaction()
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Money = new Money
                {
                    Amount = 100M,
                    Currency = Currency.Eur
                },
                Beneficiary = new Actor
                {
                    Id = Guid.NewGuid(),
                    Name = "Coding school"
                },
                OccurredAt = DateTime.Today
            };

            _context.Transactions.Add(transaction);
            await DbContext.SaveChangesAsync();

            var expectation = new TransactionDto
            {
                Id = transaction.Id,
                Money = new MoneyDto
                {
                    Amount = transaction.Money.Amount,
                    Currency = transaction.Money.Currency
                },
                Beneficiary = new ActorDto
                {
                    Id = transaction.Beneficiary.Id,
                    Name = transaction.Beneficiary.Name
                },
                OccurredAt = transaction.OccurredAt
            };

            var transactionService = CreateDefaultTransactionService();
            var result = await transactionService.GetByIdAsync(transaction.Id);

            result.Should().BeEquivalentTo(expectation);
        }
        
        [Fact]
        public async Task GetAllAsync_ReturnsAllExistingTransactions()
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Money = new Money
                {
                    Amount = 100M,
                    Currency = Currency.Eur
                },
                Beneficiary = new Actor
                {
                    Id = Guid.NewGuid(),
                    Name = "Coding school"
                },
                OccurredAt = DateTime.Today
            };
            
            var transactions = new List<Transaction>
            {
                transaction
            };
            
            _context.Transactions.AddRange(transactions);
            await DbContext.SaveChangesAsync();
        
            var expectation = new List<TransactionDto>
            {
                new ()
                {
                    Id = transaction.Id,
                    Money = new MoneyDto
                    {
                        Amount = transaction.Money.Amount,
                        Currency = transaction.Money.Currency
                    },
                    Beneficiary = new ActorDto
                    {
                        Id = transaction.Beneficiary.Id,
                        Name = transaction.Beneficiary.Name
                    },
                    OccurredAt = transaction.OccurredAt
                }
            };
        
            var transactionService = CreateDefaultTransactionService();
            var result = await transactionService.GetAllAsync();
        
            result.Should().BeEquivalentTo(expectation);
        }
        
        [Fact]
        public async Task CreateAsync_GivenRequiredObjectForCreation_CallsCreateAsyncInRepository()
        {
            var transaction = new TransactionDto
            {
                Id = Guid.NewGuid(),
                Money = new MoneyDto
                {
                    Amount = 100M,
                    Currency = Currency.Eur
                },
                Beneficiary = new ActorDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Coding school"
                },
                OccurredAt = DateTime.Today
            };
            
            var transactionService = CreateDefaultTransactionService();
            var result =await transactionService.CreateAsync(transaction);
        
            result.Should().Be(transaction.Id);
        }
        
        private TransactionService CreateDefaultTransactionService()
        {
            return new TransactionService(
                _context,
                _mapper
            );
        }
    }
}