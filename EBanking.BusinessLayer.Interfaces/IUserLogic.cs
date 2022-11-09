

using EBanking.Models;

namespace EBanking.BusinessLayer.Interfaces
{
    public interface IUserLogic
    {
        Task<User> AddUserAsync(string firstName, string lastName, string email, string password);
        Task<User> UpdateUserAsync(int userId, string firstName, string lastName, string password);
        Task<User> DeleteUserAsync(int userId, string password);
        Task<User> FindUserAsync(int userId);
        Task<User> GetAllUsersAsync();
        Task<List<Account>> GetAccountsByUser(int userId);
    }
}
