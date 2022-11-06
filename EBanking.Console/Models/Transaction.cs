using EBanking.Console.Model;
using System.Data;
using System.Data.SqlClient;

namespace EBanking.Console.Models
{
    internal class Transaction : IEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public override string ToString()
        {
            return $"{Id}. Износ: {Amount}, Давалац: {FromAccount} - Прималац: {ToAccount}";
        }
        public int GetIdentificator() { return Id; }
        public void SetIdentificator(int id) { Id = id; }
        public string GetTableName() { return "[dbo].[Transaction]"; }
        public string SinglePrint()
        {
            return $"\nИД: {Id}\nИзнос: {Amount}\nДатум: {Date}\nДавалац: {FromAccount}\nПрималац: {ToAccount}";
        }
        public IEntity GetEntityFromReader(SqlDataReader reader)
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
                    Status = (Status)reader.GetInt32("faStatus"),
                    Number = reader.GetString("faNumber"),
                    User = new User(){
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
                    Status = (Status)reader.GetInt32("taStatus"),
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
        public void SetInsertCommand(SqlCommand command)
        {
            command.CommandText = "insert into [dbo].[Transaction](Amount, Date, FromAccountId, ToAccountId)" +
                " output inserted.ID values (@amount, @date, @fromAccountId, @toAccountId)";
            command.Parameters.AddWithValue("@amount", Amount);
            command.Parameters.AddWithValue("@date", (Date));
            command.Parameters.AddWithValue("@fromAccountId", FromAccount.Id);
            command.Parameters.AddWithValue("@toAccountId", ToAccount.Id);
        }
        public void SetUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET Amount = {Amount}, Date = '{Date}', FromAccountId = {FromAccount.Id}, ToAccountId = {ToAccount.Id} WHERE Id = {Id}";
        }
        public void SetDeleteByIdCommand(SqlCommand command)
        {
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE id={Id}";
        }
        public void SetSelectByIdCommand(SqlCommand command)
        {
            command.CommandText = "select t.*, fa.Balance as faBalance, fa.Status as faStatus, fa.Number as faNumber, " +
                "fa.UserId as faUserId, fu.FirstName as fuFirstName, fu.LastName as fuLastName, fu.Email as fuEmail, fu.Password as fuPassword, " +
                "fa.CurrencyId as faCurrencyId, fc.Name as fcName, fc.Code as fcCode, " +
                "ta.Balance as taBalance, ta.Status as taStatus, ta.Number as taNumber, " +
                "ta.UserId as taUserId, tu.FirstName as tuFirstName, tu.LastName as tuLastName, tu.Email as tuEmail, tu.Password as tuPassword, " +
                "ta.CurrencyId as taCurrencyId, tc.Name as tcName, tc.Code as tcCode " +
                $"from {GetTableName()} as t INNER JOIN {new Account().GetTableName()} as fa ON (t.FromAccountId = fa.Id) " +
                $"INNER JOIN {new Account().GetTableName()} as ta ON (t.ToAccountId = ta.Id) " +
                $"INNER JOIN {new User().GetTableName()} as fu ON (fa.UserId = fu.Id) " +
                $"INNER JOIN {new Currency().GetTableName()} as fc ON (fa.CurrencyId = fc.Id)" +
                $"INNER JOIN {new User().GetTableName()} as tu ON (ta.UserId = tu.Id)" +
                $"INNER JOIN {new Currency().GetTableName()} as tc ON (ta.CurrencyId = tc.Id) " +
                "WHERE t.id =" + Id;
        }
        public void SetSelectAllCommand(SqlCommand command)
        {
            command.CommandText = "select t.*, fa.Balance as faBalance, fa.Status as faStatus, fa.Number as faNumber, " +
                "fa.UserId as faUserId, fu.FirstName as fuFirstName, fu.LastName as fuLastName, fu.Email as fuEmail, fu.Password as fuPassword, " +
                "fa.CurrencyId as faCurrencyId, fc.Name as fcName, fc.Code as fcCode, " +
                "ta.Balance as taBalance, ta.Status as taStatus, ta.Number as taNumber, " +
                "ta.UserId as taUserId, tu.FirstName as tuFirstName, tu.LastName as tuLastName, tu.Email as tuEmail, tu.Password as tuPassword, " +
                "ta.CurrencyId as taCurrencyId, tc.Name as tcName, tc.Code as tcCode " +
                $"from {GetTableName()} as t INNER JOIN {new Account().GetTableName()} as fa ON (t.FromAccountId = fa.Id) " +
                $"INNER JOIN {new Account().GetTableName()} as ta ON (t.ToAccountId = ta.Id) " +
                $"INNER JOIN {new User().GetTableName()} as fu ON (fa.UserId = fu.Id) " +
                $"INNER JOIN {new Currency().GetTableName()} as fc ON (fa.CurrencyId = fc.Id)" +
                $"INNER JOIN {new User().GetTableName()} as tu ON (ta.UserId = tu.Id)" +
                $"INNER JOIN {new Currency().GetTableName()} as tc ON (ta.CurrencyId = tc.Id)";
        }
        public void SetSelectAllByAccountId(SqlCommand command)
        {
            command.CommandText = "select t.*, fa.Balance as faBalance, fa.Status as faStatus, fa.Number as faNumber, " +
                "fa.UserId as faUserId, fu.FirstName as fuFirstName, fu.LastName as fuLastName, fu.Email as fuEmail, fu.Password as fuPassword, " +
                "fa.CurrencyId as faCurrencyId, fc.Name as fcName, fc.Code as fcCode, " +
                "ta.Balance as taBalance, ta.Status as taStatus, ta.Number as taNumber, " +
                "ta.UserId as taUserId, tu.FirstName as tuFirstName, tu.LastName as tuLastName, tu.Email as tuEmail, tu.Password as tuPassword, " +
                "ta.CurrencyId as taCurrencyId, tc.Name as tcName, tc.Code as tcCode " +
                $"from {GetTableName()} as t INNER JOIN {new Account().GetTableName()} as fa ON (t.FromAccountId = fa.Id) " +
                $"INNER JOIN {new Account().GetTableName()} as ta ON (t.ToAccountId = ta.Id) " +
                $"INNER JOIN {new User().GetTableName()} as fu ON (fa.UserId = fu.Id) " +
                $"INNER JOIN {new Currency().GetTableName()} as fc ON (fa.CurrencyId = fc.Id)" +
                $"INNER JOIN {new User().GetTableName()} as tu ON (ta.UserId = tu.Id)" +
                $"INNER JOIN {new Currency().GetTableName()} as tc ON (ta.CurrencyId = tc.Id) " +
                $"WHERE t.FromAccountId = {FromAccount.Id} OR t.ToAccountId = {ToAccount.Id}";
        }
    }
}