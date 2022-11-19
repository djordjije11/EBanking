using EBanking.AppControllers;
using EBanking.DataAccessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using EBanking.SqlDataAccess.SqlBrokers;
using SqliteDataAccess.SqliteBrokers;
using EBanking.BusinessLayer.Interfaces;
using EBanking.BusinessLayer;
using EBanking.ConfigurationManager.Interfaces;
using EBanking.ConfigurationManager;
using System.Data.SqlClient;
using EBanking.SqlDataAccess.SqlConnectors;
using SqliteDataAccess.SqliteConnectors;
using EBanking.Models;

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
            var filePath = string.Empty;

            switch (databaseType.ToUpper())
            {
                case "SQL":
                    {
                        services.AddTransient<IAccountBroker, SqlAccountBroker>();
                        services.AddTransient<IUserBroker, SqlUserBroker>();
                        services.AddTransient<ITransactionBroker, SqlTransactionBroker>();
                        services.AddTransient<ICurrencyBroker, SqlCurrencyBroker>();
                        services.AddSingleton<IConnector, SqlConnector>();
                        filePath = "D:\\MyDocs\\Repositories\\EBanking App\\SqlDataAccesss\\config.sql.json";
                        break;
                    }
                case "SQLITE":
                    {
                        services.AddTransient<IAccountBroker, SqliteAccountBroker>();
                        services.AddTransient<IUserBroker, SqliteUserBroker>();
                        services.AddTransient<ITransactionBroker, SqliteTransactionBroker>();
                        services.AddTransient<ICurrencyBroker, SqliteCurrencyBroker>();
                        services.AddSingleton<IConnector, SqliteConnector>();
                        filePath = "D:\\MyDocs\\Repositories\\EBanking App\\SqlLiteDataAccess\\config.sqlite.json";
                        break;
                    }
                default:
                    {
                        throw new Exception("НИЈЕ ПОДРЖАН РАД СА УНЕТИМ ТИПОМ БАЗЕ ПОДАТАКА.");
                    }
            }

            services.AddSingleton<ILogger, TextLogger>();
            //services.AddSingleton<UserManager, UserManager>();
            services.AddSingleton<MainConsole, MainConsole>();
            services.AddSingleton<UserConsole, UserConsole>();
            services.AddSingleton<AccountConsole, AccountConsole>();
            services.AddSingleton<CurrencyConsole, CurrencyConsole>();
            services.AddSingleton<TransactionConsole, TransactionConsole>();
            
            
            //ovaj deo ce se inicijalizovati na serverskoj strani kada bude razdvojeno na klijent-server arhitekturu, trenutno je ovde na klijentskoj strani
            services.AddSingleton<UserController, UserController>();
            services.AddSingleton<AccountController, AccountController>();
            services.AddSingleton<CurrencyController, CurrencyController>();
            services.AddSingleton<TransactionController, TransactionController>();
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IAccountLogic, AccountLogic>();
            services.AddTransient<ICurrencyLogic, CurrencyLogic>();
            services.AddTransient<ITransactionLogic, TransactionLogic>();

            services.AddSingleton<IConfigurationManager>(_ =>
            {
                var configurationManager = new JsonFileConfigurationManager();
                configurationManager.Initialize(filePath);
                return configurationManager;
            });

            return services.BuildServiceProvider();
        }
    }
}