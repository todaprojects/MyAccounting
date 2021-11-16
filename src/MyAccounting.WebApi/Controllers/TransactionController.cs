using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyAccounting.Application.Dtos;
using MyAccounting.Application.Services;

namespace MyAccounting.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        
        [HttpGet("{id:guid}")]
        public async Task<TransactionDto> GetById(Guid id)
        {
            return await _transactionService.GetByIdAsync(id);
        }
        
        [HttpGet]
        public async Task<IEnumerable<TransactionDto>> GetAll()
        {
            return await _transactionService.GetAllAsync();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(TransactionDto transactionDto)
        {
            await _transactionService.CreateAsync(transactionDto);
            
            return NoContent();
        }
    }
}