using EBanking.BusinessLayer.Interfaces;
using EBanking.Models;
using EBanking.Services.Interfaces;

namespace EBanking.Services.LogicServices
{
    public class CurrencyLogicService : ICurrencyService
    {
        private readonly ICurrencyLogic currencyLogic;
        public CurrencyLogicService(ICurrencyLogic currencyLogic)
        {
            this.currencyLogic = currencyLogic;
        }
        public async Task<Currency> AddCurrencyAsync(string name, string code)
        {
            return await currencyLogic.AddCurrencyAsync(name, code);
        }

        public async Task<Currency> DeleteCurrencyAsync(int id)
        {
            return await currencyLogic.RemoveCurrencyAsync(id);
        }

        public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync()
        {
            return await currencyLogic.GetAllCurrenciesAsync();
        }

        public async Task<Currency> GetCurrencyAsync(int id)
        {
            return await currencyLogic.FindCurrencyAsync(id);
        }

        public async Task<Currency> UpdateCurrencyAsync(int id, string name, string code)
        {
            return await currencyLogic.UpdateCurrencyAsync(id, name, code);
        }
    }
}
