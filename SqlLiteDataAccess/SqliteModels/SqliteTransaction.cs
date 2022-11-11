using EBanking.Models;
using System.Data;
using System.Data.SQLite;

namespace SqliteDataAccess.SqliteModels
{
    internal class SqliteTransaction : SqliteEntity
    {
        public Transaction Transaction { private get; set; }
        public SqliteTransaction(Transaction transaction) : base(transaction)
        {
            Transaction = transaction;
        }
        public SqliteTransaction() { }
        public override string GetTableName() { return "[dbo].[Transaction]"; }
        public override IEntity GetEntityFromSqliteReader(SQLiteDataReader reader)
        {
            return new Transaction()
            {
                Id = reader.GetInt32("Id"),
                Amount = reader.GetDecimal("Amount"),
                Date = reader.GetDateTime("Date"),
                FromAccount = new Account()
                {
                    Id = reader.GetInt32("FromAccountId"),
                    Balance = reader.GetDecimal("faBalance"),
                    Status = (AccountStatus)reader.GetInt32("faStatus"),
                    Number = reader.GetString("faNumber"),
                    User = new User()
                    {
                        Id = reader.GetInt32("faUserId"),
                        FirstName = reader.GetString("fuFirstName"),
                        LastName = reader.GetString("fuLastName"),
                        Email = reader.GetString("fuEmail"),
                        Password = reader.GetString("fuPassword")
                    },
                    Currency = new Currency()
                    {
                        Id = reader.GetInt32("faCurrencyId"),
                        Name = reader.GetString("fcName"),
                        CurrencyCode = reader.GetString("fcCode")
                    }
                },
                ToAccount = new Account()
                {
                    Id = reader.GetInt32("ToAccountId"),
                    Balance = reader.GetDecimal("taBalance"),
                    Status = (AccountStatus)reader.GetInt32("taStatus"),
                    Number = reader.GetString("taNumber"),
                    User = new User()
                    {
                        Id = reader.GetInt32("taUserId"),
                        FirstName = reader.GetString("tuFirstName"),
                        LastName = reader.GetString("tuLastName"),
                        Email = reader.GetString("tuEmail"),
                        Password = reader.GetString("tuPassword")
                    },
                    Currency = new Currency()
                    {
                        Id = reader.GetInt32("taCurrencyId"),
                        Name = reader.GetString("tcName"),
                        CurrencyCode = reader.GetString("tcCode")
                    }
                }
            };
        }
        public override void SetSqliteInsertCommand(SQLiteCommand command)
        {
            command.CommandText = "insert into [dbo].[Transaction](Amount, Date, FromAccountId, ToAccountId)" +
                " output inserted.ID values (@amount, @date, @fromAccountId, @toAccountId)";
            command.Parameters.AddWithValue("@amount", Transaction.Amount);
            command.Parameters.AddWithValue("@date", (Transaction.Date));
            command.Parameters.AddWithValue("@fromAccountId", Transaction.FromAccount.Id);
            command.Parameters.AddWithValue("@toAccountId", Transaction.ToAccount.Id);
        }
        public override void SetSqliteUpdateByIdCommand(SQLiteCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET Amount = {Transaction.Amount}, Date = '{Transaction.Date}', FromAccountId = {Transaction.FromAccount.Id}, ToAccountId = {Transaction.ToAccount.Id} WHERE Id = {Transaction.Id}";
        }
        public override void SetSqliteDeleteByIdCommand(SQLiteCommand command)
        {
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE id={Transaction.Id}";
        }
        public override void SetSqliteSelectByIdCommand(SQLiteCommand command)
        {
            command.CommandText = "select t.*, fa.Balance as faBalance, fa.Status as faStatus, fa.Number as faNumber, " +
                "fa.UserId as faUserId, fu.FirstName as fuFirstName, fu.LastName as fuLastName, fu.Email as fuEmail, fu.Password as fuPassword, " +
                "fa.CurrencyId as faCurrencyId, fc.Name as fcName, fc.Code as fcCode, " +
                "ta.Balance as taBalance, ta.Status as taStatus, ta.Number as taNumber, " +
                "ta.UserId as taUserId, tu.FirstName as tuFirstName, tu.LastName as tuLastName, tu.Email as tuEmail, tu.Password as tuPassword, " +
                "ta.CurrencyId as taCurrencyId, tc.Name as tcName, tc.Code as tcCode " +
                $"from {GetTableName()} as t INNER JOIN {new SqliteAccount().GetTableName()} as fa ON (t.FromAccountId = fa.Id) " +
                $"INNER JOIN {new SqliteAccount().GetTableName()} as ta ON (t.ToAccountId = ta.Id) " +
                $"INNER JOIN {new SqliteUser().GetTableName()} as fu ON (fa.UserId = fu.Id) " +
                $"INNER JOIN {new SqliteCurrency().GetTableName()} as fc ON (fa.CurrencyId = fc.Id)" +
                $"INNER JOIN {new SqliteUser().GetTableName()} as tu ON (ta.UserId = tu.Id)" +
                $"INNER JOIN {new SqliteCurrency().GetTableName()} as tc ON (ta.CurrencyId = tc.Id) " +
                "WHERE t.id =" + Transaction.Id;
        }
        public override void SetSqliteSelectAllCommand(SQLiteCommand command)
        {
            command.CommandText = "select t.*, fa.Balance as faBalance, fa.Status as faStatus, fa.Number as faNumber, " +
                "fa.UserId as faUserId, fu.FirstName as fuFirstName, fu.LastName as fuLastName, fu.Email as fuEmail, fu.Password as fuPassword, " +
                "fa.CurrencyId as faCurrencyId, fc.Name as fcName, fc.Code as fcCode, " +
                "ta.Balance as taBalance, ta.Status as taStatus, ta.Number as taNumber, " +
                "ta.UserId as taUserId, tu.FirstName as tuFirstName, tu.LastName as tuLastName, tu.Email as tuEmail, tu.Password as tuPassword, " +
                "ta.CurrencyId as taCurrencyId, tc.Name as tcName, tc.Code as tcCode " +
                $"from {GetTableName()} as t INNER JOIN {new SqliteAccount().GetTableName()} as fa ON (t.FromAccountId = fa.Id) " +
                $"INNER JOIN {new SqliteAccount().GetTableName()} as ta ON (t.ToAccountId = ta.Id) " +
                $"INNER JOIN {new SqliteUser().GetTableName()} as fu ON (fa.UserId = fu.Id) " +
                $"INNER JOIN {new SqliteCurrency().GetTableName()} as fc ON (fa.CurrencyId = fc.Id)" +
                $"INNER JOIN {new SqliteUser().GetTableName()} as tu ON (ta.UserId = tu.Id)" +
                $"INNER JOIN {new SqliteCurrency().GetTableName()} as tc ON (ta.CurrencyId = tc.Id)";
        }
    }
}
