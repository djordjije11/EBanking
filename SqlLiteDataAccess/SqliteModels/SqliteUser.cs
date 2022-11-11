using EBanking.Models;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace SqliteDataAccess.SqliteModels
{
    internal class SqliteUser : SqliteEntity
    {
        public User User { private get; set; }
        public SqliteUser(User entity) : base(entity)
        {
            User = entity;
        }
        public SqliteUser()
        {
        }
        public override string GetTableName() { return "[dbo].[User]"; }
        public override IEntity GetEntityFromSqliteReader(SQLiteDataReader reader)
        {
            return new User()
            {
                Id = reader.GetInt32("Id"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                Email = reader.GetString("Email"),
                Password = reader.GetString("Password")
            };
        }
        public override void SetSqliteInsertCommand(SQLiteCommand command)
        {
            command.CommandText = $"insert into {GetTableName()}(FirstName, LastName, Email, Password) output inserted.ID values (@firstname, @lastname, @email, @password)";
            command.Parameters.AddWithValue("@firstname", User.FirstName);
            command.Parameters.AddWithValue("@lastname", User.LastName);
            command.Parameters.AddWithValue("@email", User.Email);
            command.Parameters.AddWithValue("@password", User.Password);
        }
        public override void SetSqliteUpdateByIdCommand(SQLiteCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET FirstName = '{User.FirstName}', LastName = '{User.LastName}', Email = '{User.Email}', Password = '{User.Password}' WHERE Id = {User.Id}";
        }
        public override void SetSqliteDeleteByIdCommand(SQLiteCommand command)
        {
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE id={User.Id}";
        }
        public override void SetSqliteSelectByIdCommand(SQLiteCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()} WHERE id={User.Id}";
        }
        public override void SetSqliteSelectAllCommand(SQLiteCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()}";
        }
        public void SetSqliteSelectAllAccountsByUserId(SQLiteCommand command)
        {
            command.CommandText = "select a.Id, a.Balance, a.Status, a.Number, u.Id as userID, u.FirstName, u.LastName, u.Email, u.Password, c.Id as currencyID, c.Name, c.Code " +
                $"from {new SqliteAccount().GetTableName()} as a INNER JOIN {GetTableName()} as u ON (a.UserId = u.Id) INNER JOIN {new SqliteCurrency().GetTableName()} as c ON (a.CurrencyId = c.Id) WHERE u.Id = " + User.Id;
        }
    }
}
