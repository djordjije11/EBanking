using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.Controllers
{
    public class TransactionController
    {
        public TransactionController(IServiceProvider serviceProvider)
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

                                var accountBroker = ServiceProvider.GetRequiredService<IAccountBroker>();
                                var fromAccount = await accountBroker.GetAccountByNumberAsync(fromAccountNumber);

                                if (fromAccount == null)
                                {
                                    System.Console.WriteLine($"Рачун са бројем: '{fromAccountNumber}' није пронађен");
                                    break;
                                }

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

                                var toAccount = await accountBroker.GetAccountByNumberAsync(toAccountNumber);

                                if (toAccount == null)
                                {
                                    System.Console.WriteLine($"Рачун са бројем: '{toAccountNumber}' није пронађен");
                                    break;
                                }


                                var newTransaction = new Transcation()
                                {
                                    Amount = amount,
                                    Date = DateTime.UtcNow,
                                    FromAccount = fromAccount,
                                    FromAccountId = fromAccount.Id,
                                    ToAccount = toAccount,
                                    ToAccountId = toAccount.Id
                                };

                                var transactionBroker = ServiceProvider.GetRequiredService<ITransactionBroker>();
                                var transcation = await transactionBroker.CreateTranscationAsync(newTransaction);

                                System.Console.WriteLine($"Додата нова трансакција: '{transcation}'. (притисните било који тастер за наставак)");
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

                                var transactionBroker = ServiceProvider.GetRequiredService<ITransactionBroker>();
                                var transaction = await transactionBroker.GetTransactionByIdAsync(transactionID);

                                if (transaction == null)
                                {
                                    System.Console.WriteLine($"Трансакција са идентификатором: '{transactionID}' није пронађена.");
                                    break;
                                }
                                else
                                {
                                    System.Console.WriteLine($"Трансакција: '{transaction}'. (притисните било који тастер за наставак)");
                                }

                                System.Console.ReadKey();
                                break;
                            }
                        case "3":
                            {
                                var transactionBroker = ServiceProvider.GetRequiredService<ITransactionBroker>();
                                var transactions = await transactionBroker.GetAllTransactionsAsync();

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

        void ShowTransactionMenu()
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
