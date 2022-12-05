using Microsoft.Extensions.DependencyInjection;

namespace EBanking.AppControllers
{
    public class MainConsole
    {
        public MainConsole(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public IServiceProvider ServiceProvider { get;  }
        public async Task Start()
        {
            while (true)
            {
                try
                {
                    ShowMainMenu();
                    var mainOption = System.Console.ReadLine();
                    
                    switch (mainOption)
                    {
                        case "0":
                            {
                                System.Console.Clear();
                                System.Console.WriteLine("- Крај рада -");
                                return;
                            }
                        case "1":
                            {
                                await ServiceProvider.GetRequiredService<UserConsole>().Start();
                                break;
                            }
                        case "2":
                            {
                                await ServiceProvider.GetRequiredService<AccountConsole>().Start();
                                break;
                            }
                        case "3":
                            {
                                await ServiceProvider.GetRequiredService<TransactionConsole>().Start();
                                break;
                            }
                        case "4":
                            {
                                await ServiceProvider.GetRequiredService<CurrencyConsole>().Start();
                                break;
                            }
                        //case "5":
                        //    {
                        //        var userManager = ServiceProvider.GetRequiredService<UserManager>();

                        //        userManager.LoginUser();

                        //        System.Console.WriteLine($"{userManager.Username} {userManager.Password}");

                        //        var userManager1 = ServiceProvider.GetRequiredService<UserManager>();
                        //        System.Console.WriteLine($"{userManager1.Username} {userManager1.Password}");

                        //        var userManager2 = ServiceProvider.GetRequiredService<UserManager>();
                        //        System.Console.WriteLine($"{userManager2.Username} {userManager2.Password}");

                        //        userManager.LogoutUser();

                        //        System.Console.WriteLine($"{userManager.Username} {userManager.Password}");
                        //        System.Console.WriteLine("kraj");
                        //        System.Console.ReadLine();

                        //        break;
                        //    }
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
        static void ShowMainMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("1. Корисници");
            System.Console.WriteLine("2. Рачуни");
            System.Console.WriteLine("3. Трансакције");
            System.Console.WriteLine("4. Валуте");
            System.Console.WriteLine("0. Крај");
            System.Console.Write("Одаберите опцију: ");
        }
    }
}
