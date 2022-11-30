using EBanking.BusinessLayer;
using EBanking.BusinessLayer.Interfaces;
using EBanking.ConfigurationManager.Interfaces;
using EBanking.ConfigurationManager;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.SqlDataAccess.SqlBrokers;
using EBanking.SqlDataAccess.SqlConnectors;
using EBanking.Models;
using ILogger = EBanking.Models.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILogger, TextLogger>();
builder.Services.AddTransient<IUserLogic, UserLogic>();
builder.Services.AddTransient<IUserBroker, SqlUserBroker>();
builder.Services.AddSingleton<IConnector, SqlConnector>();
//var filePath = "D:\\MyDocs\\Repositories\\EBanking App\\SqlDataAccesss\\config.sql.json";
var filePath = "config.sql.json";
builder.Services.AddSingleton<IConfigurationManager>(_ =>
{
    var configurationManager = new JsonFileConfigurationManager();
    configurationManager.Initialize(filePath);
    return configurationManager;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
