using EBanking.Models;

namespace EBanking.BusinessLayer.Interfaces
{
    public interface ICurrencyLogic
    {
        Task<Currency> AddCurrencyAsync(string name, string code);
        Task<Currency> UpdateCurrencyAsync(int currencyId, string name, string code);
        Task<Currency> RemoveCurrencyAsync(int currencyId);
        Task<Currency> FindCurrencyAsync(int currencyId);
        Task<List<Currency>> GetAllCurrenciesAsync();
    }
}
