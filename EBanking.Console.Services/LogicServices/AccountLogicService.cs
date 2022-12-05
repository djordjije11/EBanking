using AutoMapper;
using EBanking.API.DTO.AccountDtos;
using EBanking.API.DTO.TransactionDtos;
using EBanking.BusinessLayer.Interfaces;
using EBanking.Models;
using EBanking.Services.Interfaces;

namespace EBanking.Services.LogicServices
{
    public class AccountLogicService : IAccountService
    {
        private readonly IAccountLogic accountLogic;
        public IMapper Mapper { get; }

        public AccountLogicService(IAccountLogic accountLogic, IMapper mapper)
        {
            this.accountLogic = accountLogic;
            Mapper = mapper;
        }

        public async Task<GetAccountDto> AddAccountAsync(int userID, int currencyID)
        {
            return Mapper.Map<GetAccountDto>(await accountLogic.AddAccountAsync(userID, currencyID));
        }

        public async Task<GetAccountDto> DeleteAccountAsync(int id)
        {
            return Mapper.Map<GetAccountDto>(await accountLogic.RemoveAccountAsync(id));
        }

        public async Task<GetAccountDto> GetAccountAsync(int id)
        {
            return Mapper.Map<GetAccountDto>(await accountLogic.FindAccountAsync(id));
        }

        public async Task<IEnumerable<GetAccountDto>> GetAllAccountsAsync()
        {
            return Mapper.Map<IEnumerable<GetAccountDto>>(await accountLogic.GetAllAccountsAsync());
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsFromAccountAsync(int accountID)
        {
            return Mapper.Map<IEnumerable<TransactionDto>>(await accountLogic.GetTransactionsByAccount(accountID));
        }

        public async Task<GetAccountDto> UpdateAccountAsync(int id, AccountStatus accountStatus)
        {
            return Mapper.Map<GetAccountDto>(await accountLogic.UpdateAccountAsync(id, accountStatus));
        }
    }
}
