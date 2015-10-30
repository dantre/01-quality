using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Markdown
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver.Solve();
//            if (File.Exists(args[0]))
//            {
//                var processor = new MarkdownProcessor(args[0]);
//                var html = processor.GetHtml();
//                File.WriteAllText("output.html", html);
//            }
        }
    }

    internal static class Solver
    {
        public static void Solve()
        {
            var data = File.ReadAllText("Example.txt");

            var result = new StringBuilder();

            var paragraphs = Regex.Split(data, "\r\n\r\n");
            foreach (var paragraph in paragraphs)
            {
                result.AppendLine($"<p>{Fix(paragraph)}</p>");
            }
            File.WriteAllText("result.html", result.ToString(), Encoding.UTF8);
        }

        private static string Fix(string paragraph)
        {
            paragraph = FixGround(paragraph);
            paragraph = FixDoubleGround(paragraph);
            paragraph = FixBacktick(paragraph);
            return paragraph;
        }
        private static string FixGround(string paragraph)
        {
            return Regex.Replace(paragraph, @"[^\\]_(\w+?)[^\\]_", "<em>{$1}</em>");
        }
        private static string FixDoubleGround(string paragraph)
        {
            return Regex.Replace(paragraph, @"[^\\]__(\w+?)[^\\]__", "<strong>{$1}</strong>");
        }
        private static string FixBacktick(string paragraph)
        {
            return Regex.Replace(paragraph, @"[^\\]`(\w+?)[^\\]`", "<code>{$1}</code>");
        }
    }
}
