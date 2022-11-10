using EBanking.Console.Common;
using EBanking.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

// Zbog cirilice
Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;
// Umesto VARCHAR smo u DDL-u za Ime i prezime stavili NVARCHAR da bi sql server mogao da cuva cirilicne karaktere

var databaseType = "sql";
var serviceProvider = HelperMethods.CreateServiceProvider(databaseType);

var mainControler = serviceProvider.GetRequiredService<MainController>();

//TextBuilder.AddText("1")
//    .AddBullet("1.a")
//    .AddBullet("1.b")
//    .Build();
//.AddText("2")
//    .AddBullet("2.a")
//    .AddBullet("2.b")
//    .Build();
//.Build();

//Primjer !
//Console.WriteLine("1");
//Console.WriteLine("\t* 1.a");
//Console.WriteLine("\t* 2.a");
//Console.WriteLine("2");
//Console.WriteLine("\t* 2.a");
//Console.WriteLine("\t* 2.a");

await mainControler.Start();