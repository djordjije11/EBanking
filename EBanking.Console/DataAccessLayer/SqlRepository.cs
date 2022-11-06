using EBanking.Console.Model;
using System.Data.SqlClient;


namespace EBanking.Console.DataAccessLayer
{
    public class SqlRepository
    {
        public static async Task<Entity> CreateEntity(Entity entity, Connector connector)
        {
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            entity.SetInsertEntityCommand(command);
            int? id = (int?)(await command.ExecuteScalarAsync());
            if (id.HasValue == false)
                throw new Exception($"Error creating {entity.GetClassName()}.");
            entity.SetIdentificator(id.Value);
            return entity;
        }
        public static async Task<List<Entity>> GetAllEntities(Entity entity, Connector connector)
        {
            var entities = new List<Entity>();
            connector.StartCommand();
            var command = connector.GetCommand();
            entity.SetSelectAllCommand(command);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                entities.Add(entity.GetEntity(reader));
            }
            await reader.CloseAsync();
            return entities;
        }
        public static async Task<Entity> DeleteEntity(Entity entity, Connector connector)
        {
            var existingEntity = await GetEntityById(entity, connector);
            if (existingEntity == null)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            entity.SetDeleteByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            return existingEntity;
        }
        public static async Task<Entity?> GetEntityById(Entity entity, Connector connector)
        {
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            entity.SetSelectByIdCommand(command);
            var reader = await command.ExecuteReaderAsync();
            Entity? wantedEntity = null;
            if (await reader.ReadAsync())
            {
                wantedEntity = entity.GetEntity(reader);
            }
            await reader.CloseAsync();
            return wantedEntity;
        }
        public static async Task<Entity> UpdateEntityById(Entity entity, Connector connector)
        {
            connector.StartCommand();
            SqlCommand command = connector.GetCommand();
            entity.SetUpdateByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");
            return entity;
        }
        public static async Task<List<Entity>> GetAllEntitiesByOtherEntity(Entity entity, Connector connector)
        {
            var entities = new List<Entity>();
            connector.StartCommand();
            var command = connector.GetCommand();
            entity.SetSelectAllWhereCommand(command);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                entities.Add(entity.GetEntity(reader));
            }
            await reader.CloseAsync();
            return entities;
        }
    }
}