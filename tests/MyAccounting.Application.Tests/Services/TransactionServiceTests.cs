using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MyAccounting.Application.Common.Mapping;
using MyAccounting.Application.Dtos;
using MyAccounting.Application.Services;
using MyAccounting.Domain.Entities;
using MyAccounting.Domain.ValueObjects;
using Xunit;
using Type = MyAccounting.Domain.ValueObjects.Type;

namespace MyAccounting.Application.Tests.Services
{
    public class TransactionServiceTests : DbContextFixture
    {
        private readonly IMapper _mapper;
        
        public TransactionServiceTests()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();
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
                Type = Type.Income,
                OccurredAt = DateTime.Today
            };

            DbContext.Transactions.Add(transaction);
            await DbContext.SaveChangesAsync();

            var expectation = new TransactionDto
            {
                Id = transaction.Id,
                Money = new MoneyDto
                {
                    Amount = transaction.Money.Amount,
                    Currency = transaction.Money.Currency
                },
                Type = Type.Income,
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
                Type = Type.Income,
                OccurredAt = DateTime.Today
            };
            
            var transactions = new List<Transaction>
            {
                transaction
            };
            
            DbContext.Transactions.AddRange(transactions);
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
                    Type = Type.Income,
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
                Type = Type.Income,
                OccurredAt = DateTime.Today
            };
            
            var transactionService = CreateDefaultTransactionService();
            var result =await transactionService.CreateAsync(transaction);
        
            result.Should().Be(transaction.Id);
        }
        
        private TransactionService CreateDefaultTransactionService()
        {
            return new TransactionService(
                DbContext,
                _mapper
            );
        }
    }
}