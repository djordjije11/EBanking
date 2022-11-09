using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.SqlDataAccess.SqlBrokers;
using EBanking.SqlDataAccess.SqlConnectors;

namespace SqlDataAccesss.SqlBrokers
{
    public class SqlBroker : IBroker
    {
        private readonly SqlConnector connector = SqlConnector.GetInstance();
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
            return "SQL";
        }
        public async Task<Account> CreateAccountAsync(Account account)
        {
            return await new SqlAccountBroker().CreateAccountAsync(account);
        }

        public async Task<Currency> CreateCurrencyAsync(Currency currency)
        {
            return await new SqlCurrencyBroker().CreateCurrencyAsync(currency);
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            return await new SqlTransactionBroker().CreateTransactionAsync(transaction);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await new SqlUserBroker().CreateUserAsync(user);
        }

        public async Task<Account> DeleteAccountAsync(Account account)
        {
            return await new SqlAccountBroker().DeleteAccountAsync(account);
        }

        public async Task<Currency> DeleteCurrencyAsync(Currency currency)
        {
            return await new SqlCurrencyBroker().DeleteCurrencyAsync(currency);
        }

        public async Task<Transaction> DeleteTransactionAsync(Transaction transaction)
        {
            return await new SqlTransactionBroker().DeleteTransactionAsync(transaction);
        }

        public async Task<User> DeleteUserAsync(User user)
        {
            return await new SqlUserBroker().DeleteUserAsync(user);
        }

        public async Task<Account?> GetAccountByIdAsync(Account account)
        {
            return await new SqlAccountBroker().GetAccountByIdAsync(account);
        }

        public async Task<List<Account>> GetAccountsByUserAsync(User user)
        {
            return await new SqlUserBroker().GetAccountsByUserAsync(user);
        }

        public async Task<List<Account>> GetAllAccountsAsync(Account account)
        {
            return await new SqlAccountBroker().GetAllAccountsAsync(account);
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync(Currency currency)
        {
            return await new SqlCurrencyBroker().GetAllCurrenciesAsync(currency);
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync(Transaction transaction)
        {
            return await new SqlTransactionBroker().GetAllTransactionsAsync(transaction);
        }

        public async Task<List<User>> GetAllUsersAsync(User user)
        {
            return await new SqlUserBroker().GetAllUsersAsync(user);
        }

        public async Task<Currency?> GetCurrencyByIdAsync(Currency currency)
        {
            return await new SqlCurrencyBroker().GetCurrencyByIdAsync(currency);
        }

        public async Task<Transaction?> GetTransactionByIdAsync(Transaction transaction)
        {
            return await new SqlTransactionBroker().GetTransactionByIdAsync(transaction);
        }

        public async Task<List<Transaction>> GetTransactionsByAccountAsync(Account account)
        {
            return await new SqlAccountBroker().GetTransactionsByAccountAsync(account); 
        }

        public async Task<User?> GetUserByIdAsync(User user)
        {
            return await new SqlUserBroker().GetUserByIdAsync(user);
        }

        public async Task<Account> UpdateAccountByIdAsync(Account account)
        {
            return await new SqlAccountBroker().UpdateAccountByIdAsync(account);
        }

        public async Task<Currency> UpdateCurrencyByIdAsync(Currency currency)
        {
            return await new SqlCurrencyBroker().UpdateCurrencyByIdAsync(currency);
        }

        public async Task<Transaction> UpdateTransactionByIdAsync(Transaction transaction)
        {
            return await new SqlTransactionBroker().UpdateTransactionByIdAsync(transaction);
        }

        public async Task<User> UpdateUserByIdAsync(User user)
        {
            return await new SqlUserBroker().UpdateUserByIdAsync(user);
        }
    }
}