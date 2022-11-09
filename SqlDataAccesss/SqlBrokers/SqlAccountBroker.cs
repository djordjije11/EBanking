using EBanking.Models;
using SqlDataAccesss.SqlModels;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models.Helper;

namespace EBanking.SqlDataAccess.SqlBrokers
{
    public class SqlAccountBroker : SqlEntityBroker, IAccountBroker
    {
        public async Task<Account> CreateAccountAsync(Account account)
        {
            return (Account)(await CreateEntityAsync(account));
        }
        public async Task<Account> UpdateAccountByIdAsync(Account account)
        {
            return (Account)(await UpdateEntityByIdAsync(account));
        }
        public async Task<Account> DeleteAccountAsync(Account account)
        {
            return (Account)(await DeleteEntityAsync(account));
        }

        public async Task<Account?> GetAccountByIdAsync(Account account)
        {
            return (await GetEntityByIdAsync(account)) as Account;
        }

        public async Task<List<Account>> GetAllAccountsAsync(Account account)
        {
            return EntitiesConverter<Account>.ConvertList(await GetAllEntitiesAsync(account));
        }

        public async Task<List<Transaction>> GetTransactionsByAccountAsync(Account account)
        {
            SqlAccount sqlAccount = (SqlAccount)account;
            SqlTransaction sqlTransaction = new SqlTransaction();
            var transactions = new List<Transaction>();
            connector.StartCommand();
            var command = connector.GetCommand();
            sqlAccount.SetSqlSelectAllTransactionsByAccountId(command);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                transactions.Add((Transaction)sqlTransaction.GetEntityFromSqlReader(reader));
            }
            await reader.CloseAsync();
            return transactions;
        }
    }
}
