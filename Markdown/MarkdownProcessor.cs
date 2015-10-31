using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Markdown
{
    internal class MarkdownProcessor
    {
        private string RawText { get; set; }
        public MarkdownProcessor(string text)
        {
            RawText = text;
        }
        public string GetHtml()
        {
            var html = "";
            var paragraphs = GetParagraphs();
            foreach (var p in paragraphs)
            {
                html += $"<p>{FixParagraph(p)}</p>";
            }
            return html;
        }
        private static string FixParagraph(string paragraph)
        {
            Stack<State> stack = new Stack<State>();

            var result = "";
            var buffer = "";

            var mas = Regex.Split(paragraph, @"(__)|(_)|(\\)|(`)");
            foreach (var ma in mas)
            {
                switch (ma)
                {
                    case "_":
                        stack.Push(State.Ground);
                        break;
                    case "__":
                        stack.Push(State.DoubleGround);
                        break;
                    case "`":
                        stack.Push(State.Backtick);
                        break;
                    case "\\":
                        stack.Push(State.Slash);
                        break;
                }
                result += $"###{ma}###";
            }
            return result;
        }

        public string[] GetParagraphs()
        {
            return Regex.Split(RawText, @"\r\n\s*\r\n");
        }
        private enum State
        {
            Ground, DoubleGround, Backtick, Slash
        }
    }
}