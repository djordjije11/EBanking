using EBanking.Console.Model;
using EBanking.Console.Validations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Validations
{
    internal class Validation<T> where T : Entity
    {
        public T? Entity { get; set; }
        public Exception? Exception { get; set; }
        public bool IsValid { get; set; }
        public Validation(Exception exception)
        {
            Exception = exception;
            if (Exception == null) IsValid = true;
            else IsValid = false;
        }

        public Validation(T entity)
        {
            Entity = entity;
            IsValid = true;
        }

        public Validation()
        {
            IsValid = true;
        }
    }
}
