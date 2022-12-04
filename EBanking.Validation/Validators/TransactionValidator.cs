using EBanking.Models;
using FluentValidation;
using System.Net.Http.Headers;

namespace EBanking.Validation.Validators
{
    public class TransactionValidator : AbstractValidator
    {
        public TransactionValidator(Transaction transaction)
        {
            
        }
    }
}
