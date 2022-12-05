using AutoMapper;
using EBanking.API.DTO.AccountDtos;
using EBanking.API.DTO.UserDtos;
using EBanking.BusinessLayer.Interfaces;
using EBanking.Services.Interfaces;

namespace EBanking.Services.LogicServices
{
    public class UserLogicService : IUserService
    {
        private readonly IUserLogic userLogic;
        public IMapper Mapper { get; }

        public UserLogicService(IUserLogic userLogic, IMapper mapper)
        {
            this.userLogic = userLogic;
            Mapper = mapper;
        }
        public async Task<GetUserDto?> AddUserAsync(string firstName, string lastName, string email, string password)
        {
            return Mapper.Map<GetUserDto>(await userLogic.AddUserAsync(firstName, lastName, email, password));
        }

        public async Task<GetUserDto?> DeleteUserAsync(int id, string password)
        {
            return Mapper.Map<GetUserDto>(await userLogic.DeleteUserAsync(id, password));
        }

        public async Task<IEnumerable<GetAccountDto>?> GetAccountsFromUserAsync(int userID)
        {
            return Mapper.Map<IEnumerable<GetAccountDto>>(await userLogic.GetAccountsByUserAsync(userID));
        }

        public async Task<IEnumerable<GetUserDto>?> GetAllUsersAsync()
        {
            return Mapper.Map<IEnumerable<GetUserDto>>(await userLogic.GetAllUsersAsync());
        }

        public async Task<GetUserDto?> GetUserAsync(int id)
        {
            return Mapper.Map<GetUserDto>(await userLogic.FindUserAsync(id));
        }

        public async Task<GetUserDto?> UpdateUserAsync(int id, string email, string oldPassword, string newPassword)
        {
            return Mapper.Map<GetUserDto>(await userLogic.UpdateUserAsync(id, email, oldPassword, newPassword));
        }
    }
}
