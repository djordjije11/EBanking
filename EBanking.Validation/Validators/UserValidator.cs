using EBanking.Models;
using FluentValidation;

namespace EBanking.Validations
{
    public class UserValidator : AbstractValidator
    {
        public UserValidator(User user)
        {
            RuleFor(user.FirstName)
                .NotEmpty()
                .WithMessage("Морате унети име корисника")
                .WithMinLength(2)
                .WithMessage("Име не сме бити краће од два карактера")
                .WithMaxLength(50)
                .WithMessage("Име не сме бити дуже од 50 карактера");

            RuleFor(user.LastName)
                .WithMinLength(3)
                .WithMessage("Презиме не сме бити краће од 2 карактера")
                .WithMaxLength(50)
                .WithMessage("Презиме не сме бити дуже од 50 карактера")
                .NotEmpty()
                .WithMessage("Морате унети презиме корисника");

            RuleFor(user.Password)
                .WithMinLength(8)
                .WithMessage("Лозинка не сме бити краћа од 8 карактера")
                .WithMaxLength(20)
                .WithMessage("Лозинка не сме бити дужа од 20 карактера")
                .NotEmpty()
                .WithMessage("Морате унети корисничку лозинку")
                .MatchesRegex(@"^[A-Za-z]*[A-Z][A-Za-z]*[0-9]+[A-Za-z]*$")
                .WithMessage("Лозинка мора имати бар осам карактера, при чему мора садржати бар једно велико слово и један број");

            RuleFor(user.Email)
                .IsEmail()
                .WithMessage("Морате унети исправну адресу електронске поште");
        }
    }
}
