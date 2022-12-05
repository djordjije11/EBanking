using EBanking.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using EBanking.API.DTO.AccountDtos;
using AutoMapper;
using EBanking.API.DTO.TransactionDtos;

namespace EBanking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountLogic AccountLogic;
        public IMapper Mapper { get; }

        public AccountController(IAccountLogic accountLogic, IMapper mapper)
        {
            AccountLogic = accountLogic;
            Mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAccountDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var accounts = await AccountLogic.GetAllAccountsAsync();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(Mapper.Map<IEnumerable<GetAccountDto>>(accounts));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccountDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var account = await AccountLogic.FindAccountAsync(id);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(Mapper.Map<GetAccountDto>(account));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
        [HttpGet("{id}/Transactions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransactionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTransactions(int id)
        {
            try
            {
                var transactions = await AccountLogic.GetTransactionsByAccount(id);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(Mapper.Map<IEnumerable<TransactionDto>>(transactions));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAccountDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(AddAccountDto accountDto)
        {
            try
            {
                var account = await AccountLogic.AddAccountAsync(accountDto.UserId, accountDto.CurrencyId);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Created(new Uri(Request.GetEncodedUrl() + "/" + account.Id), Mapper.Map<GetAccountDto>(account));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccountDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedAccount = await AccountLogic.RemoveAccountAsync(id);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(Mapper.Map<GetAccountDto>(deletedAccount));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccountDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountDto accountDto)
        {
            if (accountDto == null)
                return BadRequest();
            try
            {
                var account = await AccountLogic.UpdateAccountAsync(id, accountDto.Status);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(Mapper.Map<GetAccountDto>(account));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
