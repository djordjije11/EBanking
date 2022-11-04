using EBanking.Console.DataAccessLayer;
using EBanking.Console.Model;
using EBanking.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.ClientLayer
{
    internal class ClientRepository
    {
        public static async Task<User> CreateUser(User user)
        {
            User createdUser = (User) (await SqlRepository.CreateEntity(user));
            return createdUser;
        }

        public static async Task<List<User>> GetAllUsers()
        {
            var entities = await SqlRepository.GetAllEntities(new User());
            List<User> users = new List<User>();
            foreach (var entity in entities)
            {
                var user = (User)entity;
                users.Add(user);
            }
            return users;
        }

        public static async Task<User?> GetUserById(int id)
        {
            return (User?)(await SqlRepository.GetEntityById(new User()
            {
                Id = id
            }));
        }

        public static async Task<User> DeleteUser(int id)
        {
            User deletedUser = (User)(await SqlRepository.DeleteEntity(new User()
            {
                Id = id
            }));
            return deletedUser;
        }

        public static async Task<User> UpdateUserById(User user)
        {
            User updatedUser = (User)(await SqlRepository.UpdateEntityById(user));
            return updatedUser;
        }

        public static async Task<Currency> CreateCurrency(Currency currency)
        {
            Currency createdCurrency = (Currency)(await SqlRepository.CreateEntity(currency));
            return createdCurrency;
        }

        public static async Task<List<Currency>> GetAllCurrencies()
        {
            var entities = await SqlRepository.GetAllEntities(new Currency());
            List<Currency> currencies = new List<Currency>();
            foreach (var entity in entities)
            {
                var currency = (Currency)entity;
                currencies.Add(currency);
            }
            return currencies;
        }

        public static async Task<Currency?> GetCurrencyById(int id)
        {
            return (Currency?)(await SqlRepository.GetEntityById(new Currency()
            {
                Id = id
            }));
        }

        public static async Task<Currency> DeleteCurrency(int id)
        {
            Currency deletedCurrency = (Currency)(await SqlRepository.DeleteEntity(new Currency()
            {
                Id = id
            }));
            return deletedCurrency;
        }

        public static async Task<Currency> UpdateCurrencyById(Currency currency)
        {
            Currency updatedCurrency = (Currency)(await SqlRepository.UpdateEntityById(currency));
            return updatedCurrency;
        }

        public static async Task<Account> CreateAccount(Account account)
        {
            Account createdAccount = (Account)(await SqlRepository.CreateEntity(account));
            return createdAccount;
        }
    }
}
