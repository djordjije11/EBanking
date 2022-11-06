using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EBanking.Console.Model
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        public int GetIdentificator() { return Id; }
        public void SetIdentificator(int id) { Id = id; }
        public string GetTableName() { return "[dbo].[User]"; }
        public string SinglePrint()
        {
            return $"\nИД: {Id}\nИме: {FirstName}\nПрезиме: {LastName}\nЕмаил: {Email}";
        }
        public IEntity GetEntityFromReader(SqlDataReader reader)
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
        public void SetInsertCommand(SqlCommand command)
        {
            command.CommandText = $"insert into {GetTableName()}(FirstName, LastName, Email, Password) output inserted.ID values (@firstname, @lastname, @email, @password)";
            command.Parameters.AddWithValue("@firstname", FirstName);
            command.Parameters.AddWithValue("@lastname", LastName);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@password", Password);
        }
        public void SetUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET FirstName = '{FirstName}', LastName = '{LastName}', Email = '{Email}', Password = '{Password}' WHERE Id = {Id}";
        }
        public void SetDeleteByIdCommand(SqlCommand command)
        {
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE id={Id}";
        }
        public void SetSelectByIdCommand(SqlCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()} WHERE id={Id}";
        }
        public void SetSelectAllCommand(SqlCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()}";
        }
    }
}
