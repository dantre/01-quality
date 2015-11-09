using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Markdown
{
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

        private string GetFormattedText(IList<string> tokens)
        {
            var stack = new Stack<string>();
            int tokenIndex = 0;

            foreach (var token in tokens)
            {
                tokenIndex++;
                if (JustPushToStack(stack, token))
                {
                    stack.Push(token);
                    continue;
                }
                switch (token)
                {
                    case "_":
                        if (IsTokenInsideCode(tokens.ToList(), tokenIndex-1))
                            stack.Push("_");
                        else
                            stack = StackProduceFormattedToken(tokens, stack, "_", HtmlFormatter.FormatHtmlEm);
                        break;
                    case "__":
                        if (IsTokenInsideCode(tokens.ToList(), tokenIndex - 1))
                            stack.Push("__");
                        else
                            stack = StackProduceFormattedToken(tokens, stack, "__", HtmlFormatter.FormatHtmlStrong);
                        break;
                    case "`":
                        var list = ReverseStackToToken(ref stack, token);
                        stack.Push(HtmlFormatter.FormatHtmlCode(string.Join("", list)));
                        break;
                }
            }
            return string.Join("", stack.Reverse());
        }

        private bool IsTokenInsideCode(List<string> tokens, int tokenIndex)
        {
            int codesBeforeTokenCount = 0;
            for (int i = 0; i < tokenIndex; i++)
                if (tokens[i] == "`") codesBeforeTokenCount++;
            int codeAfterToken = 0;
            for (int i = tokenIndex; i < tokens.Count; i++)
                if (tokens[i] == "`")
                {
                    codeAfterToken = 1;
                    break;
                }
            if (codesBeforeTokenCount % 2 == 1 && codeAfterToken == 1)
                return true;
            return false;
        }

        private Stack<string> StackProduceFormattedToken(IList<string> tokens, Stack<string> stack, string token, Func<string, string> format)
        {
            // CR (krait): Неиспользуемая переменная? Неиспользуемый параметр?
            var stackCopy = new Stack<string>(stack.Reverse());
            var list = ReverseStackToToken(ref stack, token);
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

        public List<string> ReverseStackToToken(ref Stack<string> stack, string token)
        {
            var tokens = new List<string> { token };
            while (stack.Peek() != token)
                tokens.Add(stack.Pop());

            tokens.Add(stack.Pop());
            tokens.Reverse();
            return tokens;
        }

        public string RemoveSlashes(string text)
        {
            // CR (krait): Почему не воспользоваться обычным string.Replace, который работает быстрее?
            text = Regex.Replace(text, @"\\_", "_");
            text = Regex.Replace(text, @"\\__", "__");
            text = Regex.Replace(text, @"\\`", "`");
            return text;
        }

        public IList<string> GetTokens(string text)
        {
            return Regex.Split(text, @"(__)|(_)|(\\)|(`)").Where(s => s != "").ToList();
        }

        public string[] GetParagraphs(string text)
        {
            return Regex.Split(text, @"\r\n\s*\r\n");
        }
    }
}