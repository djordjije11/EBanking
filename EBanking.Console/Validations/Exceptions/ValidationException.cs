using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Validations.Exceptions
{
    internal class ValidationException : Exception
    {
        public ValidationException(string message) : base("Унети податак није валидан!\n" + message) 
        {
        }
    }
}
