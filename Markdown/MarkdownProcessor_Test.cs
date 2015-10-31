using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    class MarkdownProcessor_Test
    {
        [Test]
        public void TwoEntersWithSpaces_Should_TwoParagraphs()
        {
            string data = "A\r\n    \r\nB";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(result.Length, 2);
        }

        [Test]
        public void TwoEnters_Should_TakeRightParagraphs()
        {
            string data = "A\r\n    \r\nB";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(result[0], "A");
            Assert.AreEqual(result[1], "B");
        }

        [Test]
        public void OneEnter_ShouldNot_NewParagraph()
        {
            string data = "A\r\n B";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(result.Length, 1);
        }
        [Test]
        public void TokenString_Should_ParseRight()
        {
            var data = "a_b__c\\d`e";
            var expectedResult = new string[] { "a", "_", "b", "__", "c", "\\", "d", "`", "e" };
            var processor = new MarkdownProcessor();
            
            var result = processor.GetTokens(data);

            CollectionAssert.AreEqual(result, expectedResult);
        }
    }
}
