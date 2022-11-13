using EBanking.Models;

namespace EBanking.DataAccessLayer.Interfaces
{
    public interface IAccountBroker : IBroker
    {
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountByIdAsync(Account account);
        Task<Account> DeleteAccountAsync(Account account);
        Task<Account?> GetAccountByIdAsync(Account account);
        Task<List<Account>> GetAllAccountsAsync(Account account);
        Task<Account?> GetAccountByNumber(Account account);
        Task<List<Transaction>> GetTransactionsByAccountAsync(Account account);
    }
}
