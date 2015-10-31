using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace Markdown
{
    [TestFixture]
    class MarkdownProcessor_GetParagraph
    {
        [Test]
        public void TwoEntersWithSpaces_Should_TwoParagraphs()
        {
            string data = "asd\r\n    \r\nasd";
            var processor = new MarkdownProcessor(data);

            var result = processor.GetParagraphs();

            Assert.AreEqual(result.Length, 2);
        }
        [Test]
        public void TwoEnters_Should_TakeRightParagraphs()
        {
            string data = "asd\r\n    \r\n asd";
            var processor = new MarkdownProcessor(data);

            var result = processor.GetParagraphs();

            Assert.AreEqual(result[0], "asd");
            Assert.AreEqual(result[1], " asd");
        }
        [Test]
        public void OneEnter_ShouldNot_NewParagraph()
        {
            string data = "asd\r\n asd";
            var processor = new MarkdownProcessor(data);

            var result = processor.GetParagraphs();

            Assert.AreEqual(result.Length, 1);
        }

    }
}
