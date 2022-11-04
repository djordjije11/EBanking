using EBanking.Console.Models;
using EBanking.Console.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Validations.Impl
{
    internal class TransactionValidator : IValidator<Transaction>
    {
        public void Validate(Transaction entity)
        {
            //ne znam sta bih dodao kao proveru
        }
    }
}
