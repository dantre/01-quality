using System;
using System.IO;
using System.Text;

namespace Markdown
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(args[0]))
            {
                var allText = File.ReadAllText(args[0]);
                var processor = new MarkdownProcessor(allText);
                var html = processor.GetHtml();
                File.WriteAllText("result.html", html, Encoding.UTF8);
            }
            else
            // CR (krait): Если чувак не знает, как пользоваться твоей штукой, ему от такого сообщения легче не станет.
            // CR (krait): Стоит написать, какой аргумент ожидается.
                Console.WriteLine("File not found.");
        }
    }
}
