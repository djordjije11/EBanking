using EBanking.BusinessLayer.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.Validation.Validators;

namespace EBanking.BusinessLayer
{
    public class AccountLogic : IAccountLogic
    {
        IAccountBroker AccountBroker { get; }
        IUserBroker UserBroker { get; }
        ICurrencyBroker CurrencyBroker { get; }
        public AccountLogic(IAccountBroker accountBroker, IUserBroker userBroker, ICurrencyBroker currencyBroker)
        {
            AccountBroker = accountBroker;
            UserBroker = userBroker;
            CurrencyBroker = currencyBroker;
        }
        public async Task<Account> AddAccountAsync(int userId, int currencyId)
        {
            try
            {
                await AccountBroker.StartConnectionAsync();
                var user = await UserBroker.GetUserByIdAsync(new User() { Id = userId});

                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");

                var currency = await CurrencyBroker.GetCurrencyByIdAsync(new Currency() { Id = currencyId});

                if (currency == null)
                    throw new Exception($"Валута са идентификатором: '{currencyId}' није пронађен.");

                var userAccounts = await UserBroker.GetAccountsByUserAsync(user);

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

                var resultInfo = new AccountValidator(newAccount).Validate();
                if (resultInfo.IsValid == false)
                    throw new Exception(resultInfo.GetErrorsString());

                await AccountBroker.StartTransactionAsync();
                var accountFromDB = await AccountBroker.CreateAccountAsync(newAccount);
                await AccountBroker.CommitTransactionAsync();
                return accountFromDB;
            }
            catch
            {
                await AccountBroker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await AccountBroker.EndConnectionAsync();
            }
        }
        public async Task<Account> FindAccountAsync(int accountId)
        {
            try
            {
                await AccountBroker.StartConnectionAsync();
                var account = await AccountBroker.GetAccountByIdAsync(new Account() { Id = accountId });
                if (account == null)
                    throw new Exception($"Рачун са идентификатором: '{accountId}' није пронађен.");
                return account;
            }
            finally
            {
                await AccountBroker.EndConnectionAsync();
            }
        }
        public async Task<List<Account>> GetAccountsByUser(int userId)
        {
            try
            {
                await AccountBroker.StartConnectionAsync();
                var user = await UserBroker.GetUserByIdAsync(new User() { Id = userId });
                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");
                var accounts = await UserBroker.GetAccountsByUserAsync(user);
                return accounts;
            }
            finally
            {
                await AccountBroker.EndConnectionAsync();
            }
        }
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            try
            {
                await AccountBroker.StartConnectionAsync();
                return await AccountBroker.GetAllAccountsAsync(new Account());
            }
            finally
            {
                await AccountBroker.EndConnectionAsync();
            }
        }
        public async Task<Account> RemoveAccountAsync(int accountId)
        {
            try
            {
                await AccountBroker.StartConnectionAsync();
                var account = new Account() { Id = accountId };
                var accountTransactions = await AccountBroker.GetTransactionsByAccountAsync(account);
                if (accountTransactions != null && accountTransactions.Count > 0)
                    throw new Exception("Не сме се обрисати рачун који има историју трансакција.");
                await AccountBroker.StartTransactionAsync();
                var deletedAccount = await AccountBroker.DeleteAccountAsync(account);
                await AccountBroker.CommitTransactionAsync();
                return deletedAccount;
            }
            catch
            {
                await AccountBroker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await AccountBroker.EndConnectionAsync();
            }
        }
        public async Task<Account> UpdateAccountAsync(int accountId, AccountStatus status)
        {
            try
            {
                await AccountBroker.StartConnectionAsync();
                var account = await AccountBroker.GetAccountByIdAsync(new Account() { Id = accountId });
                if(account == null)
                    throw new Exception($"Рачун са идентификатором: '{accountId}' није пронађен.");
                account.Status = status;
                var resultInfo = new AccountValidator(account).Validate();
                if (resultInfo.IsValid == false)
                    throw new Exception(resultInfo.GetErrorsString());
                await AccountBroker.StartTransactionAsync();
                var updatedAccount = await AccountBroker.UpdateAccountByIdAsync(account);
                await AccountBroker.CommitTransactionAsync();
                return updatedAccount;
            }
            catch
            {
                await AccountBroker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await AccountBroker.EndConnectionAsync();
            }
        }
        public async Task<List<Transaction>> GetTransactionsByAccount(int accountId)
        {
            try
            {
                await AccountBroker.StartConnectionAsync();
                var account = await AccountBroker.GetAccountByIdAsync(new Account() { Id = accountId });
                if (account == null)
                    throw new Exception($"Рачун са идентификатором: '{accountId}' није пронађен.");
                var transactions = await AccountBroker.GetTransactionsByAccountAsync(account);
                return transactions;
            }
            finally
            {
                await AccountBroker.EndConnectionAsync();
            }
        }
        
    }
}