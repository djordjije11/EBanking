using EBanking.ConsoleForms;
using EBanking.DataAccessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using EBanking.SqlDataAccess.SqlBrokers;
using SqliteDataAccess.SqliteBrokers;
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
                        services.AddTransient<IAccountBroker, SqlAccountBroker>();
                        services.AddTransient<IUserBroker, SqlUserBroker>();
                        services.AddTransient<ITransactionBroker, SqlTransactionBroker>();
                        services.AddTransient<ICurrencyBroker, SqlCurrencyBroker>();
                        break;
                    }
                case "SQLITE":
                    {
                        services.AddTransient<IAccountBroker, SqliteAccountBroker>();
                        services.AddTransient<IUserBroker, SqliteUserBroker>();
                        services.AddTransient<ITransactionBroker, SqliteTransactionBroker>();
                        services.AddTransient<ICurrencyBroker, SqliteCurrencyBroker>();
                        break;
                    }
                default:
                    {
                        throw new Exception("НИЈЕ ПОДРЖАН РАД СА УНЕТИМ ТИПОМ БАЗЕ ПОДАТАКА.");
                    }
            }

            //services.AddSingleton<ILogger, TextLogger>();
            //services.AddSingleton<UserManager, UserManager>();
            services.AddSingleton<MainConsole, MainConsole>();
            services.AddSingleton<UserConsole, UserConsole>();
            services.AddSingleton<AccountConsole, AccountConsole>();
            services.AddSingleton<CurrencyConsole, CurrencyConsole>();
            services.AddSingleton<TransactionConsole, TransactionConsole>();
            services.AddSingleton<UserController, UserController>();
            services.AddSingleton<AccountController, AccountController>();
            services.AddSingleton<CurrencyController, CurrencyController>();
            services.AddSingleton<TransactionController, TransactionController>();


            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IAccountLogic, AccountLogic>();
            services.AddTransient<ICurrencyLogic, CurrencyLogic>();
            services.AddTransient<ITransactionLogic, TransactionLogic>();

            return services.BuildServiceProvider();
        }
    }
}
