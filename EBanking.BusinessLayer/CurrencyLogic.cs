using EBanking.BusinessLayer.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.Validation.Validators;

namespace EBanking.BusinessLayer
{
    public class CurrencyLogic : ICurrencyLogic
    {
        IBroker Broker { get; }
        public CurrencyLogic(IBroker broker)
        {
            Broker = broker;
        }
        public async Task<Currency> AddCurrencyAsync(string name, string code)
        {
            var newCurrency = new Currency()
            {
                Name = name,
                CurrencyCode = code
            };
            var resultInfo = new CurrencyValidator(newCurrency).Validate();
            if (resultInfo.IsValid == false)
                throw new Exception(resultInfo.GetErrorsString());
            try
            {
                await Broker.StartConnectionAsync();
                await Broker.StartTransactionAsync();
                var currencyFromDB = await Broker.CreateCurrencyAsync(newCurrency);
                await Broker.CommitTransactionAsync();
                return currencyFromDB;
            }
            catch
            {
                await Broker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }

        public async Task<Currency> FindCurrencyAsync(int currencyId)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var currency = await Broker.GetCurrencyByIdAsync(new Currency() { Id = currencyId });
                if (currency == null)
                    throw new Exception($"Валута са идентификатором: '{currencyId}' није пронађена.");
                return currency;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            try
            {
                await Broker.StartConnectionAsync();
                return await Broker.GetAllCurrenciesAsync(new Currency());
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
        public async Task<Currency> RemoveCurrencyAsync(int currencyId)
        {
            var currency = new Currency() { Id = currencyId };
            try
            {
                await Broker.StartConnectionAsync();
                var accounts = await Broker.GetAllAccountsByCurrencyAsync(currency);
                if (accounts != null && accounts.Count > 0)
                    throw new Exception("Не сме се обрисати валута коју користе рачуни.");
                await Broker.StartTransactionAsync();
                var deletedCurrency = await Broker.DeleteCurrencyAsync(currency);
                await Broker.CommitTransactionAsync();
                return deletedCurrency;
            }
            catch
            {
                await Broker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
        public async Task<Currency> UpdateCurrencyAsync(int currencyId, string name, string code)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var currency = await Broker.GetCurrencyByIdAsync(new Currency() { Id = currencyId });
                if (currency == null)
                    throw new Exception($"Валута са идентификатором: '{currencyId}' није пронађена.");
                currency.Name = name;
                currency.CurrencyCode = code;

                var resultInfo = new CurrencyValidator(currency).Validate();
                if (resultInfo.IsValid == false)
                    throw new Exception(resultInfo.GetErrorsString());

                await Broker.StartTransactionAsync();
                var updatedCurrency = await Broker.UpdateCurrencyByIdAsync(currency);
                await Broker.CommitTransactionAsync();
                return updatedCurrency;
            }
            catch
            {
                await Broker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
    }
}