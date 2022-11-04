using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EBanking.Console.Model
{
    public class Currency : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
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
            return new Currency()
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                CurrencyCode = reader.GetString("Code")
            };
        }

        public override void SetInsertEntityCommand(SqlCommand command)
        {
            command.CommandText = "insert into [dbo].[Currency](Name, Code) output inserted.ID values (@name, @code)";
            command.Parameters.AddWithValue("@name", this.Name);
            command.Parameters.AddWithValue("@code", this.CurrencyCode);
        }
        public override string ToString()
        {
            return CurrencyCode;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Currency currency)
            {
                return currency.Id == this.Id;
            }
            else return false;
        }
        public override void SetUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE [dbo].[Currency] SET Name = '{Name}', Code = '{CurrencyCode}' WHERE Id = {Id}";
        }

        public override string SinglePrint()
        {
            return $"\nИд: {Id}\nИме валуте: {Name}\nКод валуте: {CurrencyCode}";
        }
    }
}
