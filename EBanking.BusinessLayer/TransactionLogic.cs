using EBanking.BusinessLayer.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.Validation.Validators;

namespace EBanking.BusinessLayer
{
    public class TransactionLogic : ITransactionLogic
    {
        ITransactionBroker TransactionBroker { get; }
        IAccountBroker AccountBroker { get; }
        public TransactionLogic(ITransactionBroker transactionBroker, IAccountBroker accountBroker)
        {
            TransactionBroker = transactionBroker;
            AccountBroker = accountBroker;
        }
        public async Task<Transaction> AddTransactionAsync(decimal amount, string fromAccountNumber, string toAccountNumber, DateTime? date = null)
        {
            DateTime dateTransaction = date ?? DateTime.Now;
            try
            {
                await TransactionBroker.StartConnectionAsync();
                Account? fromAccount = await AccountBroker.GetAccountByNumber(new Account() { Number = fromAccountNumber });
                if (fromAccount == null)
                    throw new Exception($"Рачун са бројем: '{fromAccountNumber}' није пронађен.");
                Account? toAccount = await AccountBroker.GetAccountByNumber(new Account() { Number = toAccountNumber });
                if (toAccount == null)
                    throw new Exception($"Рачун са бројем: '{toAccountNumber}' није пронађен.");
                if (fromAccount.Status == AccountStatus.INACTIVE || toAccount.Status == AccountStatus.INACTIVE)
                    throw new Exception("Рачуни морају бити активни да би вршили трансакције.");
                if (fromAccount.Currency.Equals(toAccount.Currency) == false)
                    throw new Exception("Рачуни морају бити исте валуте.");
                if (fromAccount.Balance < amount)
                    throw new Exception("На стању даваоца нема довољно средстава за ову трансакцију.");
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;
                var transaction = new Transaction() { Amount = amount, Date = dateTransaction, FromAccount = fromAccount, ToAccount = toAccount };
                var resultInfo = new TransactionValidator(transaction).Validate();
                if (resultInfo.IsValid == false)
                    throw new Exception(resultInfo.GetErrorsString());
                await TransactionBroker.StartTransactionAsync();
                await AccountBroker.UpdateAccountByIdAsync(fromAccount);
                await AccountBroker.UpdateAccountByIdAsync(toAccount);
                var createdTransaction = await TransactionBroker.CreateTransactionAsync(transaction);
                await TransactionBroker.CommitTransactionAsync();
                return createdTransaction;
            }
            catch
            {
                await TransactionBroker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await TransactionBroker.EndConnectionAsync();
            }
        }
        public async Task<Transaction> FindTransactionAsync(int transactionId)
        {
            try
            {
                await TransactionBroker.StartConnectionAsync();
                var transaction = await TransactionBroker.GetTransactionByIdAsync(new Transaction() { Id = transactionId});
                if (transaction == null)
                    throw new Exception($"Трансакција са идентификатором: '{transactionId}' није пронађена.");
                return transaction;
            }
            finally
            {
                await TransactionBroker.EndConnectionAsync();
            }
        }
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            try
            {
                await TransactionBroker.StartConnectionAsync();
                return await TransactionBroker.GetAllTransactionsAsync(new Transaction());
            }
            finally
            {
                await TransactionBroker.EndConnectionAsync();
            }
        }
    }
}
