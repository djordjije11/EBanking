using EBanking.Models;

namespace EBanking.DataAccessLayer.Interfaces
{
    public interface ICurrencyBroker : IBroker
    {
        Task<Currency> CreateCurrencyAsync(Currency currency);
        Task<Currency> UpdateCurrencyByIdAsync(Currency currency);
        Task<Currency> DeleteCurrencyAsync(Currency currency);
        Task<Currency?> GetCurrencyByIdAsync(Currency currency);
        Task<List<Currency>> GetAllCurrenciesAsync(Currency currency);
        Task<List<Account>> GetAllAccountsByCurrencyAsync(Currency currency);
    }
}
