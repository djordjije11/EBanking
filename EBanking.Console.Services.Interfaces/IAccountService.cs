using EBanking.API.DTO.AccountDtos;
using EBanking.API.DTO.TransactionDtos;
using EBanking.Models;

namespace EBanking.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<GetAccountDto>> GetAllAccountsAsync();
        Task<GetAccountDto> GetAccountAsync(int id);
        Task<GetAccountDto> AddAccountAsync(int userID, int currencyID);
        Task<GetAccountDto> DeleteAccountAsync(int id);
        Task<GetAccountDto> UpdateAccountAsync(int id, AccountStatus accountStatus);
        Task<IEnumerable<TransactionDto>> GetTransactionsFromAccountAsync(int accountID);
    }
}
