using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models.Helper;
using EBanking.Models;
using SqliteDataAccess.SqliteModels;
using System.Data.SQLite;

namespace SqliteDataAccess.SqliteBrokers
{
    public class SqliteUserBroker : SqliteEntityBroker, IUserBroker
    {
        public SqliteUserBroker(IConnector connector) : base(connector)
        {
        }
        public async Task<User> CreateUserAsync(User user)
        {
            return (User)(await CreateEntityAsync(new SqliteUser(user)));
        }
        public async Task<User> UpdateUserByIdAsync(User user)
        {
            return (User)(await UpdateEntityByIdAsync(new SqliteUser(user)));
        }
        public async Task<User> DeleteUserAsync(User user)
        {
            return (User)(await DeleteEntityAsync(new SqliteUser(user)));
        }
        public async Task<User?> GetUserByIdAsync(User user)
        {
            return (await GetEntityByIdAsync(new SqliteUser(user))) as User;
        }
        public async Task<List<User>> GetAllUsersAsync(User user)
        {
            return EntitiesConverter<User>.ConvertList(await GetAllEntitiesAsync(new SqliteUser(user)));
        }
        public async Task<List<Account>> GetAccountsByUserAsync(User user)
        {
            SqliteUser sqlUser = new SqliteUser(user);
            SqliteAccount sqlAccount = new SqliteAccount();
            var accounts = new List<Account>();
            connector.StartCommand();
            var command = connector.GetCommand();
            sqlUser.SetSqliteSelectAllAccountsByUserId(command);
            SQLiteDataReader reader = (SQLiteDataReader)await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                accounts.Add((Account)sqlAccount.GetEntityFromSqliteReader(reader));
            }
            await reader.CloseAsync();
            return accounts;
        }
    }
}
