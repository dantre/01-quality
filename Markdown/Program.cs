using System;
using System.IO;
using System.Text;

namespace Markdown
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: Markdown.exe [filename]");
                return;
            }
            if (File.Exists(args[0]))
            {
                var allText = File.ReadAllText(args[0]);
                var processor = new MarkdownProcessor(allText);
                var html = processor.GetHtml();
                File.WriteAllText("result.html", html, Encoding.UTF8);
            }
            else
                Console.WriteLine("File not found.");
        }
    }
}
