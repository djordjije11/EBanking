using EBanking.BusinessLayer.Interfaces;
using EBanking.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.AppControllers
{
    public class TransactionController
    {
        public TransactionController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public IServiceProvider ServiceProvider { get; }
        public async Task<Transaction> CreateTransactionAsync(decimal amount, DateTime date, string fromAccountNumber, string toAccountNumber)
        {
            var transactionLogic = ServiceProvider.GetRequiredService<ITransactionLogic>();
            return await transactionLogic.AddTransactionAsync(amount, date, fromAccountNumber, toAccountNumber);
        }
        public async Task<Transaction> ReadTransactionAsync(int transactionId)
        {
            var transactionLogic = ServiceProvider.GetRequiredService<ITransactionLogic>();
            return await transactionLogic.FindTransactionAsync(transactionId);
        }
        public async Task<List<Transaction>> ReadAllTransactionsAsync()
        {
            var transactionLogic = ServiceProvider.GetRequiredService<ITransactionLogic>();
            return await transactionLogic.GetAllTransactionsAsync();
        }
    }
}