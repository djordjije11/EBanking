using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using Microsoft.Extensions.DependencyInjection;
using SqliteDataAccess.SqliteConnectors;
using SqliteDataAccess.SqliteModels;
using System.Data.SQLite;

namespace SqliteDataAccess.SqliteBrokers
{
    public abstract class SqliteEntityBroker : IBroker
    {
        //protected readonly SqliteConnector connector = SqliteConnector.GetInstance();
        protected readonly SqliteConnector connector;
        public IServiceProvider ServiceProvider { get; }
        public SqliteEntityBroker(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            connector = (SqliteConnector)ServiceProvider.GetRequiredService<IConnector>();
        }
        public async Task StartConnectionAsync()
        {
            await connector.StartConnectionAsync();
        }
        public async Task StartTransactionAsync()
        {
            await connector.StartTransactionAsync();
        }
        public void StartCommand()
        {
            connector.StartCommand();
        }
        public async Task CommitTransactionAsync()
        {
            await connector.CommitTransactionAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await connector.RollbackTransactionAsync();
        }
        public async Task EndConnectionAsync()
        {
            await connector.EndConnectionAsync();
        }
        public bool IsConnected() => connector.IsConnected();
        public string GetBrokerName()
        {
            return "SQLite";
        }
        protected async Task<IEntity> CreateEntityAsync(SqliteEntity sqliteEntity)
        {
            IEntity entity = sqliteEntity.Entity;
            connector.StartCommand();
            SQLiteCommand command = connector.GetCommand();
            sqliteEntity.SetSqliteInsertCommand(command);
            int? id = (int?)(await command.ExecuteScalarAsync());
            if (id.HasValue == false)
                throw new Exception($"Error creating {entity.GetClassName()}.");
            entity.SetIdentificator(id.Value);
            return entity;
        }
        protected async Task<IEntity> UpdateEntityByIdAsync(SqliteEntity sqliteEntity)
        {
            IEntity entity = sqliteEntity.Entity;
            connector.StartCommand();
            SQLiteCommand command = connector.GetCommand();
            sqliteEntity.SetSqliteUpdateByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            return entity;
        }
        protected async Task<IEntity> DeleteEntityAsync(SqliteEntity sqliteEntity)
        {
            IEntity entity = sqliteEntity.Entity;
            connector.StartCommand();
            SQLiteCommand command = connector.GetCommand();
            sqliteEntity.SetSqliteDeleteByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            return entity;
        }
        protected async Task<IEntity?> GetEntityByIdAsync(SqliteEntity sqliteEntity)
        {
            IEntity entity = sqliteEntity.Entity;
            connector.StartCommand();
            SQLiteCommand command = connector.GetCommand();
            sqliteEntity.SetSqliteSelectByIdCommand(command);
            SQLiteDataReader reader = (SQLiteDataReader)await command.ExecuteReaderAsync();
            IEntity? wantedEntity = null;
            if (await reader.ReadAsync())
            {
                wantedEntity = sqliteEntity.GetEntityFromSqliteReader(reader);
            }
            await reader.CloseAsync();
            return wantedEntity;
        }
        protected async Task<List<IEntity>> GetAllEntitiesAsync(SqliteEntity sqliteEntity)
        {
            IEntity entity = sqliteEntity.Entity;
            var entities = new List<IEntity>();
            connector.StartCommand();
            var command = connector.GetCommand();
            sqliteEntity.SetSqliteSelectAllCommand(command);
            SQLiteDataReader reader = (SQLiteDataReader)await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                entities.Add(sqliteEntity.GetEntityFromSqliteReader(reader));
            }
            await reader.CloseAsync();
            return entities;
        }
    }
}