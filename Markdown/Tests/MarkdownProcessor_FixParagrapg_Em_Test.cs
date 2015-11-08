using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class MarkdownProcessor_FixParagrapg_Em_Test
    {
        [Test]
        public void Ground_Should_ProduceEm()
        {
            var data = "_A_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<em>A</em>");
        }

        [Test]
        public void InsideCode_ShouldNot_Em()
        {
            var data = "`A_B_C`";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<code>A_B_C</code>");
        }

        [Test]
        public void Digits_ShouldNot_InsideEm()
        {
            var data = "_1_ _222_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "_1_ _222_");
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
        public void NotClosedCodeTagWithEm_Shoud_ProduceEm()
        {
            var data = "A`B_C_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A`B<em>C</em>", result);
        }

        [Test]
        public void DoubleGroundInsideGround_Should_ProduceStrongInsideEm()
        {
            var data = "_A__B__C_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<em>A<strong>B</strong>C</em>");
        }
    }
}