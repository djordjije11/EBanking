using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.Models.Helper;
using SqlDataAccesss.SqlModels;

namespace EBanking.SqlDataAccess.SqlBrokers
{
    public class SqlUserBroker : SqlEntityBroker, IUserBroker
    {
        public async Task<User> CreateUserAsync(User user)
        {
            return (User)(await CreateEntityAsync(user));
        }
        public async Task<User> UpdateUserByIdAsync(User user)
        {
            return (User)(await UpdateEntityByIdAsync(user));
        }
        public async Task<User> DeleteUserAsync(User user)
        {
            return (User)(await DeleteEntityAsync(user));
        }
        public async Task<User?> GetUserByIdAsync(User user)
        {
            return (await GetEntityByIdAsync(user)) as User;
        }
        public async Task<List<User>> GetAllUsersAsync(User user)
        {
            return EntitiesConverter<User>.ConvertList(await GetAllEntitiesAsync(user));
        }
        public async Task<List<Account>> GetAccountsByUserAsync(User user)
        {
            SqlUser sqlUser = (SqlUser)user;
            SqlAccount sqlAccount = new SqlAccount();
            var accounts = new List<Account>();
            connector.StartCommand();
            var command = connector.GetCommand();
            sqlUser.SetSqlSelectAllAccountsByUserId(command);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                accounts.Add((Account)sqlAccount.GetEntityFromSqlReader(reader));
            }
            await reader.CloseAsync();
            return accounts;
        }
    }
}
