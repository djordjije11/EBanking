using EBanking.ConfigurationManager.Interfaces;
using EBanking.Console.Common;
using EBanking.Models;
using System.Net.Http.Json;

namespace EBanking.Console.HttpClients
{
    public interface ICurrencyHttpClient
    {
        Task<Currency?> DeleteAsync(int id);
        Task<IEnumerable<Currency>?> GetAsync();
        Task<Currency?> GetAsync(int id);
        Task<Currency?> PostAsync(string name, string code);
        Task<Currency?> PutAsync(int id, string name, string code);
    }

    public class CurrencyHttpClient : ICurrencyHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly string url;

        public CurrencyHttpClient(IConfigurationManager manager, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            url = manager.GetConfigParam(ConfigParamKeys.URL_CURRENCY);
        }
        public async Task<Currency?> PostAsync(string name, string code)
        {
            var response = await httpClient.PostAsJsonAsync(url, new Currency() { Name = name, CurrencyCode = code });
            return await HelperMethods.GetEntityFromHttpResponse<Currency>(response);
        }
        public async Task<Currency?> GetAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}");
            return await HelperMethods.GetEntityFromHttpResponse<Currency>(response);
        }
        public async Task<IEnumerable<Currency>?> GetAsync()
        {
            var response = await httpClient.GetAsync(url);
            return await HelperMethods.GetEntitiesFromHttpResponse<Currency>(response);
        }
        public async Task<Currency?> PutAsync(int id, string name, string code)
        {
            var response = await httpClient.PutAsJsonAsync($"{url}/{id}", new Currency() { Name = name, CurrencyCode = code });
            return await HelperMethods.GetEntityFromHttpResponse<Currency>(response);
        }
        public async Task<Currency?> DeleteAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"{url}/{id}");
            return await HelperMethods.GetEntityFromHttpResponse<Currency>(response);
        }
    }
}
