
namespace EBanking.DataAccessLayer.Interfaces
{
    public interface IBroker : IUserBroker, ICurrencyBroker, IAccountBroker, ITransactionBroker
    {
        string GetBrokerName();
        Task StartConnectionAsync();
        Task EndConnectionAsync();
        Task StartTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        bool IsConnected();
    }
}