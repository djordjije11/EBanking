using ConsoleTableExt;
using EBanking.BusinessLayer.Interfaces;
using EBanking.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.AppControllers
{
    public class CurrencyConsole
    {
        public CurrencyConsole(IServiceProvider serviceProvider)
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

                                var currencyController = ServiceProvider.GetRequiredService<CurrencyController>();
                                var currency = await currencyController.CreateCurrencyAsync(name, code);

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

                                var currencyController = ServiceProvider.GetRequiredService<CurrencyController>();
                                var currency = await currencyController.UpdateCurrencyAsync(currencyID, name, code);

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

                                var currencyController = ServiceProvider.GetRequiredService<CurrencyController>();
                                var currency = await currencyController.DeleteCurrencyAsync(currencyID);

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

                                var currencyController = ServiceProvider.GetRequiredService<CurrencyController>();
                                var currency = await currencyController.ReadCurrencyAsync(currencyID);

                                System.Console.WriteLine($"Валута: '{currency}'. (притисните било који тастер за наставак)");

                                System.Console.ReadKey();
                                break;
                            }
                        case "5":
                            {
                                var currencyController = ServiceProvider.GetRequiredService<CurrencyController>();
                                var currencies = await currencyController.ReadAllCurrenciesAsync();

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
        static void ShowCurrencyMenu()
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
    }
}
