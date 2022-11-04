using EBanking.Console.Managers;
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
                    await UserUseCases(new UserManager(new UserValidator()));
                    break;
                }
            case "2":
                {
                    await AccountUseCases(new AccountManager(new AccountValidator()));
                    break;
                }
            case "3":
                {
                    await TransactionUseCases(new TransactionManager(new TransactionValidator()));
                    break;
                }
            case "4":
                {
                    await CurrencyUseCases(new CurrencyManager(new CurrencyValidator()));
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
async Task UserUseCases(EntityManager<User> userManager)
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
                        await userManager.CreateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await userManager.UpdateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }

                case "3":
                    {
                        await userManager.DeleteEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await userManager.GetEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await userManager.GetEntitiesFromInput();
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
async Task CurrencyUseCases(EntityManager<Currency> currencyManager)
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
                        await currencyManager.CreateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await currencyManager.UpdateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        await currencyManager.DeleteEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await currencyManager.GetEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await currencyManager.GetEntitiesFromInput();
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
async Task AccountUseCases(EntityManager<Account> accountManager)
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
                        await accountManager.CreateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await accountManager.UpdateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        await accountManager.DeleteEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await accountManager.GetEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await accountManager.GetEntitiesFromInput();
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
async Task TransactionUseCases(EntityManager<Transaction> transactionManager)
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
                        await transactionManager.CreateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await transactionManager.UpdateEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        await transactionManager.DeleteEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await transactionManager.GetEntityFromInput();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await transactionManager.GetEntitiesFromInput();
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