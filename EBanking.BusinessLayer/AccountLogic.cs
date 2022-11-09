using EBanking.BusinessLayer.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using System.Net.WebSockets;

namespace EBanking.BusinessLayer
{
    public class AccountLogic : IAccountLogic
    {
        IBroker Broker { get;}
        public AccountLogic(IBroker broker)
        {
            Broker = broker;
        }
        public async Task<Account> AddAccountAsync(int userId, int currencyId)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var user = await Broker.GetUserByIdAsync(new User() { Id = userId }); ;

                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");

                var currency = await Broker.GetCurrencyByIdAsync(new Currency() { Id = currencyId });

                if (currency == null)
                    throw new Exception($"Валута са идентификатором: '{currencyId}' није пронађен.");

                var userAccounts = await Broker.GetAccountsByUserAsync(user);

                if (userAccounts.Count >= 3)
                    throw new Exception("Корисник не сме имати више од три рачуна.");

                var newAccount = new Account()
                {
                    Balance = 0,
                    Currency = currency,
                    User = user,
                    Number = Account.GenerateAccountNumber(),
                    Status = AccountStatus.ACTIVE
                };

                //VALIDIRATI!!!!!

                await Broker.StartTransactionAsync();
                var accountFromDB = await Broker.CreateAccountAsync(newAccount);
                await Broker.CommitTransactionAsync();
                return accountFromDB;
            }
            catch
            {
                await Broker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }

        public async Task<Account> FindAccountAsync(int accountId)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var account = await Broker.GetAccountByIdAsync(new Account() { Id = accountId });
                if (account == null)
                    throw new Exception($"Рачун са идентификатором: '{accountId}' није пронађен.");
                return account;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
        public async Task<List<Account>> GetAccountsByUser(int userId)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var user = await Broker.GetUserByIdAsync(new User() { Id = userId });
                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");
                var accounts = await Broker.GetAccountsByUserAsync(user);
                return accounts;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            try
            {
                await Broker.StartConnectionAsync();
                return await Broker.GetAllAccountsAsync(new Account());
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
        public async Task<Account> RemoveAccountAsync(int accountId)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var account = new Account() { Id = accountId };
                var accountTransactions = await Broker.GetTransactionsByAccountAsync(account);
                if (accountTransactions != null && accountTransactions.Count > 0)
                    throw new Exception("Не сме се обрисати рачун који има историју трансакција.");
                await Broker.StartTransactionAsync();
                var deletedAccount = await Broker.DeleteAccountAsync(account);
                await Broker.CommitTransactionAsync();
                return deletedAccount;
            }
            catch
            {
                await Broker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }

        public async Task<Account> UpdateAccountAsync(int accountId, AccountStatus status)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var account = await Broker.GetAccountByIdAsync(new Account() { Id = accountId });
                if(account == null)
                    throw new Exception($"Рачун са идентификатором: '{accountId}' није пронађен.");
                account.Status = status;
                await Broker.StartTransactionAsync();
                var updatedAccount = await Broker.UpdateAccountByIdAsync(account);
                await Broker.CommitTransactionAsync();
                return updatedAccount;
            }
            catch
            {
                await Broker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
        public async Task<List<Transaction>> GetTransactionsByAccount(int accountId)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var account = await Broker.GetAccountByIdAsync(new Account() { Id = accountId });
                if (account == null)
                    throw new Exception($"Рачун са идентификатором: '{accountId}' није пронађен.");
                var transactions = await Broker.GetTransactionsByAccountAsync(account);
                return transactions;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
    }
}