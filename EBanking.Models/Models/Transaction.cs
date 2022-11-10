namespace EBanking.Models
{
    public class Transaction : IEntity
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
        public string SinglePrint()
        {
            return $"\nИД: {Id}\nИзнос: {Amount}\nДатум: {Date}\nДавалац: {FromAccount}\nПрималац: {ToAccount}";
        }
    }
}