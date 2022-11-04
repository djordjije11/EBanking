using EBanking.Console.Model;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Interfaces;
using System.Text.RegularExpressions;

namespace EBanking.Console.Validations.Impl
{
    internal class UserValidator : IValidator<User>
    {
        public void Validate(User entity)
        {
            ValidateName(entity.FirstName, "Име");
            ValidateName(entity.LastName, "Презиме");
            ValidateEmail(entity.Email);
            ValidatePassword(entity.Password);
        }

        void ValidateName(string name, string messageText)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException($"Морате унети {messageText.ToLower()} корисника");

            if (name.Length > 100)
                throw new ValidationException($"{messageText} корисника не сме имати више од 100 карактера");

            if (name.Length < 2)
                throw new ValidationException($"{messageText} корисника мора садржати бар два карактера");

            var regex = new Regex(@"^[АБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ][абвгдђежзијклљмнњопрстћуфхцчџш]+([ -]{0,1}[АБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ][абвгдђежзијклљмнњопрстћуфхцчџш]+)*$");

            if (regex.IsMatch(name) == false)
                throw new ValidationException($"{messageText} корисника мора бити написано ћириличним писмом и прво слово мора бити велико");
        }
        void ValidateEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                throw new ValidationException("Емаил адреса не сме да се завршава тачком.");
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != trimmedEmail) throw new ValidationException("Емаил адреса није валидна.");
            }
            catch
            {
                throw new ValidationException("Емаил адреса није валидна.");
            }
        }
        void ValidatePassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-ZАБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            if (!hasNumber.IsMatch(password)) throw new ValidationException("Шифра мора да садржи цифру у себи.");
            if (!hasUpperChar.IsMatch(password)) throw new ValidationException("Шифра мора да садржи бар једно велико слово у себи.");
            if (!hasMinimum8Chars.IsMatch(password)) throw new ValidationException("Шифра мора да има најмање 8 знакова.");
        }
    }
}
