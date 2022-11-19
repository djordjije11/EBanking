using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.Models.Helper;
using SqliteDataAccess.SqliteModels;

namespace SqliteDataAccess.SqliteBrokers
{
    public class SqliteTransactionBroker : SqliteEntityBroker, ITransactionBroker
    {
        public SqliteTransactionBroker(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            return (Transaction)(await CreateEntityAsync(new SqliteTransaction(transaction)));
        }
        public async Task<Transaction> UpdateTransactionByIdAsync(Transaction transaction)
        {
            return (Transaction)(await UpdateEntityByIdAsync(new SqliteTransaction(transaction)));
        }
        public async Task<Transaction> DeleteTransactionAsync(Transaction transaction)
        {
            return (Transaction)(await DeleteEntityAsync(new SqliteTransaction(transaction)));
        }
        public async Task<Transaction?> GetTransactionByIdAsync(Transaction transaction)
        {
            return (await GetEntityByIdAsync(new SqliteTransaction(transaction))) as Transaction;
        }
        public async Task<List<Transaction>> GetAllTransactionsAsync(Transaction transaction)
        {
            return EntitiesConverter<Transaction>.ConvertList(await GetAllEntitiesAsync(new SqliteTransaction(transaction)));
        }
    }
}
