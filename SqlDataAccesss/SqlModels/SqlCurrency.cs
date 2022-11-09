using System.Data;
using EBanking.Models;
using System.Data.SqlClient;

namespace SqlDataAccesss.SqlModels
{
    internal class SqlCurrency : Currency, ISqlEntity
    {
        public string GetTableName() { return "[dbo].[Currency]"; }
        public IEntity GetEntityFromSqlReader(SqlDataReader reader)
        {
            return new Currency()
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                CurrencyCode = reader.GetString("Code")
            };
        }
        public void SetSqlInsertCommand(SqlCommand command)
        {
            command.CommandText = $"insert into {GetTableName()}(Name, Code) output inserted.ID values (@name, @code)";
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@code", CurrencyCode);
        }
        public void SetSqlUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET Name = '{Name}', Code = '{CurrencyCode}' WHERE Id = {Id}";
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
        public void SetSqlSelectAllAccountsByCurrencyCommand(SqlCommand command)
        {
            command.CommandText = $"SELECT * FROM {new SqlAccount().GetTableName()} WHERE CurrencyId={Id}";
        }
    }
}
