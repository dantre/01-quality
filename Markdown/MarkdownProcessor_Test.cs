using NUnit.Framework;
// ReSharper disable All

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

    [TestFixture]
    class MarkdownProcessor_FixParagraph_Test
    {
        [Test]
        public void Ground_Should_Em()
        {
            var data = "_A_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<em>A</em>");
        }

        [Test]
        public void DoubleGround_Should_Strong()
        {
            var data = "__A__";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<strong>A</strong>");
        }
        [Test]
        public void Can_ScreeningGround()
        {
            var data = "\\_A\\_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "_A_");
        }
        [Test]
        public void Can_ScreeningDoubleGround()
        {
            var data = "\\_\\_A\\_\\_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "__A__");
        }
        [Test]
        public void Can_ScreeningBacktickGround()
        {
            var data = "\\`A\\`";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "`A`");
        }
    }
}
