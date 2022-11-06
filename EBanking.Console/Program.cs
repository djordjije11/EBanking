using EBanking.Console.Brokers;
using EBanking.Console.ClientLayer;
using EBanking.Console.Validations.Impl;
using System.Text;

// Zbog cirilice
Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;
// Umesto VARCHAR smo u DDL-u za Ime i prezime stavili NVARCHAR da bi sql server mogao da cuva cirilicne karaktere

Client client = new Client();

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
                    client.SetBroker(new UserBroker(new UserValidator()));
                    await UserUseCases(client);
                    break;
                }
            case "2":
                {
                    client.SetBroker(new AccountBroker(new AccountValidator()));
                    await AccountUseCases(client);
                    break;
                }
            case "3":
                {
                    client.SetBroker(new TransactionBroker(new TransactionValidator()));
                    await TransactionUseCases(client);
                    break;
                }
            case "4":
                {
                    client.SetBroker(new CurrencyBroker(new CurrencyValidator()));
                    await CurrencyUseCases(client);
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
async Task UserUseCases(Client client)
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
                        await client.Create();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await client.Update();
                        Console.ReadKey();
                        break;
                    }

                case "3":
                    {
                        await client.Delete();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await client.GetOne();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await client.GetAll();
                        Console.ReadKey();
                        break;
                    }
                case "6":
                    {
                        await client.GetAllAccountsByUser();
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
    Console.WriteLine("6. Прикажи све рачуне корисника");
    Console.WriteLine("0. Назад");
    Console.Write("Одаберите опцију: ");
}
async Task CurrencyUseCases(Client client)
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
                        await client.Create();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await client.Update();
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        await client.Delete();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await client.GetOne();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await client.GetAll();
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
async Task AccountUseCases(Client client)
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
                        await client.Create();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await client.Update();
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        await client.Delete();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await client.GetOne();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await client.GetAll();
                        Console.ReadKey();
                        break;
                    }
                case "6":
                    {
                        await client.GetAllTransactionsByAccount();
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
    Console.WriteLine("6. Прикажи све трансакције рачуна");
    Console.WriteLine("0. Назад");
    Console.Write("Одаберите опцију: ");
}
async Task TransactionUseCases(Client client)
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
                        await client.Create();
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        await client.Update();
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        await client.Delete();
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        await client.GetOne();
                        Console.ReadKey();
                        break;
                    }
                case "5":
                    {
                        await client.GetAll();
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