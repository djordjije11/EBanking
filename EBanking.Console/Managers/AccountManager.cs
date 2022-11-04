using EBanking.Console.Model;
using EBanking.Console.Models;
using EBanking.Console.Validations;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Managers
{
    internal class AccountManager
    {

        /*
        public override async Task<Validation<Account>> ConstructEntityFromInput(int? id)
        {
            // SREDI ZA TASK<VALIDATION DA BUDE POVRATNA VREDNOST
            throw new NotImplementedException();
            
            Validation validation = await (new UserManager(null).FindEntityFromInput());
            if (validation.IsValid == false && validation.Exception != null) throw validation.Exception;
            User wantedUser = (User)validation.Entity;
            int currencyId = GetIdFromInput("валуте");
            new UserManager(null);

            System.Console.WriteLine("Унесите стање рачуна:");
            if (!Double.TryParse(System.Console.ReadLine() ?? "", out double balance)) throw new ValidationException("Стање на рачуну мора бити број.");
            System.Console.WriteLine("Одаберите статус рачуна:\n1. Активан 2. Неактиван");
            string statusOption = System.Console.ReadLine() ?? "";
            while(statusOption != "1" || statusOption != "2")
            {
                System.Console.WriteLine("Непозната опција. Покушајте опет.");
                System.Console.Clear();
                System.Console.WriteLine("Одаберите статус рачуна:\n1. Активан 2. Неактиван");
                statusOption = System.Console.ReadLine() ?? "";
            }
            Status status = (Status)Int32.Parse(statusOption);
            System.Console.WriteLine("Унесите број рачуна:");
            string accountNumber = System.Console.ReadLine() ?? "";

            

            var newAccount = new Account()
            {
                Balance = balance,
                Status = status,
                Number = accountNumber,
                //User
                //Currency
            };
            if (id.HasValue) newAccount.SetIdentificator(id.Value);
            ValidateEntity(newAccount);
            return newAccount;
            
        }*/

        /*
        public override Task<Validation<Account>> DeleteEntityFromInput()
        {
            throw new NotImplementedException();
        }

        public override Task<Validation<Account>> GetEntitiesFromInput()
        {
            throw new NotImplementedException();
        }

        public override Task<Validation<Account>> GetEntityFromInput()
        {
            throw new NotImplementedException();
        }

        public override Task<Validation<Account>> UpdateEntityFromInput()
        {
            throw new NotImplementedException();
        }

        public override Task<Validation<Account>> FindEntityFromInput()
        {
            throw new NotImplementedException();
        }
        */
    }
}
