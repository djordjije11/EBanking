using EBanking.Console.Model;
using System.Data;
using System.Data.SqlClient;


namespace EBanking.Console.DataAccessLayer
{
    public class SqlRepository
    {
        public const string CONNECTION_STRING =
            @"Data Source=DESKTOP-A2R6AE6\SQLEXPRESS;Initial Catalog=EBankingDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static async Task<Entity> CreateEntity(Entity entity, Connector connector)
        {
            SqlCommand command = connector.GetCommand();
            entity.SetInsertEntityCommand(command);

            int? id = (int?)(await command.ExecuteScalarAsync());

            if (id.HasValue == false)
                throw new Exception($"Error creating {entity.GetClassName()}.");

            entity.SetIdentificator(id.Value);
            return entity;
        }
        public static async Task<List<Entity>> GetAllEntities(Entity entity)
        {
            var entities = new List<Entity>();

            var connection = new SqlConnection(CONNECTION_STRING);

            await connection.OpenAsync();
            var command = connection.CreateCommand();
            entity.SetSelectAllCommand(command);
            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                entities.Add(entity.GetEntity(reader));
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return entities;
        }

        public static async Task<Entity> DeleteEntity(Entity entity)
        {
            var existingEntity = await GetEntityById(entity);
            if (existingEntity == null)
                throw new Exception($"{entity.GetClassName()} does not exist.");

            var connection = new SqlConnection(CONNECTION_STRING);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            entity.SetDeleteByIdCommand(command);

            int affectedCount = await command.ExecuteNonQueryAsync();
         
            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");

            return existingEntity;
        }

        public static async Task<Entity?> GetEntityById(Entity entity)
        {
            var connection = new SqlConnection(CONNECTION_STRING);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            entity.SetSelectByIdCommand(command);

            var reader = await command.ExecuteReaderAsync();

            Entity? wantedEntity = null;

            if (await reader.ReadAsync())
            {
                wantedEntity = entity.GetEntity(reader);
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return wantedEntity;
        }

        public static async Task<Entity> UpdateEntityById(Entity entity)
        {
            var connection = new SqlConnection(CONNECTION_STRING);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            entity.SetUpdateByIdCommand(command);
            int affectedCount = await command.ExecuteNonQueryAsync();

            if (affectedCount == 0)
                throw new Exception($"{entity.GetClassName()} does not exist.");

            return entity;
        }
    }
}
