using EBanking.ConfigurationManager.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using System.Data.SQLite;

namespace SqliteDataAccess.SqliteConnectors
{
    public class SqliteConnector : IConnector
    {
        /*
        private const string CONNECTION_STRING = @"Data Source=c:\\EBankingDatabase.db;Version=3;";
        */
        public IConfigurationManager ConfigurationManager { get; }
        public ILogger Logger { get; }
        private readonly SQLiteConnection connection;
        private SQLiteTransaction? transaction;
        private SQLiteCommand? command;
        public SqliteConnector(IConfigurationManager configurationManager, ILogger logger)
        {
            ConfigurationManager = configurationManager;
            Logger = logger;
            connection = new SQLiteConnection(ConfigurationManager.GetConfigParam(ConfigParamKeys.CONNECTION_STRING));
        }
        //private static SqliteConnector connector;
        /*
        private SqliteConnector()
        {
            connection = new SQLiteConnection(CONNECTION_STRING);
        }
        */
        /*
        public static SqliteConnector GetInstance()
        {
            if (connector == null)
                connector = new SqliteConnector();
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
            transaction = (SQLiteTransaction)(await connection.BeginTransactionAsync());
        }
        public async Task CommitTransactionAsync()
        {
            if (transaction != null)
                await transaction.CommitAsync();
            transaction = null;
        }
        public async Task RollbackTransactionAsync()
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            transaction = null;
        }
        public void StartCommand()
        {
            command = connection.CreateCommand();
            if (transaction != null)
                command.Transaction = transaction;
        }
        public bool IsConnected() => connection.State == System.Data.ConnectionState.Open;
        public SQLiteConnection GetConnection() => connection;
        public SQLiteTransaction GetTransaction() => transaction;
        public SQLiteCommand GetCommand() => command;
    }
}
