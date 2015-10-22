using System;
using System.IO;
using System.Linq;

namespace Markdown
{
    internal class MarkdownProcessor
    {
        public string FileName { get; private set; }
        public MarkdownProcessor(string s)
        {
            FileName = s;
            
        }

        public string GetHtml()
        {
            var allText = File.ReadAllText(FileName);
            var paragraphs = GetParagraphs(allText);
            return String.Join("", paragraphs.Select(p => ManageParagraph(p)));
        }

        private string ManageParagraph(string p)
        {
            return String.Format("<p>{0}</p>", PrepareParagraph(p));
        }

        private string PrepareParagraph(string s)
        {
            s = ManageScreening(s);
            s = ManageEm(s);
            s = ManageStrong(s);
            s = ManageCode(s);
            return s;
        }

        private string ManageScreening(string s)
        {
            throw new NotImplementedException();
        }

        private string ManageCode(string s)
        {
            throw new NotImplementedException();
        }

        private string ManageStrong(string s)
        {
            throw new NotImplementedException();
        }

        private string ManageEm(string s)
        {
            throw new NotImplementedException();
        }

        private string[] GetParagraphs(string allText)
        {
            throw new NotImplementedException();
        }
    }
}