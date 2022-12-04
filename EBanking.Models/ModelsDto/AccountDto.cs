namespace EBanking.Models.ModelsDto
{
    public class AccountDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public AccountStatus Status { get; set; }
    }
}
