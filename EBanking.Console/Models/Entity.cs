using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Model
{
    public abstract class Entity
    {
        public abstract int GetIdentificator();
        public abstract void SetIdentificator(int id);
        public string GetClassName()
        {
            return GetType().Name;
        }
        public abstract void SetInsertEntityCommand(SqlCommand command);
        public abstract Entity GetEntity(SqlDataReader reader);
        public virtual void SetSelectByIdCommand(SqlCommand command)
        {
            command.CommandText = $"select * from [dbo].[{GetClassName()}] where id={GetIdentificator()}";
        }
        public virtual void SetSelectAllCommand(SqlCommand command)
        {
            command.CommandText = $"select * from [dbo].[{GetClassName()}]";
        }
        public void SetDeleteByIdCommand(SqlCommand command)
        {
            command.CommandText = $"delete from [dbo].[{GetClassName()}] where id={GetIdentificator()}";
        }
        public abstract void SetUpdateByIdCommand(SqlCommand command);
    }
}
