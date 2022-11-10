using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models.Helper;
using EBanking.Models;
using SqlDataAccesss.SqlModels;

namespace EBanking.SqlDataAccess.SqlBrokers
{

    public class SqlTransactionBroker : SqlEntityBroker, ITransactionBroker
    {
        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            return (Transaction)(await CreateEntityAsync(new SqlTransaction(transaction)));
        }
        public async Task<Transaction> UpdateTransactionByIdAsync(Transaction transaction)
        {
            return (Transaction)(await UpdateEntityByIdAsync(new SqlTransaction(transaction)));
        }
        public async Task<Transaction> DeleteTransactionAsync(Transaction transaction)
        {
            return (Transaction)(await DeleteEntityAsync(new SqlTransaction(transaction)));
        }
        public async Task<Transaction?> GetTransactionByIdAsync(Transaction transaction)
        {
            return (await GetEntityByIdAsync(new SqlTransaction(transaction))) as Transaction;
        }
        public async Task<List<Transaction>> GetAllTransactionsAsync(Transaction transaction)
        {
            return EntitiesConverter<Transaction>.ConvertList(await GetAllEntitiesAsync(new SqlTransaction(transaction)));
        }
    }
}
