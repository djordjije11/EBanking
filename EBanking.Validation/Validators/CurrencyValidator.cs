using EBanking.Models;
using FluentValidation;

namespace EBanking.Validation.Validators
{
    public class CurrencyValidator : AbstractValidator
    {
        public CurrencyValidator(Currency currency)
        {
            RuleFor(currency.Name)
                .WithMinLength(2)
                .WithMessage("Валута не сме бити краће од два карактера")
                .WithMaxLength(20)
                .WithMessage("Валута не сме бити дужа од 20 карактера");
            RuleFor(currency.CurrencyCode)
                .MatchesRegex(@"^[A-Z]{3}$")
                .WithMessage("Код валуте мора имати само 3 велика слова");
        }
    }
}
