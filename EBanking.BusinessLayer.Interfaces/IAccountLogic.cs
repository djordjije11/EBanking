using EBanking.Models;

namespace EBanking.BusinessLayer.Interfaces
{
    public interface IAccountLogic
    {
        Task<Account> AddAccountAsync(int userId, int currencyId);
        Task<Account> UpdateAccountAsync(int accountId, string status);
        Task<Account> RemoveAccountAsync(int accountId);
        Task<Account> FindAccountAsync(int accountId);
        Task<List<Account>> GetAllAccountsAsync();
        Task<List<Transaction>> GetTransactionsByAccount(int accountId);
    }
}
