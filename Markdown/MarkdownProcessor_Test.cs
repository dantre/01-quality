using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    internal class MarkdownProcessor_Test
    {
        [Test]
        public void TwoEntersWithSpaces_Should_TwoParagraphs()
        {
            string data = "asd\r\n    \r\nasd";

            var result = MarkdownProcessor.GetParagraphs(data);

            Assert.AreEqual(result.Length, 2);
        }

        [Test]
        public void TwoEnters_Should_TakeRightParagraphs()
        {
            string data = "asd\r\n    \r\n asd";

            var result = MarkdownProcessor.GetParagraphs(data);

            Assert.AreEqual(result[0], "asd");
            Assert.AreEqual(result[1], " asd");
        }

        [Test]
        public void OneEnter_ShouldNot_NewParagraph()
        {
            string data = "asd\r\n asd";

            var result = MarkdownProcessor.GetParagraphs(data);

            Assert.AreEqual(result.Length, 1);
        }
        [Test]
        public void TokenString_Should_ParseRight()
        {
            var data = "a_b__c\\d`e";

            var result = MarkdownProcessor.GetTokens(data);

            var expectedResult = new string[] {"a", "_", "b", "__", "c", "\\", "d", "`","e"};
            CollectionAssert.AreEqual(result, expectedResult);
        }
    }
}
