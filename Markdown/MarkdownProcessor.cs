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
            paragraph = HtmlFormatter.FormatGreaterAndLesserHtml(paragraph);
            var tokens = GetTokens(paragraph);
            return RemoveSlashes(GetFormattedText(tokens));
        }

        private string GetFormattedText(IEnumerable<string> tokens)
        {
            var stack = new Stack<string>();

            foreach (var token in tokens)
            {
                if (JustPushToStack(stack, token))
                {
                    stack.Push(token);
                    continue;
                }
                switch (token)
                {
                    case "_":
                        stack = StackProduceFormattedToken(stack, "_", HtmlFormatter.FormatHtmlEm);
                        break;
                    case "__":
                        stack = StackProduceFormattedToken(stack, "__", HtmlFormatter.FormatHtmlStrong);
                        break;
                    case "`":
                        var list = ReverseStackToToken(ref stack, token);
                        stack.Push(HtmlFormatter.FormatHtmlCode(string.Join("", list)));
                        break;
                }
            }
            return string.Join("", stack.Reverse());
        }

        private Stack<string> StackProduceFormattedToken(Stack<string> stack, string token, Func<string,string> format)
        {
            var stackCopy = new Stack<string>(stack.Reverse());
            var list = ReverseStackToToken(ref stack, token);
            if (stack.Contains("`"))
            {
                stack = stackCopy;
                stack.Push(token);
            }
            else
                stack.Push(format(string.Join("", list)));
            return stack;
        }

        private bool JustPushToStack(Stack<string> stack, string token)
        {
            return (stack.Count != 0 && stack.Peek() == "\\") 
                || !IsFormattedToken(token) 
                || (IsFormattedToken(token) && !stack.Contains(token));
        }

        private bool IsFormattedToken(string token)
        {
            return Regex.IsMatch(token, "_|__|`");
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

        private string RemoveSlashes(string text)
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
    }
}