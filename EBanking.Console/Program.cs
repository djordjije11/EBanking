using EBanking.Console.Common;
using EBanking.AppControllers;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

// Zbog cirilice
Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;
// Umesto VARCHAR smo u DDL-u za Ime i prezime stavili NVARCHAR da bi sql server mogao da cuva cirilicne karaktere

var document = new Document()
    .AddParagraph(new TextBuilder()
    .AddCenterText("EBanking App")
    .Build())
    .AddParagraph(new TextBuilder()
    .AddText("Одаберите опцију за приступање бази:")
    .AddBullet("SQL")
    .AddBullet("SQLite")
    .Build());
Console.WriteLine(document.ToString());
var databasetype = Console.ReadLine() ?? "";
var serviceprovider = HelperMethods.CreateServiceProvider(databasetype);
var mainconsole = serviceprovider.GetRequiredService<MainConsole>();
await mainconsole.Start();