using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class MarkdownProcessor_FixParagraph_Code_Test
    {
        [Test]
        public void Backtick_Should_ProduceCode()
        {
            var data = "`A`";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<code>A</code>");
        }

        [Test]
        public void Can_ScreeningBacktickGround()
        {
            var data = "\\`A\\`";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "`A`");
        }

        [Test]
        public void NotClosedCodeWithEmAndStrong_Should_ProduceEmAndStrong()
        {
            var data = "A`B_C_D__E__F";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A`B<em>C</em>D<strong>E</strong>F", result);
        }
    }
}