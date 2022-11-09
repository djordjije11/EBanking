using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models.Helper;
using EBanking.Models;

namespace EBanking.SqlDataAccess.SqlBrokers
{

    public class SqlTransactionBroker : SqlEntityBroker, ITransactionBroker
    {
        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            return (Transaction)(await CreateEntityAsync(transaction));
        }
        public async Task<Transaction> UpdateTransactionByIdAsync(Transaction transaction)
        {
            return (Transaction)(await UpdateEntityByIdAsync(transaction));
        }
        public async Task<Transaction> DeleteTransactionAsync(Transaction transaction)
        {
            return (Transaction)(await DeleteEntityAsync(transaction));
        }
        public async Task<Transaction?> GetTransactionByIdAsync(Transaction transaction)
        {
            return (await GetEntityByIdAsync(transaction)) as Transaction;
        }
        public async Task<List<Transaction>> GetAllTransactionsAsync(Transaction transaction)
        {
            return EntitiesConverter<Transaction>.ConvertList(await GetAllEntitiesAsync(transaction));
        }
    }
}
