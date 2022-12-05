namespace EBanking.API.DTO.TransactionDtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
    }
}
