using ConsoleTableExt;
using EBanking.BusinessLayer.Interfaces;
using EBanking.Console.Common;
using EBanking.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.Controllers
{
    public class AccountController
    {
        public AccountController(IServiceProvider serviceProvider)
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
                    ShowAccountMenu();
                    var accountOption = System.Console.ReadLine();
                    System.Console.Clear();
                    switch (accountOption)
                    {
                        case "0":
                            {
                                goBackRequested = true;
                                break;
                            }
                        case "1":
                            {
                                var exitRequested = false;
                                var userID = 0;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите идентификатор корисника: (x - за назад)");
                                    var userIDText = System.Console.ReadLine() ?? "";

                                    if (userIDText.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (int.TryParse(userIDText, out userID) == true)
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
                                                              
                                var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
                                Account account;
                                account = await accountLogic.AddAccountAsync(userID, currencyID);

                                System.Console.WriteLine($"Додат нови рачун: '{account.SinglePrint()}'. (притисните било који тастер за наставак)");
                                System.Console.ReadKey();

                                break;
                            }
                        case "2":
                            {
                                var exitRequested = false;
                                var accountID = 0;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите идентификатор рачуна: (x - за назад)");
                                    var accountIDText = System.Console.ReadLine() ?? "";

                                    if (accountIDText.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (int.TryParse(accountIDText, out accountID) == true)
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

                                System.Console.WriteLine("Унесите нови статус рачуна:");
                                var status = GetAccountStatus(System.Console.ReadLine() ?? "");

                                var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
                                var account = await accountLogic.UpdateAccountAsync(accountID, status);

                                System.Console.WriteLine($"Ажуриран рачун: '{account}'. (притисните било који тастер за наставак)");
                                System.Console.ReadKey();

                                break;
                            }
                        case "3":
                            {
                                var exitRequested = false;
                                var accountID = 0;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите идентификатор рачуна: (x - за назад)");
                                    var accountIDText = System.Console.ReadLine() ?? "";

                                    if (accountIDText.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (int.TryParse(accountIDText, out accountID) == true)
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

                                var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();

                                var deletedAccount = await accountLogic.RemoveAccountAsync(accountID);

                                System.Console.WriteLine($"Обрисан рачуна: '{deletedAccount}'. (притисните било који тастер за наставак)");

                                System.Console.ReadKey();
                                break;
                            }
                        case "4":
                            {
                                var exitRequested = false;
                                var accountID = 0;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите идентификатор рачуна: (x - за назад)");
                                    var accountIDText = System.Console.ReadLine() ?? "";

                                    if (accountIDText.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (int.TryParse(accountIDText, out accountID) == true)
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
                                
                                var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
                                var account = await accountLogic.FindAccountAsync(accountID);

                                System.Console.WriteLine($"Рачун: '{account}'. (притисните било који тастер за наставак)");

                                System.Console.ReadKey();
                                break;
                            }
                        case "5":
                            {
                                var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
                                var accounts = await accountLogic.GetAllAccountsAsync();

                                var tableRawData = accounts.Select(x => new
                                {
                                    Id = x.Id,
                                    Number = x.Number,
                                    UserFirstName = x.User.FirstName,
                                    UserLastName = x.User.LastName,
                                    UserEmail = x.User.Email,
                                    CurrencyName = x.Currency.Name,
                                    Balance = x.Balance,
                                    Status = x.Status.GetDisplayName()
                                }).ToList();

                                ConsoleTableBuilder
                                    .From(tableRawData)
                                    .WithTitle("РАЧУНИ ", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                                    .WithColumn("ИД", "Број рачуна", "Име", "Презиме", "ЕМаил", "Валута", "Износ", "Статус")
                                    .ExportAndWriteLine();

                                System.Console.WriteLine("Притисните било који тастер за наставак...");
                                System.Console.ReadKey();
                                break;
                            }
                        case "6":
                            {
                                var exitRequested = false;
                                var accountID = 0;
                                while (true)
                                {
                                    System.Console.WriteLine("Унесите идентификатор рачуна: (x - за назад)");
                                    var accountIDText = System.Console.ReadLine() ?? "";

                                    if (accountIDText.ToLower() == "x")
                                    {
                                        exitRequested = true;
                                        break;
                                    }

                                    if (int.TryParse(accountIDText, out accountID) == true)
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

                                var accountLogic = ServiceProvider.GetRequiredService<IAccountLogic>();
                                var transactions = await accountLogic.GetTransactionsByAccount(accountID);

                                var tableRawData = transactions.Select(x => new
                                {
                                    Id = x.Id,
                                    Amount = x.Amount,
                                    Date = x.Date,
                                    FromAccount = x.FromAccount,
                                    ToAccount = x.ToAccount
                                }).ToList();

                                ConsoleTableBuilder
                                    .From(tableRawData)
                                    .WithTitle("РАЧУНИ ", ConsoleColor.Yellow, ConsoleColor.DarkGray)
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

        void ShowAccountMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("1. Додај");
            System.Console.WriteLine("2. Ажурирај");
            System.Console.WriteLine("3. Обриши");
            System.Console.WriteLine("4. Прикажи један");
            System.Console.WriteLine("5. Прикажи све");
            System.Console.WriteLine("6. Прикажи све трансакције рачуна");
            System.Console.WriteLine("0. Назад");
            System.Console.Write("Одаберите опцију: ");
        }

        AccountStatus GetAccountStatus(string accountStatus)
        {
            if (Enum.TryParse<AccountStatus>(accountStatus, out AccountStatus status) == true)
            {
                return status;
            }

            throw new Exception("Унет невалидан статус рачуна.");
        }
    }
}
