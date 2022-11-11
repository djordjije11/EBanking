namespace EBanking.Console.Common
{

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

    /*
new TextBuilder().AddText("1")
        .AddBullet("1.a")
        .AddBullet("1.b")
        .Build()
    .AddText("2")
        .AddBullet("2.a")
        .AddBullet("2.b")
        .Build()
    .Build();

Console.ReadLine();
*/



    internal class TextBuilder
    {
        public string Text { get; set; }

        public TextBuilder() { Text = string.Empty; }
        public TextBuilder AddText(string text)
        {
            Text += text;
            return this;
        }
        public TextBuilder AddBullet(string text)
        {
            Text += $"\n\t* {text}";
            return this;
        }
        public TextBuilder Build()
        {
            System.Console.WriteLine(Text);
            Text = string.Empty;
            return this;
        }
    }
}
