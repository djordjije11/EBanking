using AutoMapper;
using EBanking.API.DTO.TransactionDtos;
using EBanking.Models;

namespace EBanking.API.DTO.TransactionProfiles
{
    internal class TransactionDtoProfile : Profile
    {
        public TransactionDtoProfile()
        {
            CreateMap<Transaction, TransactionDto>().
                ReverseMap();
        }
    }
}
