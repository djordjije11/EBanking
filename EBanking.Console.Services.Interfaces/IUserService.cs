using EBanking.API.DTO.AccountDtos;
using EBanking.API.DTO.UserDtos;

namespace EBanking.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDto>?> GetAllUsersAsync();
        Task<GetUserDto?> GetUserAsync(int id);
        Task<GetUserDto?> AddUserAsync(string firstName, string lastName, string email, string password);
        Task<GetUserDto?> UpdateUserAsync(int id, string email, string oldPassword, string newPassword);
        Task<GetUserDto?> DeleteUserAsync(int id, string password);
        Task<IEnumerable<GetAccountDto>?> GetAccountsFromUserAsync(int userID);
    }
}