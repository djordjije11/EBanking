using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EBanking.Models
{
    public enum AccountStatus
    {
        [Display(Name = "Активан")] ACTIVE = 1,
        [Display(Name = "Неактиван")] INACTIVE = 2
    }
    public class Account : IEntity
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public string Number { get; set; }
        public User User { get; set; }
        public Currency Currency { get; set; }
        public override string ToString()
        {
            return $"{Number}: {Balance}{Currency}, {User}";
        }
        public int GetIdentificator() { return Id; }
        public void SetIdentificator(int id) { Id = id; }
        public string SinglePrint()
        {
            return $"\nИД: {Id}\nСтање: {Balance}\nСтатус рачуна: {Status.ToString().ToLower()}\nБрој рачуна: {Number}\nКорисник: {User}\nВалута: {Currency}";
        }
        public static string GenerateAccountNumber()
        {
            var random = new Random();
            var startNumbers = random.Next(0, 999).ToString().PadLeft(3, '0');
            var firstPartNumbers = random.Next(0, 999999999).ToString().PadLeft(9, '0');
            var secondPartNumbers = random.Next(0, 9999).ToString().PadRight(4, '0');
            var controlNumbers = random.Next(0, 2).ToString().PadLeft(2, '0');
            return $"{startNumbers}-{firstPartNumbers}{secondPartNumbers}-{controlNumbers}";
        }
    }
}
