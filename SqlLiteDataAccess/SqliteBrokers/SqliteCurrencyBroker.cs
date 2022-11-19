using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models.Helper;
using EBanking.Models;
using SqliteDataAccess.SqliteModels;
using System.Data.SQLite;

namespace SqliteDataAccess.SqliteBrokers
{
    public class SqliteCurrencyBroker : SqliteEntityBroker, ICurrencyBroker
    {
        public SqliteCurrencyBroker(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public async Task<Currency> CreateCurrencyAsync(Currency currency)
        {
            return (Currency)(await CreateEntityAsync(new SqliteCurrency(currency)));
        }
        public async Task<Currency> UpdateCurrencyByIdAsync(Currency currency)
        {
            return (Currency)(await UpdateEntityByIdAsync(new SqliteCurrency(currency)));
        }
        public async Task<Currency> DeleteCurrencyAsync(Currency currency)
        {
            return (Currency)(await DeleteEntityAsync(new SqliteCurrency(currency)));
        }
        public async Task<Currency?> GetCurrencyByIdAsync(Currency currency)
        {
            return (await GetEntityByIdAsync(new SqliteCurrency(currency))) as Currency;
        }
        public async Task<List<Currency>> GetAllCurrenciesAsync(Currency currency)
        {
            return EntitiesConverter<Currency>.ConvertList(await GetAllEntitiesAsync(new SqliteCurrency(currency)));
        }
        public async Task<List<Account>> GetAllAccountsByCurrencyAsync(Currency currency)
        {
            SqliteCurrency sqlCurrency = new SqliteCurrency(currency);
            var accounts = new List<Account>();
            connector.StartCommand();
            SQLiteCommand command = connector.GetCommand();
            sqlCurrency.SetSqlSelectAllAccountsByCurrencyCommand(command);
            SQLiteDataReader reader = (SQLiteDataReader)await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                accounts.Add((Account)sqlCurrency.GetEntityFromSqliteReader(reader));
            }
            await reader.CloseAsync();
            return accounts;
        }
    }
}