using ConsoleTableExt;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EBanking.Controllers
{
    public class UserController
    {
        public UserController(IServiceProvider serviceProvider)
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
                    ShowUserMenu();
                    var userOption = System.Console.ReadLine();
                    System.Console.Clear();
                    switch (userOption)
                    {
                        case "0":
                            {
                                goBackRequested = true;
                                break;
                            }
                        case "1":
                            {
                                System.Console.WriteLine("Унесите име:");
                                var firstName = System.Console.ReadLine() ?? "";

                                System.Console.WriteLine("Унесите презиме:");
                                var lastName = System.Console.ReadLine() ?? "";

                                System.Console.WriteLine("Унесите корисничку адресу:");
                                var email = System.Console.ReadLine() ?? "";

                                System.Console.WriteLine("Унесите шифру:");
                                var password = System.Console.ReadLine() ?? "";

                                var newUser = new User()
                                {
                                    FirstName = firstName,
                                    LastName = lastName,
                                    Email = email,
                                    Password = password
                                };

                                // ValidateUserData(newUser);
                                //var resultInfo = new ValidatorFactory.Validate(newUser);

                                var resultInfo = new UserValidator(newUser).Validate();

                                if (resultInfo.IsValid == false)
                                {
                                    resultInfo.Errors.ForEach(err => System.Console.WriteLine($"[ERROR]> {err}"));
                                    
                                    System.Console.WriteLine("(притисните било који тастер за наставак)");
                                    System.Console.ReadKey();
                                    break;
                                }

                                var userBroker = ServiceProvider.GetRequiredService<IUserBroker>();
                                var user = await userBroker.CreateUserAsync(newUser);

                                System.Console.WriteLine($"Додат нови корисник: '{user}'. (притисните било који тастер за наставак)");
                                System.Console.ReadKey();

                                break;
                            }
                        case "2":
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

                                var userBroker = ServiceProvider.GetRequiredService<IUserBroker>();
                                var user = await userBroker.GetUserByIdAsync(userID);

                                if (user == null)
                                {
                                    System.Console.WriteLine($"Корисник са идентификатором: '{userID}' није пронађен");
                                    break;
                                }

                                System.Console.WriteLine(user.ToString());

                                System.Console.WriteLine("Унесите нову вредност за име:");
                                user.FirstName = System.Console.ReadLine() ?? "";

                                System.Console.WriteLine("Унесите нову вредност за презиме:");
                                user.LastName = System.Console.ReadLine() ?? "";

                                System.Console.WriteLine("Унесите нову вредност за шифру:");
                                user.Password = System.Console.ReadLine() ?? "";

                                //ValidateUserData(user);
                                var resultInfo = new UserValidator(user).Validate();
                                
                                if (resultInfo.IsValid == false)
                                {
                                    resultInfo.Errors.ForEach(err => System.Console.WriteLine($"[ERROR]> {err}"));
                                    
                                    System.Console.WriteLine("(притисните било који тастер за наставак)");
                                    System.Console.ReadKey();
                                    break;
                                }

                                await userBroker.UpdateUserAsync(user);

                                System.Console.WriteLine($"Ажуриран корисник: '{user}'. (притисните било који тастер за наставак)");
                                System.Console.ReadKey();

                                break;
                            }
                        case "3":
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

                                var userBroker = ServiceProvider.GetRequiredService<IUserBroker>();
                                var user = await userBroker.GetUserByIdAsync(userID);

                                if (user == null)
                                {
                                    System.Console.WriteLine($"Корисник са идентификатором: '{userID}' није пронађен");
                                    break;
                                }

                                var isDeleted = await userBroker.DeleteUserAsync(userID);

                                if (isDeleted == true)
                                {
                                    System.Console.WriteLine($"Обрисан корисник: '{user}'. (притисните било који тастер за наставак)");
                                }
                                else
                                {
                                    System.Console.WriteLine($"Грешка приликом брисања корисника: '{user}'. (притисните било који тастер за наставак)");
                                }

                                System.Console.ReadKey();
                                break;
                            }
                        case "4":
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

                                var userBroker = ServiceProvider.GetRequiredService<IUserBroker>();
                                var user = await userBroker.GetUserByIdAsync(userID);

                                if (user == null)
                                {
                                    System.Console.WriteLine($"Корисник са идентификатором: '{userID}' није пронађен");
                                    break;
                                }
                                else
                                {
                                    System.Console.WriteLine($"Корисник: '{user}'. (притисните било који тастер за наставак)");
                                }

                                System.Console.ReadKey();
                                break;
                            }
                        case "5":
                            {
                                var userBroker = ServiceProvider.GetRequiredService<IUserBroker>();
                                var users = await userBroker.GetAllUsersAsync();

                                ConsoleTableBuilder
                                    .From(users)
                                    .WithTitle("КОРИСНИЦИ ", System.ConsoleColor.Yellow, System.ConsoleColor.DarkGray)
                                    .WithColumn("ИД", "Име", "Презиме", "Мејл", "Шифра")
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

        void ShowUserMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("1. Додај");
            System.Console.WriteLine("2. Ажурирај");
            System.Console.WriteLine("3. Обриши");
            System.Console.WriteLine("4. Прикажи једног");
            System.Console.WriteLine("5. Прикажи све");
            System.Console.WriteLine("0. Назад");
            System.Console.Write("Одаберите опцију: ");
        }

        #region Old implementation for validating data

        //void ValidateUserData(User newUser)
        //{
        //    ValidateFirstName(newUser);
        //    ValidateLastName(newUser);
        //    ValidateEmail(newUser);
        //    ValidatePassword(newUser);
        //}
        //void ValidateFirstName(User newUser)
        //{
        //    if (string.IsNullOrWhiteSpace(newUser.FirstName))
        //        throw new Exception("Морате унети име корисника");

        //    if (newUser.FirstName.Length > 100)
        //        throw new Exception("Име корисника не сме имати више од 100 карактера");

        //    if (newUser.FirstName.Length < 2)
        //        throw new Exception("Име корисника мора садржати бар два карактера");

        //    var regex = new Regex(@"^[АБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ][абвгдђежзијклљмнњопрстћуфхцчџш]+([ -]{0,1}[АБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ][абвгдђежзијклљмнњопрстћуфхцчџш]+)*$");

        //    if (regex.IsMatch(newUser.FirstName) == false)
        //        throw new Exception("Име корисника мора бити написано ћириличним писмом и прво слово мора бити велико");
        //}
        //void ValidateLastName(User newUser)
        //{
        //    if (string.IsNullOrWhiteSpace(newUser.LastName))
        //        throw new Exception("Морате унети презиме корисника");

        //    if (newUser.LastName.Length > 100)
        //        throw new Exception("Презиме корисника не сме имати више од 100 карактера");

        //    if (newUser.LastName.Length < 2)
        //        throw new Exception("Презиме корисника мора садржати бар два карактера");

        //    var regex = new Regex(@"^[АБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ][абвгдђежзијклљмнњопрстћуфхцчџш]+([ -]{0,1}[АБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ][абвгдђежзијклљмнњопрстћуфхцчџш]+)*$");

        //    if (regex.IsMatch(newUser.LastName) == false)
        //        throw new Exception("Презиме корисника мора бити написано ћириличним писмом и прво слово мора бити велико");
        //}
        //void ValidateEmail(User newUser)
        //{
        //    if (string.IsNullOrWhiteSpace(newUser.Email))
        //        throw new Exception("Морате унети електронску адресу корисника");

        //    int index = newUser.Email.IndexOf('@');

        //    if ((index > 0 && index != newUser.Email.Length - 1 && index == newUser.Email.LastIndexOf('@')) == false)
        //        throw new Exception("Морате унети адресу електронске поште у валидном формату");
        //}
        //void ValidatePassword(User newUser)
        //{
        //    if (string.IsNullOrWhiteSpace(newUser.Password))
        //        throw new Exception("Морате унети лозинку корисника");

        //    if (newUser.Password.Length > 100)
        //        throw new Exception("Лоѕинка корисника не сме имати више од 100 карактера");

        //    if (newUser.Password.Length < 8)
        //        throw new Exception("Лозинка корисника мора садржати бар осам карактера");

        //    var regex = new Regex(@"^[A-Za-z]*[A-Z][A-Za-z]*[0-9]+[A-Za-z]*$");

        //    if (regex.IsMatch(newUser.Password) == false)
        //        throw new Exception("Лозинка мора имати бар осам карактера, при чему мора садржати бар једно велико слово и један број");
        //}

        #endregion
    }
}
