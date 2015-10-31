using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Markdown
{
    internal class MarkdownProcessor
    {
        private string RawText { get; }
        public MarkdownProcessor() {}
        public MarkdownProcessor(string text)
        {
            RawText = text;
        }
        public string GetHtml()
        {
            var html = "";
            var paragraphs = GetParagraphs(RawText);
            foreach (var p in paragraphs)
            {
                html += $"<p>{FixParagraph(p)}</p>\r\n";
            }
            return html;
        }
        public string FixParagraph(string paragraph)
        {
            paragraph = Regex.Replace(paragraph, "\\\\<", "&lt;");
            paragraph = Regex.Replace(paragraph, "\\\\>", "&gt;");
            var tokens = GetTokens(paragraph);
            return GetFormattedText(tokens);
        }

        private string GetFormattedText(string[] tokens)
        {
            var stack = new Stack<string>();

            foreach (var token in tokens)
            {
                if (token=="_" && stack.Contains("_"))
                {
                    
                    List<string> list = new List<string>();
                    list.Add(token);
                    while (stack.Peek() != token)
                    {
                        list.Add(stack.Pop());
                    }
                    list.Add(stack.Pop());
                    list.Reverse();

                    stack.Push(FormatHtmlEm(string.Join("", list)));
                    continue;
                }
                if (token == "__" && stack.Contains("__") && !stack.Contains("`"))
                {
                    List<string> list = new List<string>();
                    list.Add(token);
                    while (stack.Peek() != token)
                    {
                        list.Add(stack.Pop());
                    }
                    list.Add(stack.Pop());
                    list.Reverse();

                    stack.Push(FormatHtmlStrong(string.Join("", list)));
                    continue;
                }
                if (token == "`" && stack.Contains("`"))
                {
                    List<string> list = new List<string>();
                    list.Add(token);
                    while (stack.Peek() != token)
                    {
                        list.Add(stack.Pop());
                    }
                    list.Add(stack.Pop());
                    list.Reverse();

                    stack.Push(FormatHtmlCode(string.Join("", list)));
                    continue;
                }
                stack.Push(token);
            }
            return string.Join("", stack.Reverse());
        }

        private bool IsToken(string token)
        {
            return "__`".Contains(token);
        }

        private string FormatHtml(string result)
        {
            return result;
        }

        private string FormatHtmlEm(string text)
        {
            return Regex.Replace(text, "_(.*)_", "<em>$1</em>");
        }
        private string FormatHtmlStrong(string text)
        {
            return Regex.Replace(text, "__(.*)__", "<strong>$1</strong>");
        }
        private string FormatHtmlCode(string text)
        {
            return Regex.Replace(text, "`(.*)`", "<code>$1</code>");
        }


        public string[] GetTokens(string text)
        {
            return Regex.Split(text, @"(__)|(_)|(\\)|(`)");
        }
        public string[] GetParagraphs(string text)
        {
            return Regex.Split(text, @"\r\n\s*\r\n");
        }
    }
}