using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
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
                html += $"<p>{FixParagraph(p)}</p>\r\n";
            }
            return html;
        }
        public string FixParagraph(string paragraph)
        {
            var stack = new Stack<State>();

            var result = "";
            var bufferText = "";
            var bufferRaw = "";

            paragraph = Regex.Replace(paragraph, "\\<", "&lt;");
            paragraph = Regex.Replace(paragraph, "\\>", "&gt;");

            var tokens = GetTokens(paragraph);
            string PossibleTokens = "__`";

            foreach (var token in tokens.Where(t => t!=""))
            {
                bufferRaw += token;
                if (PossibleTokens.Contains(token))
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(StateDictionary[token]);
                        continue;
                    }
                    if (stack.Peek() == State.Slash)
                    {
                        stack.Pop();
                        bufferText += token;
                        continue;
                    }
                    switch (token)
                    {
                        case "_":
                            if (stack.Peek() == State.Ground)
                            {
                                stack.Pop();
                                result += $"<em>{bufferText}</em>";
                                bufferText = "";
                                bufferRaw = "";
                                continue;
                            }
                            break;
                        case "__":
                            if (stack.Peek() == State.DoubleGround)
                            {
                                stack.Pop();
                                result += $"<strong>{bufferText}</strong>";
                                bufferText = "";
                                bufferRaw = "";
                                continue;
                            }
                            break;
                        case "`":
                            if (stack.Peek() == State.Backtick)
                            {
                                stack.Pop();
                                result += $"<code>{bufferText}</code>";
                                bufferText = "";
                                bufferRaw = "";
                                continue;
                            }
                            break;
                    }
                }
                if (token == "\\")
                {
                    stack.Push(State.Slash);
                    continue;
                }

                bufferText += token;
            }
            result += bufferText;
            return result;
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