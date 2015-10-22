using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(args[0]))
            {
                var processor = new MarkdownProcessor(args[0]);
                var html = processor.GetHtml();
                File.WriteAllText("output.html", html);
            }
        }
    }
}
