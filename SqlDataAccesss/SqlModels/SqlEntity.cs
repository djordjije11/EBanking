using EBanking.Models;
using System.Data.SqlClient;

namespace SqlDataAccesss.SqlModels
{
    public abstract class SqlEntity
    {
        public IEntity Entity { get; set; }
        protected SqlEntity(IEntity entity) { Entity = entity; }
        protected SqlEntity() { }
        public abstract string GetTableName();
        public abstract IEntity GetEntityFromSqlReader(SqlDataReader reader);
        public abstract void SetSqlInsertCommand(SqlCommand command);
        public abstract void SetSqlUpdateByIdCommand(SqlCommand command);
        public abstract void SetSqlDeleteByIdCommand(SqlCommand command);
        public abstract void SetSqlSelectByIdCommand(SqlCommand command);
        public abstract void SetSqlSelectAllCommand(SqlCommand command);
    }
}
