using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.SqlDataAccess.SqlConnectors;
using SqlDataAccesss.SqlModels;
using System.Data.SqlClient;

namespace EBanking.SqlDataAccess.SqlBrokers
{
    public abstract class SqlEntityBroker : IBroker
    {
        protected readonly SqlConnector connector = SqlConnector.GetInstance();

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
            return "SQL";
        }
        protected async Task<IEntity> CreateEntityAsync(SqlEntity sqlEntity)
        {
            IEntity entity = sqlEntity.Entity;
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            sqlEntity.SetSqlInsertCommand(command);
            int? id = (int?)(await command.ExecuteScalarAsync());
            if (id.HasValue == false)
                throw new Exception($"Error creating {entity.GetClassName()}.");
            entity.SetIdentificator(id.Value);
            return entity;
        }
        protected async Task<IEntity> UpdateEntityByIdAsync(SqlEntity sqlEntity)
        {
            IEntity entity = sqlEntity.Entity;
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            sqlEntity.SetSqlUpdateByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            return entity;
        }
        protected async Task<IEntity> DeleteEntityAsync(SqlEntity sqlEntity)
        {
            IEntity entity = sqlEntity.Entity;
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            sqlEntity.SetSqlDeleteByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            return entity;
        }
        protected async Task<IEntity?> GetEntityByIdAsync(SqlEntity sqlEntity)
        {
            IEntity entity = sqlEntity.Entity;
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            sqlEntity.SetSqlSelectByIdCommand(command);
            var reader = await command.ExecuteReaderAsync();
            IEntity? wantedEntity = null;
            if (await reader.ReadAsync())
            {
                wantedEntity = sqlEntity.GetEntityFromSqlReader(reader);
            }
            await reader.CloseAsync();
            return wantedEntity;
        }
        protected async Task<List<IEntity>> GetAllEntitiesAsync(SqlEntity sqlEntity)
        {
            IEntity entity = sqlEntity.Entity;
            var entities = new List<IEntity>();
            connector.StartCommand();
            var command = connector.GetCommand();
            sqlEntity.SetSqlSelectAllCommand(command);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                entities.Add(sqlEntity.GetEntityFromSqlReader(reader));
            }
            await reader.CloseAsync();
            return entities;
        }
    }
}