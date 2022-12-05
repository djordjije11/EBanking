using AutoMapper;
using EBanking.API.DTO.AccountDtos;
using EBanking.Models;

namespace EBanking.API.DTO.AccountProfiles
{
    public class GetAccountDtoProfile : Profile
    {
        public GetAccountDtoProfile()
        {
            CreateMap<Account, GetAccountDto>();
        }
    }
}
