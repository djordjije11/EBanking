using EBanking.Models.Helper;
using EBanking.Models;
using SqliteDataAccess.SqliteModels;
using EBanking.DataAccessLayer.Interfaces;
using System.Data.SQLite;

namespace SqliteDataAccess.SqliteBrokers
{
    public class SqliteAccountBroker : SqliteEntityBroker, IAccountBroker
    {
        public async Task<Account> CreateAccountAsync(Account account)
        {
            return (Account)(await CreateEntityAsync(new SqliteAccount(account)));
        }
        public async Task<Account> UpdateAccountByIdAsync(Account account)
        {
            return (Account)(await UpdateEntityByIdAsync(new SqliteAccount(account)));
        }
        public async Task<Account> DeleteAccountAsync(Account account)
        {
            return (Account)(await DeleteEntityAsync(new SqliteAccount(account)));
        }
        public async Task<Account?> GetAccountByIdAsync(Account account)
        {
            return (await GetEntityByIdAsync(new SqliteAccount(account))) as Account;
        }
        public async Task<List<Account>> GetAllAccountsAsync(Account account)
        {
            return EntitiesConverter<Account>.ConvertList(await GetAllEntitiesAsync(new SqliteAccount(account)));
        }
        public async Task<List<Transaction>> GetTransactionsByAccountAsync(Account account)
        {
            SqliteAccount sqlAccount = new SqliteAccount(account);
            SqliteTransaction sqlTransaction = new SqliteTransaction();
            var transactions = new List<Transaction>();
            connector.StartCommand();
            var command = connector.GetCommand();
            sqlAccount.SetSqliteSelectAllTransactionsByAccountId(command);
            SQLiteDataReader reader = (SQLiteDataReader)await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                transactions.Add((Transaction)sqlTransaction.GetEntityFromSqliteReader(reader));
            }
            await reader.CloseAsync();
            return transactions;
        }
        public async Task<Account?> GetAccountByNumber(Account account)
        {
            SqliteAccount sqlAccount = new SqliteAccount(account);
            connector.StartCommand();
            var command = connector.GetCommand();
            sqlAccount.SetSqliteSelectAccountByNumber(command);
            SQLiteDataReader reader = (SQLiteDataReader)await command.ExecuteReaderAsync();
            Account? wantedAccount = null;
            if (await reader.ReadAsync())
            {
                wantedAccount = (Account)sqlAccount.GetEntityFromSqliteReader(reader);
            }
            await reader.CloseAsync();
            return wantedAccount;
        }
    }
}
