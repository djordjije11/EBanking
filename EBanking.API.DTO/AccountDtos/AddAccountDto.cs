using EBanking.Models;

namespace EBanking.API.DTO.AccountDtos
{
    public class AddAccountDto
    {
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
    }
}