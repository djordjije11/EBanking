using EBanking.BusinessLayer.Interfaces;
using EBanking.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.AppControllers
{
    public class CurrencyController
    {
        public CurrencyController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public IServiceProvider ServiceProvider { get; }
        public async Task<Currency> CreateCurrencyAsync(string name, string code)
        {
            var currencyLogic = ServiceProvider.GetRequiredService<ICurrencyLogic>();
            return await currencyLogic.AddCurrencyAsync(name, code);
        }
        public async Task<Currency> UpdateCurrencyAsync(int currencyID, string name, string code)
        {
            var currencyLogic = ServiceProvider.GetRequiredService<ICurrencyLogic>();
            return await currencyLogic.UpdateCurrencyAsync(currencyID, name, code);
        }
        public async Task<Currency> DeleteCurrencyAsync(int currencyID)
        {
            var currencyLogic = ServiceProvider.GetRequiredService<ICurrencyLogic>();
            return await currencyLogic.RemoveCurrencyAsync(currencyID);
        }
        public async Task<Currency> ReadCurrencyAsync(int currencyID)
        {
            var currencyLogic = ServiceProvider.GetRequiredService<ICurrencyLogic>();
            return await currencyLogic.FindCurrencyAsync(currencyID);
        }
        public async Task<List<Currency>> ReadAllCurrenciesAsync()
        {
            var currencyLogic = ServiceProvider.GetRequiredService<ICurrencyLogic>();
            return await currencyLogic.GetAllCurrenciesAsync();
        }
    }
}
