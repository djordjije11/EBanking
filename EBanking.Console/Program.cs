using ConsoleTableExt;
using EBanking.Console.ClientLayer;
using EBanking.Console.Managers;
using EBanking.Console.Model;
using EBanking.Console.Models;
using EBanking.Console.Validations;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Impl;
using System.Linq.Expressions;
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
                    //await AccountUseCases(new AccountManager());
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
                        Validation<User> validation = await userManager.CreateEntityFromInput();
                        if (validation.IsValid == false) throw validation.Exception;
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        Validation<User> validation = await userManager.UpdateEntityFromInput();
                        if (validation.IsValid == false) throw validation.Exception;
                        Console.ReadKey();
                        break;
                    }

                case "3":
                    {
                        Validation<User> validation = await userManager.DeleteEntityFromInput();
                        if (validation.IsValid == false) throw validation.Exception;
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        Validation<User> validation = await userManager.GetEntityFromInput();
                        if (validation.IsValid == false) throw validation.Exception;
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        Validation<User> validation = await userManager.GetEntitiesFromInput();
                        if (validation.IsValid == false) throw validation.Exception;
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

async Task CurrencyUseCases(CurrencyManager currencyManager)
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
                        /*
                        Validation<Currency> validation = await currencyManager.CreateEntityFromInput();
                        if (validation.IsValid == false) throw validation.Exception;
                        */
                        await currencyManager.CreateEntity();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        Validation<Currency> validation = await currencyManager.UpdateEntityFromInput();
                        if (validation.IsValid == false) throw validation.Exception;
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        Validation<Currency> validation = await currencyManager.DeleteEntityFromInput();
                        if (validation.IsValid == false) throw validation.Exception;
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        Validation<Currency> validation = await currencyManager.GetEntityFromInput();
                        if (validation.IsValid == false) throw validation.Exception;
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        Validation<Currency> validation = await currencyManager.GetEntitiesFromInput();
                        if (validation.IsValid == false) throw validation.Exception;
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
                        await accountManager.CreateEntityFromInput();
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
