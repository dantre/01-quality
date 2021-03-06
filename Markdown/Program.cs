﻿using System;
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
                var htmlFormatter = new HtmlFormatter();
                var processor = new MarkdownProcessor(allText, htmlFormatter);
                var html = processor.GetMarkdown();
                File.WriteAllText("result.html", html, Encoding.UTF8);
            }
            else
                Console.WriteLine("File not found.");
        }
    }
}
