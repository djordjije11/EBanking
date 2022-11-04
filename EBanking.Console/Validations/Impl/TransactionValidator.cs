using EBanking.Console.Model;
using EBanking.Console.Models;
using EBanking.Console.Validations.Exceptions;
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
            ValidateAccountStatus(entity.FromAccount.Status);
            ValidateAccountStatus(entity.ToAccount.Status);
            ValidateCurrencies(entity.FromAccount.Currency, entity.ToAccount.Currency);
        }
        void ValidateAccountStatus(Status status)
        {
            if (!status.Equals(Status.ACTIVE)) throw new ValidationException("Рачун мора бити активан да би вршио трансакцију!");
        }
        void ValidateCurrencies(Currency fromCurrency, Currency toCurrency)
        {
            if (!fromCurrency.Equals(toCurrency)) throw new ValidationException("Рачуни морају бити исте валуте!");
        }
    }
}
