using EBanking.Console.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EBanking.Console.Models
{
    internal class Account : Entity
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public Status Status { get; set; }
        public string Number { get; set; }
        public User User { get; set; }
        public Currency Currency { get; set; }
        public override Entity GetEntity(SqlDataReader reader)
        {
            return new Account()
            {
                Id = reader.GetInt32("Id"),
                Balance = reader.GetDecimal("Balance"),
                Status = (Status)reader.GetInt32("Status"),
                Number = reader.GetString("Number"),
                User = new User() {
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
        public override void SetSelectAllCommand(SqlCommand command)
        {
            command.CommandText = $"select a.Id, a.Balance, a.Status, a.Number, u.Id as userID, u.FirstName, u.LastName, u.Email, u.Password, c.Id as currencyID, c.Name, c.Code " +
                $"from [dbo].[Account] as a INNER JOIN [dbo].[User] as u ON (a.UserId = u.Id) INNER JOIN [dbo].[Currency] as c ON (a.CurrencyId = c.Id)";
        }
        public override void SetSelectByIdCommand(SqlCommand command)
        {
            command.CommandText = $"select a.Id, a.Balance, a.Status, a.Number, u.Id as userID, u.FirstName, u.LastName, u.Email, u.Password, c.Id as currencyID, c.Name, c.Code " +
                 $"from [dbo].[Account] as a INNER JOIN [dbo].[User] as u ON (a.UserId = u.Id) INNER JOIN [dbo].[Currency] as c ON (a.CurrencyId = c.Id) " +
                 "where a.Id = " + Id;
        }
        public override int GetIdentificator()
        {
            return Id;
        }
        public override void SetIdentificator(int id)
        {
            Id = id;
        }
        public override void SetInsertEntityCommand(SqlCommand command)
        {
            command.CommandText = "insert into [dbo].[Account](Balance, Status, Number, UserId, CurrencyId)" +
                " output inserted.ID values (@balance, @status, @number, @userId, @currencyId)";
            command.Parameters.AddWithValue("@balance", this.Balance);
            command.Parameters.AddWithValue("@status", ((int)this.Status));
            command.Parameters.AddWithValue("@number", this.Number);
            command.Parameters.AddWithValue("@userId", this.User.Id);
            command.Parameters.AddWithValue("@currencyId", this.Currency.Id);
        }
        public override void SetUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE [dbo].[Account] SET Balance = {Balance}, Status = {(int)Status}, Number = '{Number}', UserId = {User.Id}, CurrencyId = {Currency.Id} WHERE Id = {Id}";
        }
        public override string ToString()
        {
            return $"{Number}: {Balance}{Currency}, {User}";
        }
        public override string SinglePrint()
        {
            return $"\nИД: {Id}\nСтање: {Balance}\nСтатус рачуна: {Status.ToString().ToLower()}\nБрој рачуна: {Number}\nКорисник: {User}\nВалута: {Currency}";
        }

        public override void SetSelectAllWhereCommand(SqlCommand command)
        {
            command.CommandText = "select a.Id, a.Balance, a.Status, a.Number, u.Id as userID, u.FirstName, u.LastName, u.Email, u.Password, c.Id as currencyID, c.Name, c.Code " +
                "from [dbo].[Account] as a INNER JOIN [dbo].[User] as u ON (a.UserId = u.Id) INNER JOIN [dbo].[Currency] as c ON (a.CurrencyId = c.Id) WHERE u.Id = " + User.Id;
        }
    }
}
