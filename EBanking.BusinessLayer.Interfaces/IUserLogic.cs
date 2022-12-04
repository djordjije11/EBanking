using EBanking.Models;

namespace EBanking.BusinessLayer.Interfaces
{
    public interface IUserLogic
    {
        Task<User> AddUserAsync(string firstName, string lastName, string email, string password);
        Task<User> UpdateUserAsync(int userId, string email, string oldPassword, string newPassword);
        Task<User> DeleteUserAsync(int userId, string password);
        Task<User> FindUserAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
        Task<List<Account>> GetAccountsByUserAsync(int userId);
    }
}
