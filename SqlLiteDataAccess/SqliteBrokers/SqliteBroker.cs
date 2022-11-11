using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using SqliteDataAccess.SqliteConnectors;

namespace SqliteDataAccess.SqliteBrokers
{
    public class SqliteBroker : IBroker
    {
        private readonly SqliteConnector connector = SqliteConnector.GetInstance();
        public async Task StartConnectionAsync()
        {
            await connector.StartConnectionAsync();
        }
        public async Task StartTransactionAsync()
        {
            await connector.StartTransactionAsync();
        }
        public void StartCommand()
        {
            connector.StartCommand();
        }
        public async Task CommitTransactionAsync()
        {
            await connector.CommitTransactionAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await connector.RollbackTransactionAsync();
        }
        public async Task EndConnectionAsync()
        {
            await connector.EndConnectionAsync();
        }
        public bool IsConnected() => connector.IsConnected();
        public string GetBrokerName()
        {
            return "SQLite";
        }
        public async Task<Account> CreateAccountAsync(Account account)
        {
            return await new SqliteAccountBroker().CreateAccountAsync(account);
        }

        public async Task<Currency> CreateCurrencyAsync(Currency currency)
        {
            return await new SqliteCurrencyBroker().CreateCurrencyAsync(currency);
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            return await new SqliteTransactionBroker().CreateTransactionAsync(transaction);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await new SqliteUserBroker().CreateUserAsync(user);
        }

        public async Task<Account> DeleteAccountAsync(Account account)
        {
            return await new SqliteAccountBroker().DeleteAccountAsync(account);
        }

        public async Task<Currency> DeleteCurrencyAsync(Currency currency)
        {
            return await new SqliteCurrencyBroker().DeleteCurrencyAsync(currency);
        }

        public async Task<Transaction> DeleteTransactionAsync(Transaction transaction)
        {
            return await new SqliteTransactionBroker().DeleteTransactionAsync(transaction);
        }

        public async Task<User> DeleteUserAsync(User user)
        {
            return await new SqliteUserBroker().DeleteUserAsync(user);
        }

        public async Task<Account?> GetAccountByIdAsync(Account account)
        {
            return await new SqliteAccountBroker().GetAccountByIdAsync(account);
        }

        public async Task<List<Account>> GetAccountsByUserAsync(User user)
        {
            return await new SqliteUserBroker().GetAccountsByUserAsync(user);
        }

        public async Task<List<Account>> GetAllAccountsAsync(Account account)
        {
            return await new SqliteAccountBroker().GetAllAccountsAsync(account);
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync(Currency currency)
        {
            return await new SqliteCurrencyBroker().GetAllCurrenciesAsync(currency);
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync(Transaction transaction)
        {
            return await new SqliteTransactionBroker().GetAllTransactionsAsync(transaction);
        }

        public async Task<List<User>> GetAllUsersAsync(User user)
        {
            return await new SqliteUserBroker().GetAllUsersAsync(user);
        }

        public async Task<Currency?> GetCurrencyByIdAsync(Currency currency)
        {
            return await new SqliteCurrencyBroker().GetCurrencyByIdAsync(currency);
        }

        public async Task<Transaction?> GetTransactionByIdAsync(Transaction transaction)
        {
            return await new SqliteTransactionBroker().GetTransactionByIdAsync(transaction);
        }

        public async Task<List<Transaction>> GetTransactionsByAccountAsync(Account account)
        {
            return await new SqliteAccountBroker().GetTransactionsByAccountAsync(account);
        }

        public async Task<User?> GetUserByIdAsync(User user)
        {
            return await new SqliteUserBroker().GetUserByIdAsync(user);
        }

        public async Task<Account> UpdateAccountByIdAsync(Account account)
        {
            return await new SqliteAccountBroker().UpdateAccountByIdAsync(account);
        }

        public async Task<Currency> UpdateCurrencyByIdAsync(Currency currency)
        {
            return await new SqliteCurrencyBroker().UpdateCurrencyByIdAsync(currency);
        }

        public async Task<Transaction> UpdateTransactionByIdAsync(Transaction transaction)
        {
            return await new SqliteTransactionBroker().UpdateTransactionByIdAsync(transaction);
        }

        public async Task<User> UpdateUserByIdAsync(User user)
        {
            return await new SqliteUserBroker().UpdateUserByIdAsync(user);
        }

        public async Task<List<Account>> GetAllAccountsByCurrencyAsync(Currency currency)
        {
            return await new SqliteCurrencyBroker().GetAllAccountsByCurrencyAsync(currency);
        }

        public async Task<Account?> GetAccountByNumber(Account account)
        {
            return await new SqliteAccountBroker().GetAccountByNumber(account);
        }
    }
}
