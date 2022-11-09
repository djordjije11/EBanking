using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EBanking.Models
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
        
        public string SinglePrint()
        {
            return $"\nИД: {Id}\nИме: {FirstName}\nПрезиме: {LastName}\nЕмаил: {Email}";
        }
    }
}
