using System.Collections.Generic;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    internal class MarkdownProcessor_Test
    {
        [Test]
        public void TwoEntersWithSpaces_Should_ProduceTwoParagraphs()
        {
            string data = "A\r\n    \r\nB";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(result.Length, 2);
        }

        [Test]
        public void TwoEnters_Should_GiveTwoParagraphs()
        {
            string data = "A\r\n    \r\nB";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(result[0], "A");
            Assert.AreEqual(result[1], "B");
        }

        [Test]
        public void OneEnter_ShouldNot_ProduceParagraph()
        {
            string data = "A\r\n B";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void TokenString_Should_ParseRight()
        {
            string data = "a_b__c\\d`e";
            var expectedResult = new[] {"a", "_", "b", "__", "c", "\\", "d", "`", "e"};
            var processor = new MarkdownProcessor();

            var result = processor.GetTokens(data);

            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void EmptyParagraphs_Should_Omit()
        {
            string data = "Paragraph1\r\n \r\n \r\n \r\nParagraph2";
            var processor = new MarkdownProcessor();
            var expectedResult = new List<string>() {"Paragraph1","Paragraph2"};

            var result = processor.GetParagraphs(data);
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void RemoveSlashes_Test()
        {
            var processor = new MarkdownProcessor();
            string data = @"\A\_\b\\\`\C";

            var result = processor.RemoveSlashes(data);
            Assert.AreEqual(@"\A_\b\\`\C", result);
        }

        [Test]
        public void StackWithToken_Should_ReverseRight()
        {
            var stack = new Stack<string>();
            stack.Push("__");
            stack.Push("text");
            var expectedTokens = new List<string>() {"__","text","__"};

            var processor = new MarkdownProcessor();
            var result = processor.ReverseStackToToken(ref stack, "__");


            CollectionAssert.AreEqual(expectedTokens, result);
        }

        [Test]
        public void StackWithTwoTokens_Should_ReverseRight()
        {
            var stack = new Stack<string>();
            stack.Push("__");
            stack.Push("text1");
            stack.Push("text2");
            var expectedTokens = new List<string>() { "__", "text1", "text2", "__" };

            var processor = new MarkdownProcessor();
            var result = processor.ReverseStackToToken(ref stack, "__");

            CollectionAssert.AreEqual(expectedTokens, result);
        }
    }
}
