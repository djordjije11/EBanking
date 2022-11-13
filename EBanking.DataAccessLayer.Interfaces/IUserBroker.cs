using EBanking.Models;

namespace EBanking.DataAccessLayer.Interfaces
{
    public interface IUserBroker : IBroker
    {
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserByIdAsync(User user);
        Task<User> DeleteUserAsync(User user);
        Task<User?> GetUserByIdAsync(User user);
        Task<List<User>> GetAllUsersAsync(User user);
        Task<List<Account>> GetAccountsByUserAsync(User user);
    }
}
