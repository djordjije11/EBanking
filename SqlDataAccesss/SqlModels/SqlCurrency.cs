using System.Data;
using EBanking.Models;
using System.Data.SqlClient;

namespace SqlDataAccesss.SqlModels
{
    internal class SqlCurrency : SqlEntity
    {
        public Currency Currency { private get; set; }
        public SqlCurrency(Currency currency) : base(currency)
        {
            Currency = currency;
        }
        public SqlCurrency() { }

        public override string GetTableName() { return "[dbo].[Currency]"; }
        public override IEntity GetEntityFromSqlReader(SqlDataReader reader)
        {
            return new Currency()
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                CurrencyCode = reader.GetString("Code")
            };
        }
        public override void SetSqlInsertCommand(SqlCommand command)
        {
            command.CommandText = $"insert into {GetTableName()}(Name, Code) output inserted.ID values (@name, @code)";
            command.Parameters.AddWithValue("@name", Currency.Name);
            command.Parameters.AddWithValue("@code", Currency.CurrencyCode);
        }
        public override void SetSqlUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET Name = '{Currency.Name}', Code = '{Currency.CurrencyCode}' WHERE Id = {Currency.Id}";
        }
        public override void SetSqlDeleteByIdCommand(SqlCommand command)
        {
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE id={Currency.Id}";
        }
        public override void SetSqlSelectByIdCommand(SqlCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()} WHERE id={Currency.Id}";
        }
        public override void SetSqlSelectAllCommand(SqlCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()}";
        }
        public void SetSqlSelectAllAccountsByCurrencyCommand(SqlCommand command)
        {
            command.CommandText = $"SELECT * FROM {new SqlAccount().GetTableName()} WHERE CurrencyId={Currency.Id}";
        }
    }
}
