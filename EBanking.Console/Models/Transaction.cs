using EBanking.Console.Model;
using System.Data;
using System.Data.SqlClient;

namespace EBanking.Console.Models
{
    internal class Transaction : Entity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public override Entity GetEntity(SqlDataReader reader)
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
        public override void SetSelectAllCommand(SqlCommand command)
        {
            command.CommandText = "select t.*, fa.Balance as faBalance, fa.Status as faStatus, fa.Number as faNumber, " +
                "fa.UserId as faUserId, fu.FirstName as fuFirstName, fu.LastName as fuLastName, fu.Email as fuEmail, fu.Password as fuPassword, " +
                "fa.CurrencyId as faCurrencyId, fc.Name as fcName, fc.Code as fcCode, " +
                "ta.Balance as taBalance, ta.Status as taStatus, ta.Number as taNumber, " +
                "ta.UserId as taUserId, tu.FirstName as tuFirstName, tu.LastName as tuLastName, tu.Email as tuEmail, tu.Password as tuPassword, " +
                "ta.CurrencyId as taCurrencyId, tc.Name as tcName, tc.Code as tcCode " +
                "from [dbo].[Transaction] as t INNER JOIN [dbo].[Account] as fa ON (t.FromAccountId = fa.Id) " +
                "INNER JOIN [dbo].[Account] as ta ON (t.ToAccountId = ta.Id) " +
                "INNER JOIN [dbo].[User] as fu ON (fa.UserId = fu.Id) " +
                "INNER JOIN [dbo].[Currency] as fc ON (fa.CurrencyId = fc.Id)" +
                "INNER JOIN [dbo].[User] as tu ON (ta.UserId = tu.Id)" +
                "INNER JOIN [dbo].[Currency] as tc ON (ta.CurrencyId = tc.Id)";
        }
        public override void SetSelectByIdCommand(SqlCommand command)
        {
            command.CommandText = "select t.*, fa.Balance as faBalance, fa.Status as faStatus, fa.Number as faNumber, " +
                "fa.UserId as faUserId, fu.FirstName as fuFirstName, fu.LastName as fuLastName, fu.Email as fuEmail, fu.Password as fuPassword, " +
                "fa.CurrencyId as faCurrencyId, fc.Name as fcName, fc.Code as fcCode, " +
                "ta.Balance as taBalance, ta.Status as taStatus, ta.Number as taNumber, " +
                "ta.UserId as taUserId, tu.FirstName as tuFirstName, tu.LastName as tuLastName, tu.Email as tuEmail, tu.Password as tuPassword, " +
                "ta.CurrencyId as taCurrencyId, tc.Name as tcName, tc.Code as tcCode " +
                "from [dbo].[Transaction] as t INNER JOIN [dbo].[Account] as fa ON (t.FromAccountId = fa.Id) " +
                "INNER JOIN [dbo].[Account] as ta ON (t.ToAccountId = ta.Id) " +
                "INNER JOIN [dbo].[User] as fu ON (fa.UserId = fu.Id) " +
                "INNER JOIN [dbo].[Currency] as fc ON (fa.CurrencyId = fc.Id) " +
                "INNER JOIN [dbo].[User] as tu ON (ta.UserId = tu.Id) " +
                "INNER JOIN [dbo].[Currency] as tc ON (ta.CurrencyId = tc.Id) " +
                "WHERE t.id = " + Id;
        }
        public override int GetIdentificator() => Id;
        public override void SetIdentificator(int id)
        {
            Id = id;
        }
        public override void SetInsertEntityCommand(SqlCommand command)
        {
            command.CommandText = "insert into [dbo].[Transaction](Amount, Date, FromAccountId, ToAccountId)" +
                " output inserted.ID values (@amount, @date, @fromAccountId, @toAccountId)";
            command.Parameters.AddWithValue("@amount", this.Amount);
            command.Parameters.AddWithValue("@date", (this.Date));
            command.Parameters.AddWithValue("@fromAccountId", this.FromAccount.Id);
            command.Parameters.AddWithValue("@toAccountId", this.ToAccount.Id);
        }
        public override void SetUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE [dbo].[Transaction] SET Amount = {Amount}, Date = '{Date}', FromAccountId = {FromAccount.Id}, ToAccountId = {ToAccount.Id} WHERE Id = {Id}";
        }
        public override string ToString()
        {
            return $"{Id}. Износ: {Amount}, Давалац: {FromAccount} - Прималац: {ToAccount}";
        }
        public override string SinglePrint()
        {
            return $"\nИД: {Id}\nИзнос: {Amount}\nДатум: {Date}\nДавалац: {FromAccount}\nПрималац: {ToAccount}";
        }

        public override void SetSelectAllWhereCommand(SqlCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
