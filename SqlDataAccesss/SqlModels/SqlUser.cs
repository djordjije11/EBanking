using EBanking.Models;
using System.Data;
using System.Data.SqlClient;

namespace SqlDataAccesss.SqlModels
{
    internal class SqlUser : User, ISqlEntity
    {
        public string GetTableName() { return "[dbo].[User]"; }
        public IEntity GetEntityFromSqlReader(SqlDataReader reader)
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
        public void SetSqlInsertCommand(SqlCommand command)
        {
            command.CommandText = $"insert into {GetTableName()}(FirstName, LastName, Email, Password) output inserted.ID values (@firstname, @lastname, @email, @password)";
            command.Parameters.AddWithValue("@firstname", FirstName);
            command.Parameters.AddWithValue("@lastname", LastName);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@password", Password);
        }
        public void SetSqlUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET FirstName = '{FirstName}', LastName = '{LastName}', Email = '{Email}', Password = '{Password}' WHERE Id = {Id}";
        }
        public void SetSqlDeleteByIdCommand(SqlCommand command)
        {
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE id={Id}";
        }
        public void SetSqlSelectByIdCommand(SqlCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()} WHERE id={Id}";
        }
        public void SetSqlSelectAllCommand(SqlCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()}";
        }
        public void SetSqlSelectAllAccountsByUserId(SqlCommand command)
        {
            command.CommandText = "select a.Id, a.Balance, a.Status, a.Number, u.Id as userID, u.FirstName, u.LastName, u.Email, u.Password, c.Id as currencyID, c.Name, c.Code " +
                $"from {new SqlAccount().GetTableName()} as a INNER JOIN {GetTableName()} as u ON (a.UserId = u.Id) INNER JOIN {new SqlCurrency().GetTableName()} as c ON (a.CurrencyId = c.Id) WHERE u.Id = " + Id;
        }
    }
}
