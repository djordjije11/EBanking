using EBanking.Models;

namespace EBanking.BusinessLayer.Interfaces
{
    public interface ITransactionLogic
    {
        Task<Transaction> AddTransactionAsync(decimal amount, string fromAccountNumber, string toAccountNumber, DateTime? date = null);
        Task<Transaction> FindTransactionAsync(int transactionId);
        Task<List<Transaction>> GetAllTransactionsAsync();
    }
}
