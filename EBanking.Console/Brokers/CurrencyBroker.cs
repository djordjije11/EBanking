using EBanking.Console.DataAccessLayer;
using EBanking.Console.Model;
using EBanking.Console.Repositories;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Brokers
{
    internal class CurrencyBroker : EntityBroker<Currency>
    {
        private CurrencyRepository currencyRepository;
        public CurrencyBroker() : base(new CurrencyRepository())
        {
            currencyRepository = (CurrencyRepository)repository;
        }
        public CurrencyBroker(Connector connector) : base(new CurrencyRepository(), connector)
        {
            currencyRepository = (CurrencyRepository)repository;
        }
        public CurrencyBroker(IValidator<Currency> validator) : base(new CurrencyRepository(), validator)
        {
            currencyRepository = (CurrencyRepository)repository;
        }
        public CurrencyBroker(Connector connector, IValidator<Currency> validator) : base(new CurrencyRepository(), connector, validator)
        {
            currencyRepository = (CurrencyRepository)repository;
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
            return new string[] {"ИД", "Име валуте", "Код валуте"};
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
            Currency newCurrency = new()
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
