using EBanking.ConfigurationManager.Interfaces;
using EBanking.Console.Common;
using EBanking.Models;
using EBanking.Models.ModelsDto;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace EBanking.Console.HttpClients
{
    public interface IUserHttpClient
    {
        Task<User?> PostAsync(string firstName, string lastName, string email, string password);
        Task<User?> GetAsync(int id);
        Task<User?> PutAsync(int id, string email, string oldPassword, string newPassword);
        Task<IEnumerable<User>?> GetAsync();
        Task<User?> DeleteAsync(int id, string password);
        Task<IEnumerable<Account>?> GetAccountsAsync(int id);

    }

    public class UserHttpClient : IUserHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly string url;

        public UserHttpClient(IConfigurationManager manager, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            url = manager.GetConfigParam(ConfigParamKeys.URL_USER);
        }

        public async Task<User?> PostAsync(string firstName, string lastName, string email, string password)
        {
            var response = await httpClient.PostAsJsonAsync(url, new User() { FirstName = firstName, LastName = lastName, Email = email, Password = password });
            return await HelperMethods.GetEntityFromHttpResponse<User>(response);
        }
        public async Task<User?> GetAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}");
            return await HelperMethods.GetEntityFromHttpResponse<User>(response);
        }
        public async Task<IEnumerable<User>?> GetAsync()
        {
            var response = await httpClient.GetAsync(url);
            return await HelperMethods.GetEntitiesFromHttpResponse<User>(response);
        }
        public async Task<User?> PutAsync(int id, string email, string oldPassword, string newPassword)
        {
            var response = await httpClient.PutAsJsonAsync($"{url}/{id}", new UserDto() { Email = email, Password = newPassword, OldPassword = oldPassword });
            return await HelperMethods.GetEntityFromHttpResponse<User>(response);
        }
        public async Task<User?> DeleteAsync(int id, string password)
        {
            //new Dictionary<string, string>() { { "Password", password } }
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{url}/{id}"),
                Content = new StringContent(JsonConvert.SerializeObject(new UserDto() { Password = password }), Encoding.UTF8, "application/json")
            };
            var response = await httpClient.SendAsync(request);
            return await HelperMethods.GetEntityFromHttpResponse<User>(response);
        }
        public async Task<IEnumerable<Account>?> GetAccountsAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}/Accounts");
            return await HelperMethods.GetEntitiesFromHttpResponse<Account>(response);
        }
    }
}
