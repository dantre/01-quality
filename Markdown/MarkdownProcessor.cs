using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Markdown
{
    class MarkdownProcessor
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
        private string GetFormattedText(IEnumerable<string> tokens)
        {
            var stack = new Stack<string>();

            foreach (var token in tokens)
            {
                if (token=="_" && stack.Contains("_") && stack.Peek()!="\\")
                {
                    var stackCopy = new Stack<string>(stack.Reverse());
                    var list = ReverseStackToToken(ref stack, token);
                    if (stack.Contains("`") || stack.Contains("__"))
                    {
                        stack = stackCopy;
                        stack.Push("_");
                    }
                    else
                        stack.Push(FormatHtmlEm(string.Join("", list)));
                    continue;
                }
                if (token == "__" && stack.Contains("__") && !stack.Contains("`") && stack.Peek() != "\\")
                {
                    var list = ReverseStackToToken(ref stack, token);
                    stack.Push(FormatHtmlStrong(string.Join("", list)));
                    continue;
                }
                if (token == "`" && stack.Contains("`") && stack.Peek() != "\\")
                {
                    var list = ReverseStackToToken(ref stack, token);
                    stack.Push(FormatHtmlCode(string.Join("", list)));
                    continue;
                }
                stack.Push(token);
            }
            return string.Join("", stack.Reverse());
        }
        private List<string> ReverseStackToToken(ref Stack<string> stack, string token)
        {
            List<string> tokens = new List<string>();
            tokens.Add(token);
            while (stack.Peek() != token)
            {
                tokens.Add(stack.Pop());
            }
            tokens.Add(stack.Pop());
            tokens.Reverse();
            return tokens;
        }
        private string FormatHtmlEm(string text)
        {
            var data = Regex.Match(text, "_(.*)_");
            if (data.Groups[1].Value.ToCharArray().All(d => Char.IsDigit(d)))
                return text;
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
        public IEnumerable<string> GetTokens(string text)
        {
            return Regex.Split(text, @"(__)|(_)|(\\)|(`)").Where(s => s!="");
        }
        public string[] GetParagraphs(string text)
        {
            return Regex.Split(text, @"\r\n\s*\r\n");
        }
    }
}