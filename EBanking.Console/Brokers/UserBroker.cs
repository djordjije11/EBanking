using ConsoleTableExt;
using EBanking.Console.DataAccessLayer;
using EBanking.Console.Model;
using EBanking.Console.Models;
using EBanking.Console.Repositories;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Brokers
{
    internal class UserBroker : EntityBroker<User>
    {
        private UserRepository userRepository;
        public UserBroker() : base(new UserRepository())
        { 
            userRepository = (UserRepository)repository;
        }
        public UserBroker(Connector connector) : base(new UserRepository(), connector)
        {
            userRepository = (UserRepository)repository;
        }
        public UserBroker(IValidator<User> validator) : base(new UserRepository(), validator)
        {
            userRepository = (UserRepository)repository;
        }
        public UserBroker(Connector connector, IValidator<User> validator) : base(new UserRepository(), connector, validator)
        {
            userRepository = (UserRepository)repository;
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
            return new string[] { "ИД", "Име", "Презиме", "Емаил адреса", "Шифра" };
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
        public override async Task DeleteEntityFromInput()
        {
            try
            {
                await connector.StartConnection();
                User user = await FindEntityFromInput();
                List<Account> accounts = await FindAccountsFromUser(user);
                if (accounts.Count > 0) throw new ValidationException("Не можете обрисати кориснике који имају рачуне. (притисните било који тастер за наставак)");
                await connector.StartTransaction();
                User deletedUser = (User)(await repository.DeleteEntity(user, connector));
                await connector.CommitTransaction();
                System.Console.WriteLine($"Обрисан {GetClassNameForScreen()} објекат: '{deletedUser.SinglePrint()}'. (притисните било који тастер за наставак)");
            }
            catch
            {
                await connector.RollbackTransaction();
                throw;
            }
            finally
            {
                await connector.EndConnection();
            }
        }
        public async Task<List<Account>> FindAccountsFromUser(User user)
        {
            Account account = new() { User = user };
            return await userRepository.GetAccountsByUser(account, connector);
        }
        public async Task GetAccountsFromUserFromInput()
        {
            List<Account> accounts;
            try
            {
                await connector.StartConnection();
                User user = await FindEntityFromInput();
                System.Console.WriteLine($"Тражени {GetClassNameForScreen()}: '{user.SinglePrint()}'. (притисните било који тастер за наставак)");
                accounts = await FindAccountsFromUser(user);
            }
            catch
            {
                throw;
            }
            finally
            {
                await connector.EndConnection();
            }
            new AccountBroker().WriteEntitiesTable(accounts);
        }
    }
}
