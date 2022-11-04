using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Model
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override int GetIdentificator()
        {
            return Id;
        }

        public override void SetIdentificator(int id)
        {
            this.Id = id;
        }
        public override Entity GetEntity(SqlDataReader reader)
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

        public override void SetInsertEntityCommand(SqlCommand command)
        {
            command.CommandText = "insert into [dbo].[User](FirstName, LastName, Email, Password) output inserted.ID values (@firstname, @lastname, @email, @password)";
            command.Parameters.AddWithValue("@firstname", this.FirstName);
            command.Parameters.AddWithValue("@lastname", this.LastName);
            command.Parameters.AddWithValue("@email", this.Email);
            command.Parameters.AddWithValue("@password", this.Password);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.FirstName} {this.LastName}";
        }

        public override void SetUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE [dbo].[User] SET FirstName = '{FirstName}', LastName = '{LastName}', Email = '{Email}', Password = '{Password}' WHERE Id = {Id}";
        }
    }
}
