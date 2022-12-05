using AutoMapper;
using EBanking.API.DTO.TransactionDtos;
using EBanking.BusinessLayer.Interfaces;
using EBanking.Services.Interfaces;

namespace EBanking.Services.LogicServices
{
    public class TransactionLogicService : ITransactionService
    {
        private readonly ITransactionLogic transactionLogic;
        public IMapper Mapper { get; }

        public TransactionLogicService(ITransactionLogic transactionLogic, IMapper mapper)
        {
            this.transactionLogic = transactionLogic;
            Mapper = mapper;
        }
        public async Task<TransactionDto?> AddTransactionAsync(decimal amount, string fromAccountNumber, string toAccountNumber)
        {
            return Mapper.Map<TransactionDto>(await transactionLogic.AddTransactionAsync(amount, fromAccountNumber, toAccountNumber));
        }

        public async Task<IEnumerable<TransactionDto>?> GetAllTransactionsAsync()
        {
            return Mapper.Map<IEnumerable<TransactionDto>>(await transactionLogic.GetAllTransactionsAsync());
        }

        public async Task<TransactionDto?> GetTransactionAsync(int id)
        {
            return Mapper.Map<TransactionDto>(await transactionLogic.FindTransactionAsync(id));
        }
    }
}
