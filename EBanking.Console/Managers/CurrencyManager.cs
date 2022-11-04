using ConsoleTableExt;
using EBanking.Console.ClientLayer;
using EBanking.Console.Model;
using EBanking.Console.Validations;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Managers
{
    internal class CurrencyManager : EntityManager<Currency>
    {
        public CurrencyManager(IValidator<Currency> validator) : base(validator)
        {
        }

        public async Task CreateEntity()
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
            ValidateEntity(newCurrency);
            var currency = await ClientRepository.CreateCurrency(newCurrency);
            System.Console.WriteLine($"Додата нова валута: '{currency}'. (притисните било који тастер за наставак)");
        }

        public override async Task<Validation<Currency>> ConstructEntityFromInput(int? id)
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
            try
            {
                ValidateEntity(newCurrency);
            }
            catch (Exception ex)
            {
                return new Validation<Currency>(ex);
            }
            return new Validation<Currency>(newCurrency);
        }

        public override async Task<Validation<Currency>> FindEntityFromInput()
        {
            try
            {
                int id = GetIdFromInput("валуте");
                var wantedCurrency = await ClientRepository.GetCurrencyById(id);
                if (wantedCurrency == null) throw new ValidationException("У бази не постоји валута са унетим ид бројем.");
                return new Validation<Currency>(wantedCurrency);
            } catch (Exception ex)
            {
                return new Validation<Currency>(ex);
            }
        }

        public override async Task<Validation<Currency>> CreateEntityFromInput()
        {
            try
            {
                Currency newCurrency = GetEntityFromValidation(await ConstructEntityFromInput(null));
                var currency = await ClientRepository.CreateCurrency(newCurrency);
                System.Console.WriteLine($"Додата нова валута: '{currency}'. (притисните било који тастер за наставак)");
            }
            catch(Exception ex)
            {
                return new Validation<Currency>(ex);
            }
            return new Validation<Currency>();
        }

        public override async Task<Validation<Currency>> DeleteEntityFromInput()
        {
            try
            {
                int id = GetIdFromInput("валуте");
                var currency = await ClientRepository.DeleteCurrency(id);
                System.Console.WriteLine($"Обрисана валута: '{currency}'. (притисните било који тастер за наставак)");
            }
            catch (Exception ex)
            {
                return new Validation<Currency>(ex);
            }
            return new Validation<Currency>();
        }

        public override async Task<Validation<Currency>> GetEntitiesFromInput()
        {
            try
            {
                var currencies = await ClientRepository.GetAllCurrencies();
                ConsoleTableBuilder
                    .From(currencies)
                    .WithTitle("КОРИСНИЦИ ", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                    .WithColumn("ИД", "Име", "Код валуте")
                    .ExportAndWriteLine();
                System.Console.WriteLine("Притисните било који тастер за наставак...");
            } catch (Exception ex)
            {
                return new Validation<Currency>(ex);
            }
            return new Validation<Currency>();
        }

        public override async Task<Validation<Currency>> GetEntityFromInput()
        {
            try
            {
                Currency wantedCurrency = GetEntityFromValidation(await FindEntityFromInput());
                System.Console.WriteLine($"Тражена валута: '{wantedCurrency}'. (притисните било који тастер за наставак)");
            } catch (Exception ex)
            {
                return new Validation<Currency>(ex);
            }
            return new Validation<Currency>();
        }

        public override async Task<Validation<Currency>> UpdateEntityFromInput()
        {
            try
            {
                Currency wantedCurrency = GetEntityFromValidation(await FindEntityFromInput());
                System.Console.WriteLine($"Тражена валута: '{wantedCurrency}'.\n");
                Currency newCurrency = GetEntityFromValidation(await ConstructEntityFromInput(wantedCurrency.Id));
                Currency updatedCurrency = await ClientRepository.UpdateCurrencyById(newCurrency);
                System.Console.WriteLine($"Ажурирана валута: '{updatedCurrency}'. (притисните било који тастер за наставак)");
            } catch (Exception ex)
            {
                return new Validation<Currency>(ex);
            }
            return new Validation<Currency>();
        }
    }
}
