using EBanking.BusinessLayer.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.Validation.Validators;

namespace EBanking.BusinessLayer
{
    public class TransactionLogic : ITransactionLogic
    {
        IBroker Broker { get; }
        public TransactionLogic(IBroker broker)
        {
            Broker = broker;
        }

        public async Task<Transaction> AddTransactionAsync(decimal amount, DateTime date, string fromAccountNumber, string toAccountNumber)
        {
            try
            {
                await Broker.StartConnectionAsync();
                Account? fromAccount = await Broker.GetAccountByNumber(new Account() { Number = fromAccountNumber });
                if (fromAccount == null)
                    throw new Exception($"Рачун са бројем: '{fromAccountNumber}' није пронађен.");
                Account? toAccount = await Broker.GetAccountByNumber(new Account() { Number = toAccountNumber });
                if (toAccount == null)
                    throw new Exception($"Рачун са бројем: '{toAccountNumber}' није пронађен.");
                if (fromAccount.Status == AccountStatus.INACTIVE || toAccount.Status == AccountStatus.INACTIVE)
                    throw new Exception($"Рачуни морају бити активни да би вршили трансакције.");
                if (fromAccount.Currency.Equals(toAccount.Currency) == false)
                    throw new Exception($"Рачуни морају бити исте валуте.");
                var transaction = new Transaction() { Amount = amount, Date = date, FromAccount = fromAccount, ToAccount = toAccount };
                var resultInfo = new TransactionValidator(transaction).Validate();
                if (resultInfo.IsValid == false)
                    throw new Exception(resultInfo.GetErrorsString());
                await Broker.StartTransactionAsync();
                var createdTransaction = await Broker.CreateTransactionAsync(transaction);
                await Broker.CommitTransactionAsync();
                return createdTransaction;
            }
            catch
            {
                await Broker.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
        public async Task<Transaction> FindTransactionAsync(int transactionId)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var transaction = await Broker.GetTransactionByIdAsync(new Transaction() { Id = transactionId});
                if (transaction == null)
                    throw new Exception($"Трансакција са идентификатором: '{transactionId}' није пронађена.");
                return transaction;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            try
            {
                await Broker.StartConnectionAsync();
                return await Broker.GetAllTransactionsAsync(new Transaction());
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
    }
}
