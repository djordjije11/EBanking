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
    internal class Document
    {
        private string Text { get; set; }
        public Document() { Text = string.Empty; }
        public Document AddParagraph(Paragraph paragraph)
        {
            Text += paragraph.Text;
            return this;
        }
        public override string ToString() => Text;
    }
    internal class Paragraph
    {
        public string Text { get; set; }
        public Paragraph(string text)
        {
            Text = text;
        }
        public Paragraph() { }
    }

    internal class TextBuilder
    {
        public Paragraph Paragraph { get; set; }
        //public string BulletString { get; set; }

        public TextBuilder() { Paragraph = new(); /*BulletString = string.Empty;*/ }
        public TextBuilder AddText(string text)
        {
            Paragraph.Text += text;
            return this;
        }
        public TextBuilder AddCenterText(string text)
        {
            Paragraph.Text += $"\t{text}";
            return this;
        }
        public TextBuilder AddBullet(string text)
        {
            //BulletString += "\t";
            //Text += $"\n{BulletString}* {text}";
            Paragraph.Text += $"\n\t* {text}";
            return this;
        }
        public Paragraph Build()
        {
            //BulletString.Remove(BulletString.Length - 2);

            var paragraph = new Paragraph(Paragraph.Text + "\n");
            Paragraph = new();
            return paragraph;

            //System.Console.WriteLine(Text);
            //Text = string.Empty;
            //return this;
        }
    }
}
