using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class MarkdownProcessor_FixParagrapg_Strong_Test
    {
        [Test]
        public void DoubleGround_Should_ProduceStrong()
        {
            var data = "__A__";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<strong>A</strong>");
        }

        [Test]
        public void InsideCode_ShouldNot_Strong()
        {
            var data = "`A__B__C`";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<code>A__B__C</code>");
        }

        [Test]
        public void Digits_ShouldNot_InsideStrong()
        {
            var data = "__1__ __222__";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "__1__ __222__");
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
        public void NotClosedCodeTagWithStrong_Should_ProduceStrong()
        {
            var data = "A`B__C__";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A`B<strong>C</strong>", result);
        }
    }
}