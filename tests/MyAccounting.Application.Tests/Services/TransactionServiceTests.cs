using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using MyAccounting.Application.Common.Interfaces;
using MyAccounting.Application.Common.Mapping;
using MyAccounting.Application.Dtos;
using MyAccounting.Application.Services;
using MyAccounting.Domain.Entities;
using MyAccounting.Domain.ValueObjects;
using Xunit;

namespace MyAccounting.Application.Tests.Services
{
    public class TransactionServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        
        public TransactionServiceTests()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();
            
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
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
                }
            };

            var transactionsMock = new List<Transaction>
            {
                transaction
            }.AsQueryable().BuildMock();
            
            _transactionRepositoryMock.Setup(x => x.GetByIdAsync(transaction.Id))
                .Returns(transactionsMock.Object);

            var transactionService = CreateDefaultTransactionService();

            var expectation = new TransactionDto
            {
                Id = transaction.Id,
                Money = new MoneyDto
                {
                    Amount = transaction.Money.Amount,
                    Currency = transaction.Money.Currency
                },
                OccurredAt = transaction.OccurredAt
            };

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
                }
            };

            var transactionsMock = new List<Transaction>
            {
                transaction
            }.AsQueryable().BuildMock();
            
            _transactionRepositoryMock.Setup(x => x.GetAllAsync())
                .Returns(transactionsMock.Object);

            var transactionService = CreateDefaultTransactionService();

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
                    OccurredAt = transaction.OccurredAt
                }
            };

            var result = await transactionService.GetAllAsync();

            result.Should().BeEquivalentTo(expectation);
        }
        
        [Fact]
        public async Task CreateAsync_GivenRequiredObjectForCreation_CallsCreateAsyncInRepository()
        {
            var transactionService = CreateDefaultTransactionService();

            await transactionService.CreateAsync(It.IsAny<TransactionDto>());

            _transactionRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Transaction>()), Times.Once());
        }
        
        private TransactionService CreateDefaultTransactionService()
        {
            return new TransactionService(
                _transactionRepositoryMock.Object,
                _mapper
            );
        }
    }
}