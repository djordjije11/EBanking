using EBanking.BusinessLayer.Interfaces;
using EBanking.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyLogic CurrencyLogic;
        public CurrencyController(ICurrencyLogic currencyLogic)
        {
            CurrencyLogic = currencyLogic;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Currency>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var currencies = await CurrencyLogic.GetAllCurrenciesAsync();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Currency))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var currency = await CurrencyLogic.FindCurrencyAsync(id);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(currency);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Currency))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Currency currency)
        {
            try
            {
                var createdCurrency = await CurrencyLogic.AddCurrencyAsync(currency.Name, currency.CurrencyCode);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Created(new Uri(Request.GetEncodedUrl() + "/" + createdCurrency.Id), createdCurrency);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Currency))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedCurrency = await CurrencyLogic.RemoveCurrencyAsync(id);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(deletedCurrency);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Currency))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] Currency currency)
        {
            if (currency == null)
                return BadRequest();
            try
            {
                var updatedCurrency = await CurrencyLogic.UpdateCurrencyAsync(id, currency.Name, currency.CurrencyCode);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(updatedCurrency);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
