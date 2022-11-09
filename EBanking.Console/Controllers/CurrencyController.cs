using ConsoleTableExt;
using EBanking.BusinessLayer.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.Controllers
{
    public class CurrencyController
    {
        public CurrencyController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public async Task Start()
        {
            var goBackRequested = false;
            while (goBackRequested == false)
            {
                try
                {
                    ShowCurrencyMenu();
                    var currencyOption = System.Console.ReadLine();
                    System.Console.Clear();
                    switch (currencyOption)
                    {
                        case "0":
                            {
                                goBackRequested = true;
                                break;
                            }
                        case "1":
                            {
                                System.Console.WriteLine("Унесите назив:");
                                var name = System.Console.ReadLine() ?? "";

                                System.Console.WriteLine("Унесите код:");
                                var code = System.Console.ReadLine() ?? "";

                                //ValidateCurrencyData(newCurrency);

                                var currencyLogic = ServiceProvider.GetRequiredService<ICurrencyLogic>();
                                var currency = await currencyLogic.AddCurrencyAsync(name, code);

                                System.Console.WriteLine($"Додата нова валута: '{currency}'. (притисните било који тастер за наставак)");
                                System.Console.ReadKey();

                                break;
                            }
                        case "2":
                            {
                                var exitRequested = false;
                                var currencyID = 0;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите идентификатор валуте: (x - за назад)");
                                    var currencyIDText = System.Console.ReadLine() ?? "";

                                    if (currencyIDText.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (int.TryParse(currencyIDText, out currencyID) == true)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("Грешка приликом уноса. Покушајте поново.");
                                    }
                                }

                                if (exitRequested == true)
                                    break;

                                System.Console.WriteLine("Унесите нову вредност за назив:");
                                string name = System.Console.ReadLine() ?? "";

                                System.Console.WriteLine("Унесите нову вредност за код:");
                                string code = System.Console.ReadLine() ?? "";

                                var currencyLogic = ServiceProvider.GetRequiredService<ICurrencyLogic>();
                                var currency = await currencyLogic.UpdateCurrencyAsync(currencyID, name, code);

                                System.Console.WriteLine($"Ажурирана валута: '{currency}'. (притисните било који тастер за наставак)");
                                System.Console.ReadKey();

                                break;
                            }
                        case "3":
                            {
                                var exitRequested = false;
                                var currencyID = 0;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите идентификатор валуте: (x - за назад)");
                                    var currencyIDText = System.Console.ReadLine() ?? "";

                                    if (currencyIDText.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (int.TryParse(currencyIDText, out currencyID) == true)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("Грешка приликом уноса. Покушајте поново.");
                                    }
                                }

                                if (exitRequested == true)
                                    break;

                                var currencyLogic = ServiceProvider.GetRequiredService<ICurrencyLogic>();
                                var currency = await currencyLogic.RemoveCurrencyAsync(currencyID);

                                System.Console.WriteLine($"Обрисана валута: '{currency}'. (притисните било који тастер за наставак)");

                                System.Console.ReadKey();
                                break;
                            }
                        case "4":
                            {
                                var exitRequested = false;
                                var currencyID = 0;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите идентификатор валуте: (x - за назад)");
                                    var currencyIDText = System.Console.ReadLine() ?? "";

                                    if (currencyIDText.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (int.TryParse(currencyIDText, out currencyID) == true)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("Грешка приликом уноса. Покушајте поново.");
                                    }
                                }

                                if (exitRequested == true)
                                    break;

                                var currencyLogic = ServiceProvider.GetRequiredService<ICurrencyLogic>();
                                var currency = await currencyLogic.FindCurrencyAsync(currencyID);

                                System.Console.WriteLine($"Валута: '{currency}'. (притисните било који тастер за наставак)");

                                System.Console.ReadKey();
                                break;
                            }
                        case "5":
                            {
                                var currencyLogic = ServiceProvider.GetRequiredService<ICurrencyLogic>();
                                var currencies = await currencyLogic.GetAllCurrenciesAsync();

                                ConsoleTableBuilder
                                    .From(currencies)
                                    .WithTitle("ВАЛУТЕ ", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                                    .WithColumn("ИД", "Назив", "Код")
                                    .ExportAndWriteLine();

                                System.Console.WriteLine("Притисните било који тастер за наставак...");
                                System.Console.ReadKey();
                                break;
                            }
                        default:
                            {
                                System.Console.WriteLine("Непозната опција. Покушајте поново..(притисните било који тастер за наставак)");
                                System.Console.ReadKey();
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Грешка: {ex.Message} {Environment.NewLine} (притисните било који тастер за наставак)");
                    System.Console.ReadKey();
                }
            }
        }

        void ShowCurrencyMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("1. Додај");
            System.Console.WriteLine("2. Ажурирај");
            System.Console.WriteLine("3. Обриши");
            System.Console.WriteLine("4. Прикажи једну");
            System.Console.WriteLine("5. Прикажи све");
            System.Console.WriteLine("0. Назад");
            System.Console.Write("Одаберите опцију: ");
        }

        void ValidateCurrencyData(Currency currency)
        {
            ValidateName(currency);
            ValidateCode(currency);
        }
        void ValidateName(Currency currency)
        {
            if (string.IsNullOrWhiteSpace(currency.Name))
                throw new Exception("Морате унети назив валуте");

            if (currency.Name.Length > 50)
                throw new Exception("Назив валуте не сме имати више од 50 карактера");

            if (currency.Name.Length < 2)
                throw new Exception("Назив валуте мора садржати бар два карактера");
        }
        void ValidateCode(Currency currency)
        {
            if (string.IsNullOrWhiteSpace(currency.CurrencyCode))
                throw new Exception("Морате унети код валуте");

            if (currency.CurrencyCode.Length > 15)
                throw new Exception("Код валуте не сме имати више од 15 карактера");

            if (currency.CurrencyCode.Length < 2)
                throw new Exception("Код валуте мора садржати бар два карактера");
        }
    }
}
