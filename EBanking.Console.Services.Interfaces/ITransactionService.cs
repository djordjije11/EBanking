using EBanking.API.DTO.TransactionDtos;

namespace EBanking.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>?> GetAllTransactionsAsync();
        Task<TransactionDto?> GetTransactionAsync(int id);
        Task<TransactionDto?> AddTransactionAsync(decimal amount, string fromAccountNumber, string toAccountNumber);
    }
}
