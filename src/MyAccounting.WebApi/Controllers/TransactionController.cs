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
        public async Task<ActionResult<TransactionDto>> GetById(Guid id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);

            return Ok(transaction);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();

            return Ok(transactions);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(TransactionDto transactionDto)
        {
            var transactionId = await _transactionService.CreateAsync(transactionDto);
            
            return CreatedAtAction(nameof(GetById), new { id = transactionId }, transactionId);
        }
    }
}