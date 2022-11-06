using System.Data.SqlClient;

namespace EBanking.Console.Model
{
    public interface IEntity
    {
        public abstract int GetIdentificator();
        public abstract void SetIdentificator(int id);
        public string GetClassName()
        {
            return GetType().Name;
        }
        public string GetTableName();
        public abstract string SinglePrint();
        public abstract IEntity GetEntityFromReader(SqlDataReader reader);
        public abstract void SetInsertCommand(SqlCommand command);
        public abstract void SetUpdateByIdCommand(SqlCommand command);
        public void SetDeleteByIdCommand(SqlCommand command);
        public void SetSelectByIdCommand(SqlCommand command);
        public void SetSelectAllCommand(SqlCommand command);
    }
}
