using AutoMapper;
using EBanking.API.DTO.UserDtos;
using EBanking.Models;

namespace EBanking.API.DTO.UserProfiles
{
    public class GetUserDtoProfile : Profile
    {
        public GetUserDtoProfile()
        {
            CreateMap<User, GetUserDto>();
        }
    }
}
