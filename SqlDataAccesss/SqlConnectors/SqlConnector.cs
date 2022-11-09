using EBanking.DataAccessLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace EBanking.SqlDataAccess.SqlConnectors
{
    public class SqlConnector : IConnector
    {
        private const string CONNECTION_STRING =
            @"Data Source=DESKTOP-A2R6AE6\SQLEXPRESS;Initial Catalog=EBankingDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection connection;
        private SqlTransaction? transaction;
        private SqlCommand? command;
        private static SqlConnector connector;
        private SqlConnector()
        {
            connection = new SqlConnection(CONNECTION_STRING);
        }
        public static SqlConnector GetInstance()
        {
            if(connector == null)
            {
                connector = new SqlConnector();
            }
            return connector;
        }
        public async Task StartConnectionAsync()
        {
            await connection.OpenAsync();
        }
        public async Task StartTransactionAsync()
        {
            transaction = (SqlTransaction)(await connection.BeginTransactionAsync());
        }
        public void StartCommand()
        {
            command = connection.CreateCommand();
            if(transaction != null)
                command.Transaction = transaction;
        }
        public async Task CommitTransactionAsync()
        {
            if(transaction != null)
                await transaction.CommitAsync();
            transaction = null;
        }
        public async Task RollbackTransactionAsync()
        {
            if(transaction != null)
                await transaction.RollbackAsync();
            transaction = null;
        }
        public async Task EndConnectionAsync()
        {
            await connection.CloseAsync();
        }
        public bool IsConnected() => connection.State == ConnectionState.Open;
        public SqlConnection GetConnection() => connection;
        public SqlTransaction GetTransaction() => transaction;
        public SqlCommand GetCommand() => command;
    }
}
