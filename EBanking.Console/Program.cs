using EBanking.Console.Brokers;
using EBanking.Console.Model;
using EBanking.Console.Models;
using EBanking.Console.Validations.Impl;
using System.Text;

// Zbog cirilice
Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;
// Umesto VARCHAR smo u DDL-u za Ime i prezime stavili NVARCHAR da bi sql server mogao da cuva cirilicne karaktere

while (true)
{
    try
    {
        ShowMainMenu();
        var mainOption = Console.ReadLine();

        switch (mainOption)
        {
            case "0":
                {
                    Console.Clear();
                    Console.WriteLine("- Крај рада -");
                    return;
                }
            case "1":
                {
                    await UserUseCases(new UserBroker(new UserValidator()));
                    break;
                }
            case "2":
                {
                    await AccountUseCases(new AccountBroker(new AccountValidator()));
                    break;
                }
            case "3":
                {
                    await TransactionUseCases(new TransactionBroker(new TransactionValidator()));
                    break;
                }
            case "4":
                {
                    await CurrencyUseCases(new CurrencyBroker(new CurrencyValidator()));
                    break;
                }
            default:
                {
                    Console.WriteLine("Непозната опција. Покушајте поново..(притисните било који тастер за наставак)");
                    Console.ReadKey();
                    break;
                }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Грешка: {ex.Message} {Environment.NewLine} (притисните било који тастер за наставак)");
        Console.ReadKey();
    }
}
void ShowMainMenu()
{
    Console.Clear();
    Console.WriteLine("1. Корисници");
    Console.WriteLine("2. Рачуни");
    Console.WriteLine("3. Трансакције");
    Console.WriteLine("4. Валуте");
    Console.WriteLine("0. Крај");
    Console.Write("Одаберите опцију: ");
}
async Task UserUseCases(EntityBroker<User> userBroker)
{
    var goBackRequested = false;
    while (goBackRequested == false)
    {
        try
        {
            ShowUserMenu();
            var userOption = Console.ReadLine();
            Console.Clear();
            switch (userOption)
            {
                case "0":
                    {
                        goBackRequested = true;
                        break;
                    }
                case "1":
                    {
                        await userBroker.CreateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await userBroker.UpdateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }

                case "3":
                    {
                        await userBroker.DeleteEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await userBroker.GetEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await userBroker.GetEntitiesFromInput();
                        Console.ReadKey();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Непозната опција. Покушајте поново..(притисните било који тастер за наставак)");
                        Console.ReadKey();
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Грешка: {ex.Message} {Environment.NewLine} (притисните било који тастер за наставак)");
            Console.ReadKey();
        }
    }
}
void ShowUserMenu()
{
    Console.Clear();
    Console.WriteLine("1. Додај");
    Console.WriteLine("2. Ажурирај");
    Console.WriteLine("3. Обриши");
    Console.WriteLine("4. Прикажи једног");
    Console.WriteLine("5. Прикажи све");
    Console.WriteLine("0. Назад");
    Console.Write("Одаберите опцију: ");
}
async Task CurrencyUseCases(EntityBroker<Currency> currencyBroker)
{
    var goBackRequested = false;
    while (goBackRequested == false)
    {
        try
        {
            ShowCurrencyMenu();
            var userOption = Console.ReadLine();
            Console.Clear();
            switch (userOption)
            {
                case "0":
                    {
                        goBackRequested = true;
                        break;
                    }
                case "1":
                    {
                        await currencyBroker.CreateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await currencyBroker.UpdateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        await currencyBroker.DeleteEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await currencyBroker.GetEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await currencyBroker.GetEntitiesFromInput();
                        Console.ReadKey();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Непозната опција. Покушајте поново..(притисните било који тастер за наставак)");
                        Console.ReadKey();
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Грешка: {ex.Message} {Environment.NewLine} (притисните било који тастер за наставак)");
            Console.ReadKey();
        }   
    }
}
void ShowCurrencyMenu()
{
    Console.Clear();
    Console.WriteLine("1. Додај");
    Console.WriteLine("2. Ажурирај");
    Console.WriteLine("3. Обриши");
    Console.WriteLine("4. Прикажи једног");
    Console.WriteLine("5. Прикажи све");
    Console.WriteLine("0. Назад");
    Console.Write("Одаберите опцију: ");
}
async Task AccountUseCases(EntityBroker<Account> accountBroker)
{
    var goBackRequested = false;
    while (goBackRequested == false)
    {
        try
        {
            ShowAccountMenu();
            var userOption = Console.ReadLine();
            Console.Clear();
            switch (userOption)
            {
                case "0":
                    {
                        goBackRequested = true;
                        break;
                    }
                case "1":
                    {
                        await accountBroker.CreateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await accountBroker.UpdateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        await accountBroker.DeleteEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await accountBroker.GetEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await accountBroker.GetEntitiesFromInput();
                        Console.ReadKey();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Непозната опција. Покушајте поново..(притисните било који тастер за наставак)");
                        Console.ReadKey();
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Грешка: {ex.Message} {Environment.NewLine} (притисните било који тастер за наставак)");
            Console.ReadKey();
        }
    }
}
void ShowAccountMenu()
{
    Console.Clear();
    Console.WriteLine("1. Додај");
    Console.WriteLine("2. Ажурирај");
    Console.WriteLine("3. Обриши");
    Console.WriteLine("4. Прикажи једног");
    Console.WriteLine("5. Прикажи све");
    Console.WriteLine("0. Назад");
    Console.Write("Одаберите опцију: ");
}
async Task TransactionUseCases(EntityBroker<Transaction> transactionBroker)
{
    var goBackRequested = false;
    while (goBackRequested == false)
    {
        try
        {
            ShowTransactionMenu();
            var userOption = Console.ReadLine();
            Console.Clear();
            switch (userOption)
            {
                case "0":
                    {
                        goBackRequested = true;
                        break;
                    }
                case "1":
                    {
                        await transactionBroker.CreateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await transactionBroker.UpdateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        await transactionBroker.DeleteEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await transactionBroker.GetEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await transactionBroker.GetEntitiesFromInput();
                        Console.ReadKey();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Непозната опција. Покушајте поново..(притисните било који тастер за наставак)");
                        Console.ReadKey();
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Грешка: {ex.Message} {Environment.NewLine} (притисните било који тастер за наставак)");
            Console.ReadKey();
        }
    }
    void ShowTransactionMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Додај");
        Console.WriteLine("2. Ажурирај");
        Console.WriteLine("3. Обриши");
        Console.WriteLine("4. Прикажи једног");
        Console.WriteLine("5. Прикажи све");
        Console.WriteLine("0. Назад");
        Console.Write("Одаберите опцију: ");
    }
}