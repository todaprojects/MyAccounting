using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using MyAccounting.Application.Dtos;
using MyAccounting.Application.Services;
using MyAccounting.Domain.ValueObjects;
using MyAccounting.WebApi.Controllers;
using Xunit;

namespace MyAccounting.WebApi.Tests.Controllers
{
    public class TransactionControllerTests
    {
        private readonly Mock<ITransactionService> _transactionServiceMock;

        public TransactionControllerTests()
        {
            _transactionServiceMock = new Mock<ITransactionService>();
        }

        [Fact]
        public async Task GetById_GivenExistingTransactionId_ReturnsExistingTransaction()
        {
            var transaction = new TransactionDto
            {
                Id = Guid.NewGuid(),
                Money = new MoneyDto
                {
                    Amount = 100M,
                    Currency = Currency.Eur
                }
            };

            _transactionServiceMock.Setup(x => x.GetByIdAsync(transaction.Id))
                .Returns(Task.FromResult(transaction));

            var transactionController = CreateDefaultTransactionController();

            var result = await transactionController.GetById(transaction.Id);

            result.Should().BeEquivalentTo(transaction);
        }
        
        [Fact]
        public async Task GetAll_ReturnsAllExistingTransactions()
        {
            var transaction = new TransactionDto
            {
                Id = Guid.NewGuid(),
                Money = new MoneyDto
                {
                    Amount = 100M,
                    Currency = Currency.Eur
                }
            };

            var transactions = new List<TransactionDto>
            {
                transaction
            };
            
            _transactionServiceMock.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<TransactionDto>>(transactions));

            var transactionController = CreateDefaultTransactionController();

            var result = await transactionController.GetAll();

            result.Should().BeEquivalentTo(transactions);
        }
        
        [Fact]
        public async Task Create_GivenRequiredObjectForCreation_CallsCreateAsyncInService()
        {
            var transactionController = CreateDefaultTransactionController();

            await transactionController.Create(It.IsAny<TransactionDto>());

            _transactionServiceMock.Verify(x => x.CreateAsync(It.IsAny<TransactionDto>()), Times.Once());
        }

        private TransactionController CreateDefaultTransactionController()
        {
            return new TransactionController(
                _transactionServiceMock.Object
            );
        }
    }
}