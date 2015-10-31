using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Markdown
{
    internal class MarkdownProcessor
    {
        private string RawText { get; }
        private Dictionary<string, State> StateDictionary { get; set; }
        public MarkdownProcessor()
        {
            InitStateDictionary();
        }
        public MarkdownProcessor(string text)
        {
            RawText = text;
            InitStateDictionary();
        }
        private void InitStateDictionary()
        {
            StateDictionary = new Dictionary<string, State>
            {
                {"_", State.Ground},
                {"__", State.DoubleGround},
                {"`", State.Backtick},
                {"\\", State.Slash}
            };
        }
        public string GetHtml()
        {
            var html = "";
            var paragraphs = GetParagraphs(RawText);
            foreach (var p in paragraphs)
            {
                html += $"<p>{FixParagraph(p)}</p>";
            }
            return html;
        }
        private string FixParagraph(string paragraph)
        {
            var stack = new Stack<State>();

            var result = "";
            var buffer = "";

            var tokens = GetTokens(paragraph);
            string PossibleTokens = "__`\\";
            foreach (var token in tokens)
            {
                if (PossibleTokens.Contains(token))
                {
                    CheckTokens(stack, token);
                }
                else
                {
                    buffer += token;
                }
            }
            return result;
        }
        private void CheckTokens(Stack<State> stack, string token)
        {
            if (stack.Count == 0)
                stack.Push(StateDictionary[token]);
           
        }
        public string[] GetTokens(string text)
        {
            return Regex.Split(text, @"(__)|(_)|(\\)|(`)");
        }
        public string[] GetParagraphs(string text)
        {
            return Regex.Split(text, @"\r\n\s*\r\n");
        }
        private enum State
        {
            Ground, DoubleGround, Backtick, Slash
        }
    }
}