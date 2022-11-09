using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
