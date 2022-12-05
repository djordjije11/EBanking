namespace EBanking.API.DTO.UserDtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
