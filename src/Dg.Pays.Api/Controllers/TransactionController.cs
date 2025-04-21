using AutoMapper;
using Dg.Pays.Application.Contracts.DTOs;
using Dg.Pays.Application.Contracts.Requests;
using Dg.Pays.Application.Interfaces;
using Dg.Pays.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dg.Pays.Api.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _mapper = mapper;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessTransaction([FromBody] TransactionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var transaction = await _transactionService.ProcessTransactionAsync(request.CardHolderName, request.CardNumber, request.Amount, request.ExpiryDate);

                var transactionDto = _mapper.Map<TransactionDto>(transaction);

                return Ok(transactionDto);
            }
            catch (InvalidCardNumberException ex)
            {
                return BadRequest(new { ResponseCode = "-1", ex.Message });
            }
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTransaction([Required] string id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);

            if (transaction == null)
            {
                return NotFound(new { Message = "No transaction found for the given id." });
            }

            var transactionDto = _mapper.Map<TransactionDto>(transaction);

            return Ok(transactionDto);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetTransactionsByFilters([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] decimal? minAmount, [FromQuery] decimal? maxAmount)
        {
            var transactions = await _transactionService.GetTransactionsByFiltersAsync(startDate, endDate, minAmount, maxAmount);
            if (!transactions.Any())
            {
                return NotFound(new { Message = "No transactions found for the given parameters." });
            }

            var transactionsDto = _mapper.Map<IEnumerable<TransactionDto>>(transactions);

            return Ok(transactionsDto);
        }
    }
}
