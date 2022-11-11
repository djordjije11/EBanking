using EBanking.Models;
using System.Data.SQLite;

namespace SqliteDataAccess.SqliteModels
{
    public abstract class SqliteEntity
    {
        public IEntity Entity { get; set; }
        protected SqliteEntity(IEntity entity) { Entity = entity; }
        protected SqliteEntity() { }
        public abstract string GetTableName();
        public abstract IEntity GetEntityFromSqliteReader(SQLiteDataReader reader);
        public abstract void SetSqliteInsertCommand(SQLiteCommand command);
        public abstract void SetSqliteUpdateByIdCommand(SQLiteCommand command);
        public abstract void SetSqliteDeleteByIdCommand(SQLiteCommand command);
        public abstract void SetSqliteSelectByIdCommand(SQLiteCommand command);
        public abstract void SetSqliteSelectAllCommand(SQLiteCommand command);
    }
}
