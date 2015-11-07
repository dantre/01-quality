using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Markdown
{
    public static class HtmlFormatter
    {
        public static string FormatHtmlEm(string text)
        {
            var data = Regex.Match(text, "_(.*)_");
            if (data.Groups[1].Value.ToCharArray().All(d => Char.IsDigit(d)))
                return text;
            return Regex.Replace(text, "_(.*)_", "<em>$1</em>");
        }

        public static string FormatHtmlStrong(string text)
        {
            return Regex.Replace(text, "__(.*)__", "<strong>$1</strong>");
        }

        public static string FormatHtmlCode(string text)
        {
            return Regex.Replace(text, "`(.*)`", "<code>$1</code>");
        }

        public static string FormatGreaterLesserHtml(string text)
        {
            text = Regex.Replace(text, @"\\<", "&lt;");
            text = Regex.Replace(text, @"\\>", "&gt;");
            return text;
        }
    }
}
