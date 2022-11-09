using System.Data.SqlClient;

namespace EBanking.Models
{
    public interface IEntity
    {
        int GetIdentificator();
        void SetIdentificator(int id);
        string GetClassName()
        {
            return GetType().Name;
        }
        string SinglePrint();
    }
}
