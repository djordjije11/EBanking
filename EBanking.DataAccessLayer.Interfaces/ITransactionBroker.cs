using EBanking.Models;

namespace EBanking.DataAccessLayer.Interfaces
{
    public interface ITransactionBroker
    {
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<Transaction> UpdateTransactionByIdAsync(Transaction transaction);
        Task<Transaction> DeleteTransactionAsync(Transaction transaction);
        Task<Transaction?> GetTransactionByIdAsync(Transaction transaction);
        Task<List<Transaction>> GetAllTransactionsAsync(Transaction transaction);
    }
}
