using EBanking.Models;
using System.Data.SqlClient;

namespace SqlDataAccesss.SqlModels
{
    public interface ISqlEntity : IEntity
    {
        string GetTableName();
        IEntity GetEntityFromSqlReader(SqlDataReader reader);
        void SetSqlInsertCommand(SqlCommand command);
        void SetSqlUpdateByIdCommand(SqlCommand command);
        void SetSqlDeleteByIdCommand(SqlCommand command);
        void SetSqlSelectByIdCommand(SqlCommand command);
        void SetSqlSelectAllCommand(SqlCommand command);
    }
}
