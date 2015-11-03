using System.IO;
using System.Text;

namespace Markdown
{
    static class Program
    {
        static void Main(string[] args)
        {
            // CR (krait): Для удобства имя файла стоит принимать через аргументы.
            var filename = "Example.txt";
            if (File.Exists(filename))
            {
                var allText = File.ReadAllText(filename);
                var processor = new MarkdownProcessor(allText);
                var html = processor.GetHtml();
                File.WriteAllText("result.html", html, Encoding.UTF8);
            }
        }
    }
}
