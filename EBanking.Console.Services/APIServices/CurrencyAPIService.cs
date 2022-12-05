using EBanking.Models;
using EBanking.Services.HttpClients;
using EBanking.Services.Interfaces;

namespace EBanking.Services.APIServices
{
    public class CurrencyAPIService : ICurrencyService
    {
        private readonly ICurrencyHttpClient currencyHttpClient;

        public CurrencyAPIService(ICurrencyHttpClient currencyHttpClient)
        {
            this.currencyHttpClient = currencyHttpClient;
        }

        public async Task<Currency?> AddCurrencyAsync(string name, string code)
        {
            return await currencyHttpClient.PostAsync(name, code);
        }

        public async Task<Currency?> DeleteCurrencyAsync(int id)
        {
            return await currencyHttpClient.DeleteAsync(id);
        }

        public async Task<IEnumerable<Currency>?> GetAllCurrenciesAsync()
        {
            return await currencyHttpClient.GetAsync();
        }

        public async Task<Currency?> GetCurrencyAsync(int id)
        {
            return await currencyHttpClient.GetAsync(id);
        }

        public async Task<Currency?> UpdateCurrencyAsync(int id, string name, string code)
        {
            return await currencyHttpClient.PutAsync(id, name, code);
        }
    }
}
