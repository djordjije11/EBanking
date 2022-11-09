using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.Models.Helper;
using SqlDataAccesss.SqlModels;
using System.Data.SqlClient;

namespace EBanking.SqlDataAccess.SqlBrokers
{
    public class SqlCurrencyBroker : SqlEntityBroker, ICurrencyBroker
    {
        public async Task<Currency> CreateCurrencyAsync(Currency currency)
        {
            return (Currency)(await CreateEntityAsync(currency));
        }
        public async Task<Currency> UpdateCurrencyByIdAsync(Currency currency)
        {
            return (Currency)(await UpdateEntityByIdAsync(currency));
        }
        public async Task<Currency> DeleteCurrencyAsync(Currency currency)
        {
            return (Currency)(await DeleteEntityAsync(currency));
        }
        public async Task<Currency?> GetCurrencyByIdAsync(Currency currency)
        {
            return (await GetEntityByIdAsync(currency)) as Currency;
        }
        public async Task<List<Currency>> GetAllCurrenciesAsync(Currency currency)
        {
            return EntitiesConverter<Currency>.ConvertList(await GetAllEntitiesAsync(currency));
        }
        public async Task<List<Account>> GetAllAccountsByCurrencyAsync(Currency currency)
        {
            SqlCurrency sqlCurrency = (SqlCurrency)currency;
            var accounts = new List<Account>();
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            sqlCurrency.SetSqlSelectAllAccountsByCurrencyCommand(command);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                accounts.Add((Account)sqlCurrency.GetEntityFromSqlReader(reader));
            }
            await reader.CloseAsync();
            return accounts;
        }
    }
}
