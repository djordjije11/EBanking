using EBanking.Console.Models;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EBanking.Console.Validations.Impl
{
    internal class AccountValidator : IValidator<Account>
    {
        public void Validate(Account entity)
        {
            ValidateStatus(entity.Status);
            //ValidateNumber(entity.Number);    nisam siguran koje je ogranicenje za broj racuna
        }

        void ValidateStatus(Status? status)
        {
            if (status == null) throw new ValidationException("Статус рачуна није постављен.");
            if (!status.Equals(Status.ACTIVE) && !status.Equals(Status.INACTIVE))
                throw new ValidationException("Статус рачуна може бити или активан или неактиван.");
        }
        void ValidateNumber(string number)
        {
            Regex reg = new Regex(@"^Acc(?:oun)?t(?:\s + Number)?.+[\d -] +$");
            if (!reg.IsMatch(number)) throw new ValidationException("Неадекватно унет број рачуна.");
        }
    }
}
