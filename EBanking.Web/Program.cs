using EBanking.BusinessLayer;
using EBanking.BusinessLayer.Interfaces;
using EBanking.ConfigurationManager;
using EBanking.ConfigurationManager.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;
using EBanking.SqlDataAccess.SqlBrokers;
using EBanking.SqlDataAccess.SqlConnectors;
using ILogger = EBanking.Models.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ILogger, TextLogger>();
builder.Services.AddTransient<IUserLogic, UserLogic>();
builder.Services.AddTransient<IAccountLogic, AccountLogic>();
builder.Services.AddTransient<ICurrencyLogic, CurrencyLogic>();
builder.Services.AddTransient<ITransactionLogic, TransactionLogic>();
builder.Services.AddTransient<IUserBroker, SqlUserBroker>();
builder.Services.AddTransient<IAccountBroker, SqlAccountBroker>();
builder.Services.AddTransient<ICurrencyBroker, SqlCurrencyBroker>();
builder.Services.AddTransient<ITransactionBroker, SqlTransactionBroker>();
builder.Services.AddSingleton<IConnector, SqlConnector>();

var filePath = "config.sql.json";
builder.Services.AddSingleton<IConfigurationManager>(_ =>
{
    var configurationManager = new JsonFileConfigurationManager();
    configurationManager.Initialize(filePath);
    return configurationManager;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
