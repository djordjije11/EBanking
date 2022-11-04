using ConsoleTableExt;
using EBanking.Console.ClientLayer;
using EBanking.Console.Model;
using EBanking.Console.Validations;
using EBanking.Console.Validations.Impl;
using EBanking.Console.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Managers
{
    internal class UserManager : EntityManager<User>
    {
        public UserManager(IValidator<User> validator) : base(validator)
        {
        }

        public override async Task<Validation<User>> CreateEntityFromInput()
        {
            try
            {
                User newUser = GetEntityFromValidation(await ConstructEntityFromInput(null));
                var user = await ClientRepository.CreateUser(newUser);
                System.Console.WriteLine($"Додат нови корисник: '{user}'. (притисните било који тастер за наставак)");
            } catch (Exception ex)
            {
                return new Validation<User>(ex);
            }
            return new Validation<User>();
        }

        public override async Task<Validation<User>> UpdateEntityFromInput()
        {
            try
            {
                var wantedUser = GetEntityFromValidation(await FindEntityFromInput());
                System.Console.WriteLine("Тражени корисник: " + wantedUser + ".\n");
                var newUser = GetEntityFromValidation(await ConstructEntityFromInput(wantedUser.Id));
                User updatedUser = await ClientRepository.UpdateUserById(newUser);
                System.Console.WriteLine($"Ажуриран корисник: '{updatedUser}'. (притисните било који тастер за наставак)");
            } catch(Exception ex)
            {
                return new Validation<User>(ex);
            }
            return new Validation<User>();
        }

        public override async Task<Validation<User>> DeleteEntityFromInput()
        {
            try
            {
                int id = GetIdFromInput("корисника");
                var user = await ClientRepository.DeleteUser(id);
                System.Console.WriteLine($"Обрисан корисник: '{user}'. (притисните било који тастер за наставак)");
            } catch (Exception ex)
            {
                return new Validation<User>(ex);
            }
            return new Validation<User>();
        }
        public override async Task<Validation<User>> GetEntityFromInput()
        {
            try
            {
                var wantedUser = GetEntityFromValidation(await FindEntityFromInput());
                System.Console.WriteLine($"Тражени корисник: '{wantedUser}'. (притисните било који тастер за наставак)");
            } catch (Exception ex)
            {
                return new Validation<User>(ex);
            }
            return new Validation<User>();
        }

        public override async Task<Validation<User>> GetEntitiesFromInput()
        {
            try
            {
                var users = await ClientRepository.GetAllUsers();
                ConsoleTableBuilder
                    .From(users)
                    .WithTitle("КОРИСНИЦИ ", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                    .WithColumn("ИД", "Име", "Презиме", "Мејл", "Шифра")
                    .ExportAndWriteLine();
                System.Console.WriteLine("Притисните било који тастер за наставак...");
            } catch (Exception ex)
            {
                return new Validation<User>(ex);
            }
            return new Validation<User>();
        }
            

        public override async Task<Validation<User>> ConstructEntityFromInput(int? id)
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
            if (id.HasValue) newUser.SetIdentificator(id.Value);
            try
            {
                ValidateEntity(newUser);
            } catch(Exception ex)
            {
                return new Validation<User>(ex);
            }
            return new Validation<User>(newUser);
        }
        public override async Task<Validation<User>> FindEntityFromInput()
        {
            try
            {
                int id = GetIdFromInput("корисника");
                var wantedUser = await ClientRepository.GetUserById(id);
                if (wantedUser == null) throw new ValidationException("У бази не постоји валута са унетим ид бројем.");
                return new Validation<User>(wantedUser);
            } catch(Exception ex)
            {
                return new Validation<User>(ex);
            }
        }
    }
}
