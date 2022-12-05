using EBanking.API.DTO.TransactionDtos;
using EBanking.Services.HttpClients;
using EBanking.Services.Interfaces;

namespace EBanking.Services.APIServices
{
    public class TransactionAPIService : ITransactionService
    {
        private readonly ITransactionHttpClient transactionHttpClient;

        public TransactionAPIService(ITransactionHttpClient transactionHttpClient)
        {
            this.transactionHttpClient = transactionHttpClient;
        }
        public async Task<TransactionDto?> AddTransactionAsync(decimal amount, string fromAccountNumber, string toAccountNumber)
        {
            return await transactionHttpClient.PostAsync(amount, fromAccountNumber, toAccountNumber);
        }

        public async Task<IEnumerable<TransactionDto>?> GetAllTransactionsAsync()
        {
            return await transactionHttpClient.GetAsync();
        }

        public async Task<TransactionDto?> GetTransactionAsync(int id)
        {
            return await transactionHttpClient.GetAsync(id);
        }
    }
}
