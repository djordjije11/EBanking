using EBanking.BusinessLayer.Interfaces;
using EBanking.Models;
using EBanking.Models.ModelsDto;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionLogic TransactionLogic;
        public TransactionController(ITransactionLogic transactionLogic)
        {
            TransactionLogic = transactionLogic;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Transaction>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var transactions = await TransactionLogic.GetAllTransactionsAsync();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Transaction))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var transaction = await TransactionLogic.FindTransactionAsync(id);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Transaction))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(TransactionDto transactionDto)
        {
            try
            {
                var createdTransaction = await TransactionLogic.AddTransactionAsync(transactionDto.Amount, transactionDto.FromAccountNumber, transactionDto.ToAccountNumber);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Created(new Uri(Request.GetEncodedUrl() + "/" + createdTransaction.Id), createdTransaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
