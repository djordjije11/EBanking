using EBanking.Models;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace SqliteDataAccess.SqliteModels
{
    internal class SqliteCurrency : SqliteEntity
    {
        public Currency Currency { private get; set; }
        public SqliteCurrency(Currency currency) : base(currency)
        {
            Currency = currency;
        }
        public SqliteCurrency() { }

        public override string GetTableName() { return "[dbo].[Currency]"; }
        public override IEntity GetEntityFromSqliteReader(SQLiteDataReader reader)
        {
            return new Currency()
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                Code = reader.GetString("Code")
            };
        }
        public override void SetSqliteInsertCommand(SQLiteCommand command)
        {
            command.CommandText = $"insert into {GetTableName()}(Name, Code) output inserted.ID values (@name, @code)";
            command.Parameters.AddWithValue("@name", Currency.Name);
            command.Parameters.AddWithValue("@code", Currency.Code);
        }
        public override void SetSqliteUpdateByIdCommand(SQLiteCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET Name = '{Currency.Name}', Code = '{Currency.Code}' WHERE Id = {Currency.Id}";
        }
        public override void SetSqliteDeleteByIdCommand(SQLiteCommand command)
        {
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE id={Currency.Id}";
        }
        public override void SetSqliteSelectByIdCommand(SQLiteCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()} WHERE id={Currency.Id}";
        }
        public override void SetSqliteSelectAllCommand(SQLiteCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()}";
        }
        public void SetSqlSelectAllAccountsByCurrencyCommand(SQLiteCommand command)
        {
            command.CommandText = $"SELECT * FROM {new SqliteAccount().GetTableName()} WHERE CurrencyId={Currency.Id}";
        }
    }
}
