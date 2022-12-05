using EBanking.API.DTO.AccountDtos;
using EBanking.API.DTO.TransactionDtos;
using EBanking.Models;
using EBanking.Services.HttpClients;
using EBanking.Services.Interfaces;

namespace EBanking.Services.APIServices
{
    public class AccountAPIService : IAccountService
    {
        private readonly IAccountHttpClient accountHttpClient;

        public AccountAPIService(IAccountHttpClient accountHttpClient)
        {
            this.accountHttpClient = accountHttpClient;
        }

        public async Task<GetAccountDto?> AddAccountAsync(int userID, int currencyID)
        {
            return await accountHttpClient.PostAsync(userID, currencyID);
        }

        public async Task<GetAccountDto?> DeleteAccountAsync(int id)
        {
            return await accountHttpClient.DeleteAsync(id);
        }

        public async Task<GetAccountDto?> GetAccountAsync(int id)
        {
            return await accountHttpClient.GetAsync(id);
        }

        public async Task<IEnumerable<GetAccountDto>?> GetAllAccountsAsync()
        {
            return await accountHttpClient.GetAsync();
        }

        public async Task<IEnumerable<TransactionDto>?> GetTransactionsFromAccountAsync(int accountID)
        {
            return await accountHttpClient.GetTransactionsAsync(accountID);
        }

        public Task<GetAccountDto?> UpdateAccountAsync(int id, AccountStatus accountStatus)
        {
            throw new NotImplementedException();
        }
    }
}
