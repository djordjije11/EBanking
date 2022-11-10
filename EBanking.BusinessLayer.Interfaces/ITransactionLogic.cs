using EBanking.Models;

namespace EBanking.BusinessLayer.Interfaces
{
    public interface ITransactionLogic
    {
        Task<Transaction> AddTransactionAsync(decimal amount, DateTime date, string fromAccountNumber, string toAccountNumber);
        Task<Transaction> FindTransactionAsync(int transactionId);
        Task<List<Transaction>> GetAllTransactionsAsync();
    }
}
