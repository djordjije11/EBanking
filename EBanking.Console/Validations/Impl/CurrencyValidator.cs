using EBanking.Console.Model;
using EBanking.Console.Validations.Interfaces;
using EBanking.Console.Validations.Exceptions;
using System.Text.RegularExpressions;

namespace EBanking.Console.Validations.Impl
{
    internal class CurrencyValidator : IValidator<Currency>
    {
        public void Validate(Currency entity)
        {
            ValidateName(entity.Name);
            ValidateCode(entity.CurrencyCode);
        }

        void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Морате унети име валуте.");

            if (name.Length > 100)
                throw new ValidationException("Име валуте не сме имати више од 100 карактера");

            if (name.Length < 2)
                throw new ValidationException("Име валуте мора садржати бар два карактера");

            var regex = new Regex(@"^[A-Z][a-z]+([ -]{0,1}[A-Z][a-z]+)*$");

            if (regex.IsMatch(name) == false)
                throw new ValidationException("Име валуте мора бити написано латиничним писмом и прво слово мора бити велико");
        }

        void ValidateCode(string code)
        {
            var regex = new Regex(@"^[A-Z]{3}$");
            if (regex.IsMatch(code) == false) throw new ValidationException("Код валуте мора да садржи само 3 латинична велика слова.");
        }
    }
}
