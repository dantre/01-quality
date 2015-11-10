using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Markdown
{
    public class MarkdownProcessor
    {
        private readonly string rawText;
        private readonly IFormatter formatter;

        public MarkdownProcessor(IFormatter formatter)
        {
            this.formatter = formatter;
        }

        public MarkdownProcessor(string text, IFormatter formatter)
        {
            rawText = text;
            this.formatter = formatter;
        }
        public string GetMarkdown()
        {
            var paragraphs = GetParagraphs(rawText);
            var markdown = new StringBuilder();
            foreach (var p in paragraphs)
                markdown.AppendLine($"<p>{FixParagraph(p)}</p>\r\n");
            return markdown.ToString();
        }

        public string FixParagraph(string paragraph)
        {
            paragraph = formatter.FormatMoreLess(paragraph);
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
                            stack = StackProduceFormattedToken(stack, "_", formatter.FormatUnderscore);
                        break;
                    case "__":
                        if (IsTokenInsideCode(tokens.ToList(), tokenIndex - 1))
                            stack.Push("__");
                        else
                            stack = StackProduceFormattedToken(stack, "__", formatter.FormatDoubleUnderscore);
                        break;
                    case "`":
                        var list = ReverseStackToToken(ref stack, token);
                        stack.Push(formatter.FormatBacktick(string.Join("", list)));
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

        private Stack<string> StackProduceFormattedToken(Stack<string> stack, string token, Func<string, string> format)
        {
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
            text = text.Replace("\\_", "_");
            text = text.Replace("\\__", "__");
            text = text.Replace("\\`", "`");
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