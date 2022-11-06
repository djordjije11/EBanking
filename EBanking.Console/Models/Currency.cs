using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EBanking.Console.Model
{
    public class Currency : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj is Currency currency)
            {
                return currency.Id == this.Id;
            }
            else return false;
        }
        public override string ToString() { return CurrencyCode; }
        public int GetIdentificator() { return Id; }
        public void SetIdentificator(int id) { Id = id; }
        public string GetTableName() { return "[dbo].[Currency]"; }
        public string SinglePrint()
        {
            return $"\nИд: {Id}\nИме валуте: {Name}\nКод валуте: {CurrencyCode}";
        }
        public IEntity GetEntityFromReader(SqlDataReader reader)
        {
            return new Currency()
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                CurrencyCode = reader.GetString("Code")
            };
        }
        public void SetInsertCommand(SqlCommand command)
        {
            command.CommandText = $"insert into {GetTableName()}(Name, Code) output inserted.ID values (@name, @code)";
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@code", CurrencyCode);
        }
        public void SetUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET Name = '{Name}', Code = '{CurrencyCode}' WHERE Id = {Id}";
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
