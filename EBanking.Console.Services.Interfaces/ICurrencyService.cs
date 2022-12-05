using EBanking.Models;

namespace EBanking.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> GetAllCurrenciesAsync();
        Task<Currency> GetCurrencyAsync(int id);
        Task<Currency> AddCurrencyAsync(string name, string code);
        Task<Currency> DeleteCurrencyAsync(int id);
        Task<Currency> UpdateCurrencyAsync(int id, string name, string code);
    }
}
