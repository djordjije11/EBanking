using EBanking.Console.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Validations.Interfaces
{
    public interface IValidator<T> where T : IEntity
    {
        void Validate(T entity);
    }
}
