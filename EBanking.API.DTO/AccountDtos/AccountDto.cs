using EBanking.Models;

namespace EBanking.API.DTO.AccountDtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public AccountStatus Status { get; set; }
    }
}