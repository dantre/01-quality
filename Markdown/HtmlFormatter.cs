using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Markdown
{
    public class HtmlFormatter : IFormatter
    {
        private Formatter Formatter { get; set; }
        public HtmlFormatter()
        {
            Formatter = new Formatter();
        }
        public string FormatUnderscore(string text)
        {
            return Formatter.FormatGround(text, "<em>", "</em>");
        }

        public string FormatDoubleUnderscore(string text)
        {
            return Formatter.FormatDoubleGround(text, "<strong>", "</strong>");
        }

        public string FormatBacktick(string text)
        {
            return Formatter.FormatBacktick(text, "<code>", "</code>");
        }

        public string FormatMoreLess(string text)
        {
            return Formatter.FormatGreaterAndLesser(text);
        }
    }
}
