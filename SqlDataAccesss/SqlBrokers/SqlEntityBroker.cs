using EBanking.Models;
using EBanking.SqlDataAccess.SqlConnectors;
using SqlDataAccesss.SqlModels;
using System.Data.SqlClient;

namespace EBanking.SqlDataAccess.SqlBrokers
{
    public abstract class SqlEntityBroker
    {
        protected readonly SqlConnector connector = SqlConnector.GetInstance();
        protected async Task<IEntity> CreateEntityAsync(IEntity entity)
        {
            ISqlEntity sqlEntity = (ISqlEntity) entity;
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            sqlEntity.SetSqlInsertCommand(command);
            int? id = (int?)(await command.ExecuteScalarAsync());
            if (id.HasValue == false)
                throw new Exception($"Error creating {entity.GetClassName()}.");
            entity.SetIdentificator(id.Value);
            return entity;
        }
        protected async Task<IEntity> UpdateEntityByIdAsync(IEntity entity)
        {
            ISqlEntity sqlEntity = (ISqlEntity)entity;
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            sqlEntity.SetSqlUpdateByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            return entity;
        }
        protected async Task<IEntity> DeleteEntityAsync(IEntity entity)
        {
            ISqlEntity sqlEntity = (ISqlEntity)entity;
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            sqlEntity.SetSqlDeleteByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            return entity;
        }
        protected async Task<IEntity?> GetEntityByIdAsync(IEntity entity)
        {
            ISqlEntity sqlEntity = (ISqlEntity)entity;
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
        protected async Task<List<IEntity>> GetAllEntitiesAsync(IEntity entity)
        {
            ISqlEntity sqlEntity = (ISqlEntity)entity;
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
