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
            return Formatter.FormatGround(text, "<em>", "</em>");
        }

        public static string FormatHtmlStrong(string text)
        {
            return Formatter.FormatDoubleGround(text, "<strong>", "</strong>");
        }

        public static string FormatHtmlCode(string text)
        {
            return Formatter.FormatBacktick(text, "<code>", "</code>");
        }

        public static string FormatGreaterAndLesserHtml(string text)
        {
            text = text.Replace("\\<", "&lt;");
            text = text.Replace("\\>", "&gt;");
            return text;
        }
    }
}
