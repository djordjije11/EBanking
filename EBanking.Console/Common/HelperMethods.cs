
using EBanking.Controllers;
using EBanking.DataAccessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SqlDataAccesss.SqlBrokers;
using EBanking.BusinessLayer.Interfaces;
using EBanking.BusinessLayer;

namespace EBanking.Console.Common
{
    public static class HelperMethods
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null)
                return null;

            var member = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault();

            if (member == null) return enumValue.ToString();

            var attr = member.GetCustomAttribute<DisplayAttribute>();

            return attr?.GetName() ?? enumValue.ToString();
        }

        public static ServiceProvider CreateServiceProvider(string databaseType)
        {
            var services = new ServiceCollection();

            switch (databaseType.ToUpper())
            {
                case "SQL":
                    {
                        /*
                        services.AddTransient<IAccountBroker, SqlAccountBroker>();
                        services.AddTransient<IUserBroker, SqlUserBroker>();
                        services.AddTransient<ITransactionBroker, SqlTransactionBroker>();
                        services.AddTransient<ICurrencyBroker, SqlCurrencyBroker>();
                        */
                        services.AddTransient<IBroker, SqlBroker>();
                        break;
                    }
                case "SQLITE":
                    {
                        //services.AddTransient<IAccountBroker, SqliteAccountBroker>();
                        break;
                    }
                default:
                    {
                        throw new Exception("НИЈЕ ПОДРЖАН РАД СА УНЕТИМ ТИПОМ БАЗЕ ПОДАТАКА.");
                    }
            }

            //services.AddSingleton<ILogger, TextLogger>();
            //services.AddSingleton<UserManager, UserManager>();
            services.AddSingleton<MainController, MainController>();
            services.AddSingleton<UserController, UserController>();
            services.AddSingleton<AccountController>();
            services.AddSingleton<CurrencyController>();
            services.AddSingleton<TransactionController>();
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IAccountLogic, AccountLogic>();
            services.AddTransient<ICurrencyLogic, CurrencyLogic>();
            services.AddTransient<ITransactionLogic, TransactionLogic>();

            return services.BuildServiceProvider();
        }
    }
}
