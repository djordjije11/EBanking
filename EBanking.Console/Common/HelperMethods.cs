using EBanking.AppControllers;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using EBanking.ConfigurationManager.Interfaces;
using EBanking.ConfigurationManager;
using EBanking.Models;
using Newtonsoft.Json;
using EBanking.Console.HttpClients;

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

        public static async Task<T?> GetEntityFromHttpResponse<T>(HttpResponseMessage response) where T : IEntity
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new Exception(responseBody);
            }
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
        public static async Task<IEnumerable<T>?> GetEntitiesFromHttpResponse<T>(HttpResponseMessage response) where T : IEntity
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new Exception(responseBody);
            }
            return JsonConvert.DeserializeObject<IEnumerable<T>?>(responseBody);
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

            return services.BuildServiceProvider();
        }
    }
}