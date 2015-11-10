using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class MarkdownProcessor_FixParagrapg_m_Test
    {
        [Test]
        public void FixParagraph_on_text_inside_underscores_should_give_text_inside()
        {
            var data = "_A_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<em>A</em>");
        }

        [Test]
        public void FixParagraph_on_text_inside_underscores_inside_backtick_should_give_text_inside_underscores_and_code()
        {
            var data = "`A_B_C`";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<code>A_B_C</code>");
        }

        [Test]
        public void FixParagraph_on_digits_inside_underscores_should_give_digits_inside_underscores()
        {
            var data = "_1_ _222_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "_1_ _222_");
        }

        [Test]
        public void FixParagraph_on_text_inside_screened_underscores_give_text_inside_underscores()
        {
            var data = "\\_A\\_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "_A_");
        }

        [Test]
        public void FixParagraph_on_unclosed_backtick_and_text_inside_underscore_should_give_text_inside_em_tag()
        {
            var data = "A`B_C_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A`B<em>C</em>", result);
        }

        [Test]
        public void FixParagraph_on_text_with_paired_underscore_and_paired_double_underscores_should_give_text_with_em_and_strong_tags()
        {
            var data = "_A__B__C_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<em>A<strong>B</strong>C</em>");
        }
    }
}