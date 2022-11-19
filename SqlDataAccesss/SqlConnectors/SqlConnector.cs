using EBanking.ConfigurationManager.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using System.Data;
using System.Data.SqlClient;

namespace EBanking.SqlDataAccess.SqlConnectors
{
    public class SqlConnector : IConnector
    {
        /*
        private const string CONNECTION_STRING =
            @"Data Source=DESKTOP-A2R6AE6\SQLEXPRESS;Initial Catalog=EBankingDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        */
        public IConfigurationManager ConfigurationManager { get; }
        public ILogger Logger { get; }
        private readonly SqlConnection connection;
        private SqlTransaction? transaction;
        private SqlCommand? command;
        //private static SqlConnector connector;
        public SqlConnector(IConfigurationManager configurationManager, ILogger logger)
        {
            ConfigurationManager = configurationManager;
            Logger = logger;
            connection = new SqlConnection(ConfigurationManager.GetConfigParam(ConfigParamKeys.CONNECTION_STRING));
        }
        /*
        private SqlConnector()
        {
            connection = new SqlConnection(CONNECTION_STRING);
        }
        */
        /*
        public static SqlConnector GetInstance()
        {
            if(connector == null)
            {
                connector = new SqlConnector();
            }
            return connector;
        }
        */
        public async Task StartConnectionAsync()
        {
            await connection.OpenAsync();
        }
        public async Task EndConnectionAsync()
        {
            await connection.CloseAsync();
        }
        public async Task StartTransactionAsync()
        {
            transaction = (SqlTransaction)(await connection.BeginTransactionAsync());
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
        public void StartCommand()
        {
            command = connection.CreateCommand();
            if (transaction != null)
                command.Transaction = transaction;
        }
        public bool IsConnected() => connection.State == ConnectionState.Open;
        public SqlConnection GetConnection() => connection;
        public SqlTransaction? GetTransaction() => transaction;
        public SqlCommand? GetCommand() => command;
    }
}
