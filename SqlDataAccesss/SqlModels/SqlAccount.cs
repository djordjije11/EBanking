using EBanking.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SqlDataAccesss.SqlModels
{
    internal class SqlAccount : SqlEntity
    {
        public Account Account { private get; set; }
        public SqlAccount(Account account) : base(account)
        {
            Account = account;
        }
        public SqlAccount() { }
        public override string GetTableName() { return "[dbo].[Account]"; }
        public override IEntity GetEntityFromSqlReader(SqlDataReader reader)
        {
            return new Account()
            {
                Id = reader.GetInt32("Id"),
                Balance = reader.GetDecimal("Balance"),
                Status = (AccountStatus)reader.GetInt32("Status"),
                Number = reader.GetString("Number"),
                User = new User()
                {
                    Id = reader.GetInt32("userID"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Email = reader.GetString("Email"),
                    Password = reader.GetString("Password")
                },
                Currency = new Currency()
                {
                    Id = reader.GetInt32("currencyID"),
                    Name = reader.GetString("Name"),
                    CurrencyCode = reader.GetString("Code")
                }
            };
        }
        public override void SetSqlInsertCommand(SqlCommand command)
        {
            command.CommandText = "insert into [dbo].[Account](Balance, Status, Number, UserId, CurrencyId)" +
                " output inserted.ID values (@balance, @status, @number, @userId, @currencyId)";
            command.Parameters.AddWithValue("@balance", Account.Balance);
            command.Parameters.AddWithValue("@status", ((int)Account.Status));
            command.Parameters.AddWithValue("@number", Account.Number);
            command.Parameters.AddWithValue("@userId", Account.User.Id);
            command.Parameters.AddWithValue("@currencyId", Account.Currency.Id);
        }
        public override void SetSqlUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET Balance = {Account.Balance}, Status = {(int)Account.Status}, Number = '{Account.Number}', UserId = {Account.User.Id}, CurrencyId = {Account.Currency.Id} WHERE Id = {Account.Id}";
        }
        public override void SetSqlDeleteByIdCommand(SqlCommand command)
        {
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE id={Account.Id}";
        }
        public override void SetSqlSelectByIdCommand(SqlCommand command)
        {
            command.CommandText = $"select a.Id, a.Balance, a.Status, a.Number, u.Id as userID, u.FirstName, u.LastName, u.Email, u.Password, c.Id as currencyID, c.Name, c.Code " +
                 $"from {GetTableName()} as a INNER JOIN {new SqlUser().GetTableName()} as u ON (a.UserId = u.Id) INNER JOIN {new SqlCurrency().GetTableName()} as c ON (a.CurrencyId = c.Id) " +
                 "where a.Id = " + Account.Id;
        }
        public override void SetSqlSelectAllCommand(SqlCommand command)
        {
            command.CommandText = $"select a.Id, a.Balance, a.Status, a.Number, u.Id as userID, u.FirstName, u.LastName, u.Email, u.Password, c.Id as currencyID, c.Name, c.Code " +
                $"from {GetTableName()} as a INNER JOIN {new SqlUser().GetTableName()} as u ON (a.UserId = u.Id) INNER JOIN {new SqlCurrency().GetTableName()} as c ON (a.CurrencyId = c.Id)";
        }
        public void SetSqlSelectAccountByNumber(SqlCommand command)
        {
            command.CommandText = $"select a.Id, a.Balance, a.Status, a.Number, u.Id as userID, u.FirstName, u.LastName, u.Email, u.Password, c.Id as currencyID, c.Name, c.Code " +
                 $"from {GetTableName()} as a INNER JOIN {new SqlUser().GetTableName()} as u ON (a.UserId = u.Id) INNER JOIN {new SqlCurrency().GetTableName()} as c ON (a.CurrencyId = c.Id) " +
                 $"where a.Number = '{Account.Number}'";
        }
        public void SetSqlSelectAllTransactionsByAccountId(SqlCommand command)
        {
            command.CommandText = "select t.*, fa.Balance as faBalance, fa.Status as faStatus, fa.Number as faNumber, " +
                "fa.UserId as faUserId, fu.FirstName as fuFirstName, fu.LastName as fuLastName, fu.Email as fuEmail, fu.Password as fuPassword, " +
                "fa.CurrencyId as faCurrencyId, fc.Name as fcName, fc.Code as fcCode, " +
                "ta.Balance as taBalance, ta.Status as taStatus, ta.Number as taNumber, " +
                "ta.UserId as taUserId, tu.FirstName as tuFirstName, tu.LastName as tuLastName, tu.Email as tuEmail, tu.Password as tuPassword, " +
                "ta.CurrencyId as taCurrencyId, tc.Name as tcName, tc.Code as tcCode " +
                $"from {new SqlTransaction().GetTableName()} as t INNER JOIN {GetTableName()} as fa ON (t.FromAccountId = fa.Id) " +
                $"INNER JOIN {GetTableName()} as ta ON (t.ToAccountId = ta.Id) " +
                $"INNER JOIN {new SqlUser().GetTableName()} as fu ON (fa.UserId = fu.Id) " +
                $"INNER JOIN {new SqlCurrency().GetTableName()} as fc ON (fa.CurrencyId = fc.Id)" +
                $"INNER JOIN {new SqlUser().GetTableName()} as tu ON (ta.UserId = tu.Id)" +
                $"INNER JOIN {new SqlCurrency().GetTableName()} as tc ON (ta.CurrencyId = tc.Id) " +
                $"WHERE t.FromAccountId = {Account.Id} OR t.ToAccountId = {Account.Id} " +
                $"ORDER BY t.Date";
        }
    }
}
