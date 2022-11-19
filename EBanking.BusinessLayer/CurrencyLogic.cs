using EBanking.BusinessLayer.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.BusinessLayer
{
    public class CurrencyLogic : ICurrencyLogic
    {
        ICurrencyBroker CurrencyBroker { get; }
        public IServiceProvider ServiceProvider { get; }
        public CurrencyLogic(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            CurrencyBroker = ServiceProvider.GetRequiredService<ICurrencyBroker>();
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
                await CurrencyBroker.StartConnectionAsync();
                await CurrencyBroker.StartTransactionAsync();
                var currencyFromDB = await CurrencyBroker.CreateCurrencyAsync(newCurrency);
                await CurrencyBroker.CommitTransactionAsync();
                return currencyFromDB;
            }
            catch
            {
                await CurrencyBroker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await CurrencyBroker.EndConnectionAsync();
            }
        }
        public async Task<Currency> FindCurrencyAsync(int currencyId)
        {
            try
            {
                await CurrencyBroker.StartConnectionAsync();
                var currency = await CurrencyBroker.GetCurrencyByIdAsync(new Currency() { Id = currencyId });
                if (currency == null)
                    throw new Exception($"Валута са идентификатором: '{currencyId}' није пронађена.");
                return currency;
            }
            finally
            {
                await CurrencyBroker.EndConnectionAsync();
            }
        }
        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            try
            {
                await CurrencyBroker.StartConnectionAsync();
                return await CurrencyBroker.GetAllCurrenciesAsync(new Currency());
            }
            finally
            {
                await CurrencyBroker.EndConnectionAsync();
            }
        }
        public async Task<Currency> RemoveCurrencyAsync(int currencyId)
        {
            var currency = new Currency() { Id = currencyId };
            try
            {
                await CurrencyBroker.StartConnectionAsync();
                var accounts = await CurrencyBroker.GetAllAccountsByCurrencyAsync(currency);
                if (accounts != null && accounts.Count > 0)
                    throw new Exception("Не сме се обрисати валута коју користе рачуни.");
                await CurrencyBroker.StartTransactionAsync();
                var deletedCurrency = await CurrencyBroker.DeleteCurrencyAsync(currency);
                await CurrencyBroker.CommitTransactionAsync();
                return deletedCurrency;
            }
            catch
            {
                await CurrencyBroker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await CurrencyBroker.EndConnectionAsync();
            }
        }
        public async Task<Currency> UpdateCurrencyAsync(int currencyId, string name, string code)
        {
            try
            {
                await CurrencyBroker.StartConnectionAsync();
                var currency = await CurrencyBroker.GetCurrencyByIdAsync(new Currency() { Id = currencyId });
                if (currency == null)
                    throw new Exception($"Валута са идентификатором: '{currencyId}' није пронађена.");
                currency.Name = name;
                currency.CurrencyCode = code;

                var resultInfo = new CurrencyValidator(currency).Validate();
                if (resultInfo.IsValid == false)
                    throw new Exception(resultInfo.GetErrorsString());

                await CurrencyBroker.StartTransactionAsync();
                var updatedCurrency = await CurrencyBroker.UpdateCurrencyByIdAsync(currency);
                await CurrencyBroker.CommitTransactionAsync();
                return updatedCurrency;
            }
            catch
            {
                await CurrencyBroker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await CurrencyBroker.EndConnectionAsync();
            }
        }
    }
}