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
        public string Filename { get; private set; }
        public MarkdownProcessor(string filename)
        {
            Filename = filename;
        }
        public string GetHtml()
        {
            var data = File.ReadAllText("Example.txt");
            var result = new StringBuilder();

            var paragraphs = Regex.Split(data, @"\r\n\s*\r\n");
            foreach (var paragraph in paragraphs)
            {
                result.AppendLine($"<p>{Fix(paragraph)}</p>");
            }
            return result.ToString();
        }
        private static string Fix(string paragraph)
        {
            Stack<State> stack = new Stack<State>();
            var mas = Regex.Split(paragraph, @"(__)|(_)|(\\)|(`)");

            var result = "";
            var buffer = "";


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
        private enum State
        {
            Ground, DoubleGround, Backtick, Slash
        }
    }
}