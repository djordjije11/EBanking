using EBanking.AppControllers;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using EBanking.ConfigurationManager.Interfaces;
using EBanking.ConfigurationManager;
using EBanking.Models;
using EBanking.Services.HttpClients;
using EBanking.Services.Interfaces;
using EBanking.Services.APIServices;
using EBanking.Services.LogicServices;
using EBanking.BusinessLayer.Interfaces;
using EBanking.API.DTO.AccountDtos;
using EBanking.API.DTO.UserDtos;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.SqlDataAccess.SqlBrokers;
using EBanking.SqlDataAccess.SqlConnectors;
using EBanking.BusinessLayer;
using EBanking.API.DTO.TransactionDtos;

namespace EBanking.Console.Common
{
    public static class HelperMethods
    {
        public static string? GetDisplayName(this Enum enumValue)
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
        public static ServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILogger, TextLogger>();
            //services.AddSingleton<UserManager, UserManager>();
            services.AddSingleton<MainConsole, MainConsole>();
            services.AddSingleton<UserConsole, UserConsole>();
            services.AddSingleton<AccountConsole, AccountConsole>();
            services.AddSingleton<CurrencyConsole, CurrencyConsole>();
            services.AddSingleton<TransactionConsole, TransactionConsole>();

            var filePath = "D:\\MyDocs\\Repositories\\EBanking App\\EBanking.Console\\config.json";
            services.AddSingleton<IConfigurationManager>(_ =>
            {
                var configurationManager = new JsonFileConfigurationManager();
                configurationManager.Initialize(filePath);
                return configurationManager;
            });
            
            services.AddHttpClient();
            services.AddTransient<IUserHttpClient, UserHttpClient>();
            services.AddTransient<ICurrencyHttpClient, CurrencyHttpClient>();
            services.AddTransient<IAccountHttpClient, AccountHttpClient>();
            services.AddTransient<ITransactionHttpClient, TransactionHttpClient>();
            services.AddTransient<IUserService, UserAPIService>();
            services.AddTransient<ICurrencyService, CurrencyAPIService>();
            services.AddTransient<IAccountService, AccountAPIService>();
            services.AddTransient<ITransactionService, TransactionAPIService>();

            /*
            services.AddSingleton<ILogger, TextLogger>();
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IAccountLogic, AccountLogic>();
            services.AddTransient<ICurrencyLogic, CurrencyLogic>();
            services.AddTransient<ITransactionLogic, TransactionLogic>();
            services.AddTransient<IUserBroker, SqlUserBroker>();
            services.AddTransient<IAccountBroker, SqlAccountBroker>();
            services.AddTransient<ICurrencyBroker, SqlCurrencyBroker>();
            services.AddTransient<ITransactionBroker, SqlTransactionBroker>();
            services.AddSingleton<IConnector, SqlConnector>();
            services.AddAutoMapper(typeof(GetUserDto).Assembly, typeof(GetAccountDto).Assembly, typeof(TransactionDto).Assembly);
            services.AddTransient<IUserService, UserLogicService>();
            services.AddTransient<ICurrencyService, CurrencyLogicService>();
            services.AddTransient<IAccountService, AccountLogicService>();
            services.AddTransient<ITransactionService, TransactionLogicService>();
            */

            return services.BuildServiceProvider();
        }
    }
}