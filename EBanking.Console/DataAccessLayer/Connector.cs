using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.DataAccessLayer
{
    public class Connector
    {
        public const string CONNECTION_STRING =
            @"Data Source=DESKTOP-A2R6AE6\SQLEXPRESS;Initial Catalog=EBankingDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection;
        SqlTransaction? transaction;
        SqlCommand? command;
        public Connector()
        {
            connection = new SqlConnection(CONNECTION_STRING);
        }
        public async Task StartConnection()
        {
            await connection.OpenAsync();
        }
        public async Task StartTransaction()
        {
            transaction = (SqlTransaction)(await connection.BeginTransactionAsync());
        }
        public void StartCommand()
        {
            command = connection.CreateCommand();
            command.Transaction = transaction;
        }
        public async Task CommitTransaction()
        {
            await transaction.CommitAsync();
        }
        public async Task RollbackTransaction()
        {
            await transaction.RollbackAsync();
        }
        public async Task EndConnection()
        {
            await connection.CloseAsync();
        }
        public SqlConnection GetConnection() => connection;
        public SqlTransaction GetTransaction() => transaction;
        public SqlCommand GetCommand() => command;
    }
}
