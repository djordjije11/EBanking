using EBanking.ConfigurationManager.Interfaces;
using EBanking.Services.HttpClients.Helper;
using System.Net.Http.Json;
using EBanking.API.DTO.TransactionDtos;

namespace EBanking.Services.HttpClients
{
    public interface ITransactionHttpClient
    {
        Task<IEnumerable<TransactionDto>?> GetAsync();
        Task<TransactionDto?> GetAsync(int id);
        Task<TransactionDto?> PostAsync(decimal amount, string fromAccountNumber, string toAccountNumber);
    }

    public class TransactionHttpClient : ITransactionHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly string url;

        public TransactionHttpClient(IConfigurationManager manager, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            url = manager.GetConfigParam(ConfigParamKeys.URL_TRANSACTION);
        }
        public async Task<TransactionDto?> PostAsync(decimal amount, string fromAccountNumber, string toAccountNumber)
        {
            var response = await httpClient.PostAsJsonAsync(url, new TransactionDto() { Amount = amount, FromAccountNumber = fromAccountNumber, ToAccountNumber = toAccountNumber });
            return await HelperMethods.GetEntityFromHttpResponse<TransactionDto>(response);
        }
        public async Task<TransactionDto?> GetAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}");
            return await HelperMethods.GetEntityFromHttpResponse<TransactionDto>(response);
        }
        public async Task<IEnumerable<TransactionDto>?> GetAsync()
        {
            var response = await httpClient.GetAsync(url);
            return await HelperMethods.GetEntitiesFromHttpResponse<TransactionDto>(response);
        }
    }
}
