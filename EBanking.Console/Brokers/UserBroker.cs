using ConsoleTableExt;
using EBanking.Console.DataAccessLayer;
using EBanking.Console.Model;
using EBanking.Console.Models;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Brokers
{
    internal class UserBroker : EntityBroker<User>
    {
        public UserBroker() { }
        public UserBroker(Connector connector) : base(connector) { }
        public UserBroker(IValidator<User> validator) : base(validator)
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
        public override async Task DeleteEntityFromInput()
        {
            try
            {
                await connector.StartConnection();
                int id = GetIdFromInput();
                List<Account> accounts = await FindAccountsFromUser(id);
                if(accounts.Count > 0)
                {
                    System.Console.WriteLine("NEMA BRISANJA");
                    return;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                await connector.EndConnection();
            }
        }
        public async Task<List<Account>> FindAccountsFromUser(int userId)
        {
            Account account = new() { User = new User() { Id = userId } };
            List<Entity> entities = await SqlRepository.GetAllEntitiesByOtherEntity(account, connector);
            List<Account> accounts = new();
            foreach (Entity entity in entities) accounts.Add((Account)entity);
            return accounts;

        }
        public async Task GetAccountsFromUser(List<Account> accounts)
        {
            ConsoleTableBuilder
                        .From(accounts)
                        .WithTitle(GetPluralClassNameForScreen().ToUpper() + " ", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                        .WithColumn(GetColumnNames())
                        .ExportAndWriteLine();
            System.Console.WriteLine("Притисните било који тастер за наставак...");
        }
    }
}
