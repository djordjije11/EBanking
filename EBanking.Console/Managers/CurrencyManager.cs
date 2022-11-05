using EBanking.Console.DataAccessLayer;
using EBanking.Console.Model;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Managers
{
    internal class CurrencyManager : EntityManager<Currency>
    {
        public CurrencyManager() { }
        public CurrencyManager(Connector connector) : base(connector) { }
        public CurrencyManager(IValidator<Currency> validator) : base(validator)
        {
        }
        protected override string GetNameForGetId()
        {
            return "валуте";
        }
        protected override string GetClassNameForScreen()
        {
            return "валута";
        }
        protected override string GetPluralClassNameForScreen()
        {
            return "валуте";
        }
        protected override string[] GetColumnNames()
        {
            return new string[] { "ИД", "Име", "Код валуте" };
        }
        protected override Currency GetNewEntityInstance(int id = -1)
        {
            return new Currency() { Id = id };
        }
        public override async Task<Currency> ConstructEntityFromInput(int? id)
        {
            System.Console.WriteLine("Унесите име валуте:");
            var name = System.Console.ReadLine() ?? "";
            System.Console.WriteLine("Унесите код валуте:");
            var currencyCode = System.Console.ReadLine() ?? "";
            Currency newCurrency = new Currency()
            {
                Name = name,
                CurrencyCode = currencyCode
            };
            if (id.HasValue) newCurrency.SetIdentificator(id.Value);
            ValidateEntity(newCurrency);
            return newCurrency;
        }
    }
}
