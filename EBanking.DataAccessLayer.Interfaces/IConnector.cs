namespace EBanking.DataAccessLayer.Interfaces
{
    public interface IConnector
    {
        Task StartConnectionAsync();
        Task EndConnectionAsync();
        Task StartTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        bool IsConnected();
    }
}
