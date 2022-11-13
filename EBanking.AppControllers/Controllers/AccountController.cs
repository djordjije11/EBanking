using EBanking.BusinessLayer;
using EBanking.BusinessLayer.Interfaces;
using EBanking.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.ConsoleForms
{
    public class AccountController
    {
        public AccountController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            
        }
        public IServiceProvider ServiceProvider { get; }
        public async Task<Account> CreateAccountAsnyc(int userId, int currencyId)
        {
            var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
            return await accountLogic.AddAccountAsync(userId, currencyId);
        }
        public async Task<Account> UpdateAccountAsync(int accountId, string accountStatus)
        {
            var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
            return await accountLogic.UpdateAccountAsync(accountId, accountStatus);
        }
        public async Task<Account> DeleteAccountAsync(int accountId)
        {
            var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
            return await accountLogic.RemoveAccountAsync(accountId);
        }
        public async Task<Account> ReadAccountAsync(int accountId)
        {
            var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
            return await accountLogic.FindAccountAsync(accountId);
        }
        public async Task<List<Account>> ReadAllAccountsAsync()
        {
            var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
            return await accountLogic.GetAllAccountsAsync();
        }
        public async Task<List<Transaction>> ReadAllTransactionsOfAccountAsync(int accountId)
        {
            var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
            return await accountLogic.GetTransactionsByAccount(accountId);
        }
    }
}
