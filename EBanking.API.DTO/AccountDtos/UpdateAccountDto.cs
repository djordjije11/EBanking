using EBanking.Models;

namespace EBanking.API.DTO.AccountDtos
{
    public class UpdateAccountDto
    {
        public int Id { get; set; }
        public AccountStatus Status { get; set; }
    }
}
