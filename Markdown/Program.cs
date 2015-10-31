using System;
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
            var filename = "Example.txt";
            if (File.Exists(filename))
            {
                var processor = new MarkdownProcessor(filename);
                var html = processor.GetHtml();
                File.WriteAllText("result.html", html);
            }
        }
    }
}
