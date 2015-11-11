using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Markdown
{
    public class HtmlFormatter : IFormatter
    {
        public string FormatUnderscore(string text)
        {
            return Format(text, "_", "em", true);
        }

        public string FormatDoubleUnderscore(string text)
        {
            return Format(text, "__", "strong", true);
        }

        public string FormatBacktick(string text)
        {
            return Format(text, "`", "code", false);
        }

        public string FormatMoreLess(string text)
        {
            text = text.Replace("\\<", "&lt;");
            text = text.Replace("\\>", "&gt;");
            return text;
        }

        public string FormatParagraph(string text)
        {
            return $"<p>{text}</p>\r\n";
        }

        private string Format(string text, string token, string tag, bool checkDigitsInsideTokens)
        {
            if (checkDigitsInsideTokens && IsOnlyDigitsBetweenTokens(text, token))
                return text;
            return Regex.Replace(text, $"{token}(.*){token}", $"<{tag}>$1</{tag}>");
        }

        public bool IsOnlyDigitsBetweenTokens(string text, string token)
        {
            string regexp = $"{token}(.*){token}";
            var data = Regex.Match(text, regexp);
            if (data.Groups.Count == 2)
                return data.Groups[1].Value.All(Char.IsDigit);
            return false;
        }
    }
}
