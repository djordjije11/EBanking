using EBanking.ConfigurationManager.Interfaces;
using EBanking.Models;
using EBanking.Services.HttpClients.Helper;
using System.Net.Http.Json;
using EBanking.API.DTO.AccountDtos;
using EBanking.API.DTO.TransactionDtos;

namespace EBanking.Services.HttpClients
{
    public interface IAccountHttpClient
    {
        Task<GetAccountDto?> DeleteAsync(int id);
        Task<IEnumerable<GetAccountDto>?> GetAsync();
        Task<GetAccountDto?> GetAsync(int id);
        Task<IEnumerable<TransactionDto>?> GetTransactionsAsync(int id);
        Task<GetAccountDto?> PostAsync(int userID, int currencyID);
        Task<GetAccountDto?> PutAsync(int id, AccountStatus accountStatus);
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
        public async Task<GetAccountDto?> PostAsync(int userID, int currencyID)
        {
            var response = await httpClient.PostAsJsonAsync(url, new AddAccountDto() { UserId = userID, CurrencyId = currencyID });
            return await HelperMethods.GetEntityFromHttpResponse<GetAccountDto>(response);
        }
        public async Task<GetAccountDto?> GetAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}");
            return await HelperMethods.GetEntityFromHttpResponse<GetAccountDto>(response);
        }
        public async Task<IEnumerable<GetAccountDto>?> GetAsync()
        {
            var response = await httpClient.GetAsync(url);
            return await HelperMethods.GetEntitiesFromHttpResponse<GetAccountDto>(response);
        }
        public async Task<GetAccountDto?> PutAsync(int id, AccountStatus accountStatus)
        {
            var response = await httpClient.PutAsJsonAsync($"{url}/{id}", new UpdateAccountDto() { Status = accountStatus });
            return await HelperMethods.GetEntityFromHttpResponse<GetAccountDto>(response);
        }
        public async Task<GetAccountDto?> DeleteAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"{url}/{id}");
            return await HelperMethods.GetEntityFromHttpResponse<GetAccountDto>(response);
        }
        public async Task<IEnumerable<TransactionDto>?> GetTransactionsAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}/Transactions");
            return await HelperMethods.GetEntitiesFromHttpResponse<TransactionDto>(response);
        }
    }
}
