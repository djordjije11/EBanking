﻿namespace EBanking.API.DTO.UserDtos
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
