using ConsoleTableExt;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.AppControllers
{
    public class TransactionConsole
    {
        public TransactionConsole(IServiceProvider serviceProvider)
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
                    ShowTransactionMenu();
                    var transactionOption = System.Console.ReadLine();
                    System.Console.Clear();
                    switch (transactionOption)
                    {
                        case "0":
                            {
                                goBackRequested = true;
                                break;
                            }
                        case "1":
                            {
                                var exitRequested = false;
                                var amount = 0m;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите износ: (x - за назад)");
                                    var amountText = System.Console.ReadLine() ?? "";

                                    if (amountText.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (decimal.TryParse(amountText, out amount) == true && amount > 0)
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

                                var fromAccountNumber = string.Empty;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите број рачуна са кога пребацујете новац: (x - за назад)");
                                    fromAccountNumber = System.Console.ReadLine() ?? "";

                                    if (fromAccountNumber.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (fromAccountNumber.Length == 13)
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

                                var toAccountNumber = string.Empty;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите број рачуна на који пребацујете новац: (x - за назад)");
                                    toAccountNumber = System.Console.ReadLine() ?? "";

                                    if (toAccountNumber.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (toAccountNumber.Length == 13)
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

                                var transactionController = ServiceProvider.GetRequiredService<TransactionController>();
                                var transaction = await transactionController.CreateTransactionAsync(amount, DateTime.Now, fromAccountNumber, toAccountNumber);

                                System.Console.WriteLine($"Додата нова трансакција: '{transaction}'. (притисните било који тастер за наставак)");
                                System.Console.ReadKey();

                                break;
                            }
                        case "2":
                            {
                                var exitRequested = false;
                                var transactionID = 0;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите идентификатор трансакције: (x - за назад)");
                                    var transactionIDText = System.Console.ReadLine() ?? "";

                                    if (transactionIDText.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (int.TryParse(transactionIDText, out transactionID) == true)
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

                                var transactionController = ServiceProvider.GetRequiredService<TransactionController>();
                                var transaction = await transactionController.ReadTransactionAsync(transactionID);

                                System.Console.WriteLine($"Трансакција: '{transaction}'. (притисните било који тастер за наставак)");

                                System.Console.ReadKey();
                                break;
                            }
                        case "3":
                            {
                                var transactionController = ServiceProvider.GetRequiredService<TransactionController>();
                                var transactions = await transactionController.ReadAllTransactionsAsync();

                                //var tableRawData = transactions.Select(x => new
                                //{
                                //    Id = x.Id,
                                //    Number = x.Number,
                                //    UserFirstName = x.User.FirstName,
                                //    UserLastName = x.User.LastName,
                                //    UserEmail = x.User.Email,
                                //    CurrencyName = x.Currency.Name,
                                //    Balance = x.Balance,
                                //    Status = x.Status.GetDisplayName()
                                //}).ToList();

                                //ConsoleTableBuilder
                                //    .From(tableRawData)
                                //    .WithTitle("РАЧУНИ ", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                                //    .WithColumn("ИД", "Број рачуна", "Име", "Презиме", "ЕМаил", "Валута", "Износ", "Статус")
                                //    .ExportAndWriteLine();

                                ConsoleTableBuilder
                                    .From(transactions)
                                    .WithTitle("ТРАНСАКЦИЈЕ ", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                                    .WithColumn("ИД", "Износ", "Датум", "Давалац", "Прималац")
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
        static void ShowTransactionMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("1. Додај");
            System.Console.WriteLine("2. Прикажи једну");
            System.Console.WriteLine("3. Прикажи све");
            System.Console.WriteLine("0. Назад");
            System.Console.Write("Одаберите опцију: ");
        }
    }
}
