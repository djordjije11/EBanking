using EBanking.ConfigurationManager.Interfaces;
using EBanking.Console.Common;
using EBanking.Models;
using EBanking.Models.ModelsDto;
using System.Net.Http.Json;

namespace EBanking.Console.HttpClients
{
    public interface ITransactionHttpClient
    {
        Task<IEnumerable<Transaction>?> GetAsync();
        Task<Transaction?> GetAsync(int id);
        Task<Transaction?> PostAsync(decimal amount, string fromAccountNumber, string toAccountNumber);
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
        public async Task<Transaction?> PostAsync(decimal amount, string fromAccountNumber, string toAccountNumber)
        {
            var response = await httpClient.PostAsJsonAsync(url, new TransactionDto() { Amount = amount, FromAccountNumber = fromAccountNumber, ToAccountNumber = toAccountNumber });
            return await HelperMethods.GetEntityFromHttpResponse<Transaction>(response);
        }
        public async Task<Transaction?> GetAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}");
            return await HelperMethods.GetEntityFromHttpResponse<Transaction>(response);
        }
        public async Task<IEnumerable<Transaction>?> GetAsync()
        {
            var response = await httpClient.GetAsync(url);
            return await HelperMethods.GetEntitiesFromHttpResponse<Transaction>(response);
        }
    }
}
