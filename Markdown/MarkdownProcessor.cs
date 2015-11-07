using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Markdown
{
    // CR (krait): Здесь есть некоторое количество багов, судя по тому, что даже на примере разметка получается неправильная.
    // CR (krait): Нужно пофиксить баги и написать недостающие тесты, которые бы их выявили.

    public class MarkdownProcessor
    {
        private string RawText { get; }

        public MarkdownProcessor() {}

        public MarkdownProcessor(string text)
        {
            RawText = text;
        }

        public string GetHtml()
        {
            var paragraphs = GetParagraphs(RawText);
            var html = new StringBuilder();
            foreach (var p in paragraphs)
                html.AppendLine($"<p>{FixParagraph(p)}</p>\r\n");
            return html.ToString();
        }

        public string FixParagraph(string paragraph)
        {
            paragraph = HtmlFormatter.FormatGreaterLesserHtml(paragraph);
            var tokens = GetTokens(paragraph);
            return RemoveSlashes(GetFormattedText(tokens));
        }

        private string GetFormattedText(IEnumerable<string> tokens)
        {
            var stack = new Stack<string>();

            foreach (var token in tokens)
            {
                if ((stack.Count != 0 && stack.Peek() == "\\"))
                {
                    stack.Push(token);
                    continue;
                }
                if (!IsFormattedToken(token) || (IsFormattedToken(token) && !stack.Contains(token)))
                {
                    stack.Push(token);
                    continue;
                }

                if (token == "_")
                {
                    var stackCopy = new Stack<string>(stack.Reverse());
                    var list = ReverseStackToToken(ref stack, token);
                    if (stack.Contains("`") || stack.Contains("__"))
                    {
                        stack = stackCopy;
                        stack.Push("_");
                    }
                    else
                        stack.Push(HtmlFormatter.FormatHtmlEm(string.Join("", list)));
                    continue;
                }
                if (token == "__")
                {
                    var stackCopy = new Stack<string>(stack.Reverse());
                    var list = ReverseStackToToken(ref stack, token);
                    if (stack.Contains("`"))
                    {
                        stack = stackCopy;
                        stack.Push("__");
                    }
                    else
                        stack.Push(HtmlFormatter.FormatHtmlStrong(string.Join("", list)));
                    continue;
                }
                if (token == "`")
                {
                    var list = ReverseStackToToken(ref stack, token);
                    stack.Push(HtmlFormatter.FormatHtmlCode(string.Join("", list)));
                    continue;
                }
            }

            return string.Join("", stack.Reverse());
        }


        private List<string> ReverseStackToToken(ref Stack<string> stack, string token)
        {
            var tokens = new List<string> { token };
            while (stack.Peek() != token)
                tokens.Add(stack.Pop());

            tokens.Add(stack.Pop());
            tokens.Reverse();
            return tokens;
        }

        private static string RemoveSlashes(string text)
        {
            text = Regex.Replace(text, @"\\_", "_");
            text = Regex.Replace(text, @"\\__", "__");
            text = Regex.Replace(text, @"\\`", "`");
            return text;
        }

        public IEnumerable<string> GetTokens(string text)
        {
            return Regex.Split(text, @"(__)|(_)|(\\)|(`)").Where(s => s != "");
        }

        public string[] GetParagraphs(string text)
        {
            return Regex.Split(text, @"\r\n\s*\r\n");
        }

        private bool IsFormattedToken(string token)
        {
            return Regex.IsMatch(token, "_|__|`");
        }
    }
}