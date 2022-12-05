using EBanking.ConfigurationManager.Interfaces;
using EBanking.API.DTO.UserDtos;
using Newtonsoft.Json;
using System.Net.Http.Json;
using EBanking.Services.HttpClients.Helper;
using System.Text;
using EBanking.API.DTO.AccountDtos;

namespace EBanking.Services.HttpClients
{
    public interface IUserHttpClient
    {
        Task<GetUserDto?> PostAsync(string firstName, string lastName, string email, string password);
        Task<GetUserDto?> GetAsync(int id);
        Task<GetUserDto?> PutAsync(int id, string email, string oldPassword, string newPassword);
        Task<IEnumerable<GetUserDto>?> GetAsync();
        Task<GetUserDto?> DeleteAsync(int id, string password);
        Task<IEnumerable<GetAccountDto>?> GetAccountsAsync(int id);
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

        public async Task<GetUserDto?> PostAsync(string firstName, string lastName, string email, string password)
        {
            var response = await httpClient.PostAsJsonAsync(url, new AddUserDto() { FirstName = firstName, LastName = lastName, Email = email, Password = password });
            return await HelperMethods.GetEntityFromHttpResponse<GetUserDto>(response);
        }
        public async Task<GetUserDto?> GetAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}");
            return await HelperMethods.GetEntityFromHttpResponse<GetUserDto>(response);
        }
        public async Task<IEnumerable<GetUserDto>?> GetAsync()
        {
            var response = await httpClient.GetAsync(url);
            return await HelperMethods.GetEntitiesFromHttpResponse<GetUserDto>(response);
        }
        public async Task<GetUserDto?> PutAsync(int id, string email, string oldPassword, string newPassword)
        {
            var response = await httpClient.PutAsJsonAsync($"{url}/{id}", new UpdateUserDto() { Email = email, OldPassword = oldPassword, NewPassword = newPassword });
            return await HelperMethods.GetEntityFromHttpResponse<GetUserDto>(response);
        }
        public async Task<GetUserDto?> DeleteAsync(int id, string password)
        {
            //new Dictionary<string, string>() { { "Password", password } }
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{url}/{id}"),
                Content = new StringContent(JsonConvert.SerializeObject(new DeleteUserDto() { Password = password }), Encoding.UTF8, "application/json")
            };
            var response = await httpClient.SendAsync(request);
            return await HelperMethods.GetEntityFromHttpResponse<GetUserDto>(response);
        }
        public async Task<IEnumerable<GetAccountDto>?> GetAccountsAsync(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}/Accounts");
            return await HelperMethods.GetEntitiesFromHttpResponse<GetAccountDto>(response);
        }
    }
}
