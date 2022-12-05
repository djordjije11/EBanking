using EBanking.ConfigurationManager.Interfaces;
using EBanking.Models;
using EBanking.Services.HttpClients.Helper;
using System.Net.Http.Json;
using EBanking.API.DTO.AccountDtos;

namespace EBanking.Services.HttpClients
{
    public interface IAccountHttpClient
    {
        Task<Account?> DeleteAsync(int id);
        Task<IEnumerable<Account>?> GetAsync();
        Task<Account?> GetAsync(int id);
        Task<IEnumerable<Transaction>?> GetTransactionsAsync(int id);
        Task<Account?> PostAsync(int userID, int currencyID);
        Task<Account?> PutAsync(int id, AccountStatus accountStatus);
    }

    public class AccountHttpClient : IAccountHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly string url;

        public AccountHttpClient(IConfigurationManager manager, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            url = manager.GetConfigParam(ConfigParamKeys.URL_ACCOUNT);
        }
        public async Task<Account?> PostAsync(int userID, int currencyID)
        {
            var response = await httpClient.PostAsJsonAsync(url, new AccountDto() { UserId = userID, CurrencyId = currencyID });
            return await HelperMethods.GetEntityFromHttpResponse<Account>(response);
        }
        public async Task<Account?> GetAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}");
            return await HelperMethods.GetEntityFromHttpResponse<Account>(response);
        }
        public async Task<IEnumerable<Account>?> GetAsync()
        {
            var response = await httpClient.GetAsync(url);
            return await HelperMethods.GetEntitiesFromHttpResponse<Account>(response);
        }
        public async Task<Account?> PutAsync(int id, AccountStatus accountStatus)
        {
            var response = await httpClient.PutAsJsonAsync($"{url}/{id}", new AccountDto() { Status = accountStatus });
            return await HelperMethods.GetEntityFromHttpResponse<Account>(response);
        }
        public async Task<Account?> DeleteAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"{url}/{id}");
            return await HelperMethods.GetEntityFromHttpResponse<Account>(response);
        }
        public async Task<IEnumerable<Transaction>?> GetTransactionsAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}/Transactions");
            return await HelperMethods.GetEntitiesFromHttpResponse<Transaction>(response);
        }
    }
}
