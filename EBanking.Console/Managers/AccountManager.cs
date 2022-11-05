using EBanking.Console.DataAccessLayer;
using EBanking.Console.Model;
using EBanking.Console.Models;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Impl;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Managers
{
    internal class AccountManager : EntityManager<Account>
    {
        public AccountManager() { }
        public AccountManager(Connector connector) : base(connector) { }
        public AccountManager(IValidator<Account> validator) : base(validator)
        {
        }
        protected override string GetNameForGetId()
        {
            return "рачуна";
        }
        protected override string GetClassNameForScreen()
        {
            return "рачун";
        }
        protected override string GetPluralClassNameForScreen()
        {
            return "рачуни";
        }
        protected override string[] GetColumnNames()
        {
            return new string[] { "ИД", "Стање", "Статус", "Број", "Корисник", "Валута" };
        }
        protected override Account GetNewEntityInstance(int id = -1)
        {
            return new Account() { Id = id };
        }
        public override async Task<Account> ConstructEntityFromInput(int? id)
        {
            int userId = new UserManager().GetIdFromInput();
            User? wantedUser = (User?)(await SqlRepository.GetEntityById(GetNewEntityInstance(userId), connector));
            if (wantedUser == null) throw new ValidationException($"У бази не постоји {GetClassNameForScreen()} са унетим ид бројем.");
            int currencyId = new CurrencyManager().GetIdFromInput();
            Currency? wantedCurrency = (Currency?)(await SqlRepository.GetEntityById(GetNewEntityInstance(currencyId), connector));
            if (wantedCurrency == null) throw new ValidationException($"У бази не постоји {GetClassNameForScreen()} са унетим ид бројем.");
            System.Console.WriteLine("Унесите стање рачуна:");
            if (!Decimal.TryParse(System.Console.ReadLine() ?? "", out decimal balance)) throw new ValidationException("Стање на рачуну мора бити број.");
            System.Console.WriteLine("Одаберите статус рачуна:\n1. Активан 2. Неактиван");
            string statusOption = System.Console.ReadLine() ?? "";
            while (!statusOption.Trim().Equals("1") && !statusOption.Trim().Equals("2"))
            {
                System.Console.WriteLine("Непозната опција. Покушајте опет.");
                System.Console.Clear();
                System.Console.WriteLine($"Одаберите статус рачуна:\n{(int)Status.ACTIVE}. Активан {(int)Status.INACTIVE}. Неактиван");
                statusOption = System.Console.ReadLine() ?? "";
            }
            Status status = (Status)Int32.Parse(statusOption);
            System.Console.WriteLine("Унесите број рачуна:");
            string accountNumber = System.Console.ReadLine() ?? "";
            var newAccount = new Account()
            {
                Balance = balance,
                Status = status,
                Number = accountNumber,
                User = wantedUser,
                Currency = wantedCurrency
            };
            if (id.HasValue) newAccount.SetIdentificator(id.Value);
            ValidateEntity(newAccount);
            return newAccount;
        }
    }
}