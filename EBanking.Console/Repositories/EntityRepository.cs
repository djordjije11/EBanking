using EBanking.Console.DataAccessLayer;
using EBanking.Console.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Repositories
{
    internal class EntityRepository
    {
        public async Task<IEntity> CreateEntity(IEntity entity, Connector connector)
        {
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            entity.SetInsertCommand(command);
            int? id = (int?)(await command.ExecuteScalarAsync());
            if (id.HasValue == false)
                throw new Exception($"Error creating {entity.GetClassName()}.");
            entity.SetIdentificator(id.Value);
            return entity;
        }
        public async Task<IEntity> UpdateEntityById(IEntity entity, Connector connector)
        {
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            entity.SetUpdateByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            return entity;
        }
        public async Task<IEntity> DeleteEntity(IEntity entity, Connector connector)
        {
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            entity.SetDeleteByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            return entity;
        }
        public async Task<IEntity?> GetEntityById(IEntity entity, Connector connector)
        {
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            entity.SetSelectByIdCommand(command);
            var reader = await command.ExecuteReaderAsync();
            IEntity? wantedEntity = null;
            if (await reader.ReadAsync())
            {
                wantedEntity = entity.GetEntityFromReader(reader);
            }
            await reader.CloseAsync();
            return wantedEntity;
        }
        public async Task<List<IEntity>> GetAllEntities(IEntity entity, Connector connector)
        {
            var entities = new List<IEntity>();
            connector.StartCommand();
            var command = connector.GetCommand();
            entity.SetSelectAllCommand(command);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                entities.Add(entity.GetEntityFromReader(reader));
            }
            await reader.CloseAsync();
            return entities;
        }
    }
}
