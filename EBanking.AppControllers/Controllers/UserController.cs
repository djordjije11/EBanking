using EBanking.BusinessLayer.Interfaces;
using EBanking.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.ConsoleForms
{
    public class UserController
    {
        public UserController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public IServiceProvider ServiceProvider { get; }
        public async Task<User> CreateUserAsync(string firstName, string lastName, string email, string password)
        {
            var userLogic = ServiceProvider.GetRequiredService<IUserLogic>();
            return await userLogic.AddUserAsync(firstName, lastName, email, password);
        }
        public async Task<User> UpdateUserAsync(int userID, string firstName, string lastName, string oldPassword, string newPassword)
        {
            var userLogic = ServiceProvider.GetRequiredService<IUserLogic>();
            return await userLogic.UpdateUserAsync(userID, firstName, lastName, oldPassword, newPassword);
        }
        public async Task<User> DeleteUserAsync(int userID, string password)
        {
            var userLogic = ServiceProvider.GetRequiredService<IUserLogic>();
            return await userLogic.DeleteUserAsync(userID, password);
        }
        public async Task<User> ReadUserAsync(int userID)
        {
            var userLogic = ServiceProvider.GetRequiredService<IUserLogic>();
            return await userLogic.FindUserAsync(userID);
        }
        public async Task<List<User>> ReadAllUsersAsync()
        {
            var userLogic = ServiceProvider.GetRequiredService<IUserLogic>();
            return await userLogic.GetAllUsersAsync();
        }
        public async Task<List<Account>> ReadAllAccountsOfUserAsync(int userID)
        {
            var userLogic = ServiceProvider.GetRequiredService<IUserLogic>();
            return await userLogic.GetAccountsByUserAsync(userID);
        }
    }
}
