using EBanking.API.DTO.UserDtos;
using EBanking.Services.HttpClients;
using EBanking.Services.Interfaces;
using EBanking.API.DTO.AccountDtos;

namespace EBanking.Services.APIServices
{
    public class UserAPIService : IUserService
    {
        private readonly IUserHttpClient userHttpClient;

        public UserAPIService(IUserHttpClient userHttpClient)
        {
            this.userHttpClient = userHttpClient;
        }

        public async Task<GetUserDto?> AddUserAsync(string firstName, string lastName, string email, string password)
        {
            return await userHttpClient.PostAsync(firstName, lastName, email, password);
        }

        public async Task<GetUserDto?> DeleteUserAsync(int id, string password)
        {
            return await userHttpClient.DeleteAsync(id, password);
        }

        public async Task<IEnumerable<GetAccountDto>?> GetAccountsFromUserAsync(int userID)
        {
            return await userHttpClient.GetAccountsAsync(userID);
        }

        public async Task<IEnumerable<GetUserDto>?> GetAllUsersAsync()
        {
            return await userHttpClient.GetAsync();
        }

        public async Task<GetUserDto?> GetUserAsync(int id)
        {
            return await userHttpClient.GetAsync(id);
        }

        public async Task<GetUserDto?> UpdateUserAsync(int id, string email, string oldPassword, string newPassword)
        {
            return await userHttpClient.PutAsync(id, email, oldPassword, newPassword);
        }
    }
}