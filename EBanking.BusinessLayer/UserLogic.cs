using EBanking.BusinessLayer.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.BusinessLayer
{
    public class UserLogic : IUserLogic
    {
        IUserBroker UserBroker { get; }
        public IServiceProvider ServiceProvider { get; }
        public UserLogic(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            UserBroker = serviceProvider.GetRequiredService<IUserBroker>();
        }
        public async Task<User> AddUserAsync(string firstName, string lastName, string email, string password)
        {
            var user = new User() { FirstName = firstName, LastName = lastName, Email = email, Password = password };
            var resultInfo = new UserValidator(user).Validate();
            if (resultInfo.IsValid == false)
                throw new Exception(resultInfo.GetErrorsString());
            try
            {
                await UserBroker.StartConnectionAsync();
                await UserBroker.StartTransactionAsync();
                var createdUser = await UserBroker.CreateUserAsync(user);
                await UserBroker.CommitTransactionAsync();
                return createdUser;
            }
            catch
            {
                await UserBroker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await UserBroker.EndConnectionAsync();
            }
        }
        public async Task<User> UpdateUserAsync(int userId, string firstName, string lastName, string oldPassword, string newPassword)
        {
            try
            {
                await UserBroker.StartConnectionAsync();
                var user = await UserBroker.GetUserByIdAsync(new User() { Id = userId });
                if(user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");
                if (user.Password.Equals(oldPassword) == false)
                    throw new Exception("Не можете мењати податке о кориснику без тачно унете шифре.");
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Password = newPassword;
                var resultInfo = new UserValidator(user).Validate();
                if (resultInfo.IsValid == false)
                    throw new Exception(resultInfo.GetErrorsString());
                await UserBroker.StartTransactionAsync();
                var updatedUser = await UserBroker.UpdateUserByIdAsync(user);
                await UserBroker.CommitTransactionAsync();
                return updatedUser;
            }
            catch
            {
                await UserBroker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await UserBroker.EndConnectionAsync();
            }
        }
        public async Task<User> DeleteUserAsync(int userId, string password)
        {
            try
            {
                await UserBroker.StartConnectionAsync();
                var user = await UserBroker.GetUserByIdAsync(new User() { Id = userId });
                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");
                var accounts = await UserBroker.GetAccountsByUserAsync(user);
                if (accounts != null && accounts.Count > 0)
                    throw new Exception("Не можете брисати корисника уколико већ има рачуне.");
                if (user.Password.Equals(password) == false)
                    throw new Exception("Не можете брисати корисника без тачно унете шифре.");
                await UserBroker.StartTransactionAsync();
                var deletedUser = await UserBroker.DeleteUserAsync(user);
                await UserBroker.CommitTransactionAsync();
                return deletedUser;
            }
            catch
            {
                await UserBroker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await UserBroker.EndConnectionAsync();
            }
        }
        public async Task<User> FindUserAsync(int userId)
        {
            try
            {
                await UserBroker.StartConnectionAsync();
                var user = await UserBroker.GetUserByIdAsync(new User() { Id = userId });
                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");
                return user;
            }
            finally
            {
                await UserBroker.EndConnectionAsync();
            }
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                await UserBroker.StartConnectionAsync();
                return await UserBroker.GetAllUsersAsync(new User());
            }
            finally
            {
                await UserBroker.EndConnectionAsync();
            }
        }
        public async Task<List<Account>> GetAccountsByUserAsync(int userId)
        {
            try
            {
                await UserBroker.StartConnectionAsync();
                var user = await UserBroker.GetUserByIdAsync(new User() { Id = userId });
                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");
                var accounts = await UserBroker.GetAccountsByUserAsync(user);
                return accounts;
            }
            finally
            {
                await UserBroker.EndConnectionAsync();
            }
        }
    }
}
