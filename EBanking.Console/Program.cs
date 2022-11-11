using EBanking.Console.Common;
using EBanking.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

// Zbog cirilice
Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;
// Umesto VARCHAR smo u DDL-u za Ime i prezime stavili NVARCHAR da bi sql server mogao da cuva cirilicne karaktere

new TextBuilder().AddText("Одаберите опцију за приступање бази:").AddBullet("SQL").AddBullet("SQLite").Build();
var databaseType = Console.ReadLine();
var serviceProvider = HelperMethods.CreateServiceProvider(databaseType);
var mainControler = serviceProvider.GetRequiredService<MainController>();
await mainControler.Start();