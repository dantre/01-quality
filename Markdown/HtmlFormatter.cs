using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Markdown
{
    public static class HtmlFormatter
    {
        // CR (krait): Стоит ли так привязывать теги к токенам маркдауна? Что, если окажется, что текст между _ нужно форматировать по-другому?
        // CR (krait): Или понадобится рендерить не только в html?
        public static string FormatHtmlEm(string text)
        {
            if (IsOnlyDigitsBetweenTokens(text, "_"))
                return text;
            return Regex.Replace(text, "_(.*)_", "<em>$1</em>");
        }

        public static string FormatHtmlStrong(string text)
        {
            if (IsOnlyDigitsBetweenTokens(text, "__"))
                return text;
            return Regex.Replace(text, "__(.*)__", "<strong>$1</strong>");
        }

        public static string FormatHtmlCode(string text)
        {
            return Regex.Replace(text, "`(.*)`", "<code>$1</code>");
        }

        public static string FormatGreaterAndLesserHtml(string text)
        {
            // CR (krait): Почему не воспользоваться обычным string.Replace, который работает быстрее?
            text = Regex.Replace(text, @"\\<", "&lt;");
            text = Regex.Replace(text, @"\\>", "&gt;");
            return text;
        }

        public static bool IsOnlyDigitsBetweenTokens(string text, string token)
        {
            string regexp = $"{token}(.*){token}";
            var data = Regex.Match(text, regexp);
            // CR (krait): 1. Зачем нужен вызов .ToCharArray()?
            // CR (krait): 2. А почему не может быть так, что матча не будет?
            return data.Groups[1].Value.ToCharArray().All(Char.IsDigit);
        }
    }
}
