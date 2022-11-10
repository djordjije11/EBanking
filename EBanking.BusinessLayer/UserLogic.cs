using EBanking.BusinessLayer.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.Validations;

namespace EBanking.BusinessLayer
{
    public class UserLogic : IUserLogic
    {
        IBroker Broker { get; }
        public UserLogic(IBroker broker)
        {
            Broker = broker;
        }
        public async Task<User> AddUserAsync(string firstName, string lastName, string email, string password)
        {
            var user = new User() { FirstName = firstName, LastName = lastName, Email = email, Password = password };
            var resultInfo = new UserValidator(user).Validate();
            if (resultInfo.IsValid == false)
                throw new Exception(resultInfo.GetErrorsString());
            try
            {
                await Broker.StartConnectionAsync();
                await Broker.StartTransactionAsync();
                var createdUser = await Broker.CreateUserAsync(user);
                await Broker.CommitTransactionAsync();
                return createdUser;
            }
            catch
            {
                await Broker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }

        public async Task<User> UpdateUserAsync(int userId, string firstName, string lastName, string oldPassword, string newPassword)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var user = await Broker.GetUserByIdAsync(new User() { Id = userId });
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
                await Broker.StartTransactionAsync();
                var updatedUser = await Broker.UpdateUserByIdAsync(user);
                await Broker.CommitTransactionAsync();
                return updatedUser;
            }
            catch
            {
                await Broker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }

        public async Task<User> DeleteUserAsync(int userId, string password)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var user = await Broker.GetUserByIdAsync(new User() { Id = userId });
                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");
                if (user.Password.Equals(password) == false)
                    throw new Exception("Не можете брисати корисника без тачно унете шифре.");
                await Broker.StartTransactionAsync();
                var deletedUser = await Broker.DeleteUserAsync(user);
                await Broker.CommitTransactionAsync();
                return deletedUser;
            }
            catch
            {
                await Broker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }

        public async Task<User> FindUserAsync(int userId)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var user = await Broker.GetUserByIdAsync(new User() { Id = userId });
                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");
                return user;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                await Broker.StartConnectionAsync();
                return await Broker.GetAllUsersAsync(new User());
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
        public async Task<List<Account>> GetAccountsByUser(int userId)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var user = await Broker.GetUserByIdAsync(new User() { Id = userId });
                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");
                var accounts = await Broker.GetAccountsByUserAsync(user);
                return accounts;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
    }
}
