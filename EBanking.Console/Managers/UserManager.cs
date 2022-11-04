using EBanking.Console.Model;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Managers
{
    internal class UserManager : EntityManager<User>
    {
        public UserManager(IValidator<User> validator) : base(validator)
        {
        }
        protected override string GetNameForGetId()
        {
            return "корисника";
        }
        protected override string GetClassNameForScreen()
        {
            return "корисник";
        }
        protected override string GetPluralClassNameForScreen()
        {
            return "корисници";
        }
        protected override string[] GetColumnNames()
        {
            return new string[] { "ИД", "Име", "Презиме", "Мејл", "Шифра" };
        }
        protected override User GetNewEntityInstance(int id = -1)
        {
            return new User() { Id = id };
        }
        public override async Task<User> ConstructEntityFromInput(int? id)
        {
            System.Console.WriteLine("Унесите име:");
            var firstName = System.Console.ReadLine() ?? "";
            System.Console.WriteLine("Унесите презиме:");
            var lastName = System.Console.ReadLine() ?? "";
            System.Console.WriteLine("Унесите корисничку адресу:");
            var email = System.Console.ReadLine() ?? "";
            System.Console.WriteLine("Унесите шифру:");
            var password = System.Console.ReadLine() ?? "";
            var newUser = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };
            if (id.HasValue) newUser.SetIdentificator(id.Value);
            ValidateEntity(newUser);
            return newUser;
        }
    }
}
