using EBanking.Models;

namespace EBanking.API.DTO.AccountDtos
{
    public class GetAccountDto
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public string Number { get; set; }
        public int UserID { get; set; }
        public string CurrencyCode { get; set; }
    }
}
