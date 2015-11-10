using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class MarkdownProcessor_FixParagraph_Code_Test
    {
        [Test]
        public void FixParagraph_on_text_inside_backticks_should_give_text_inside_code_tags()
        {
            var data = "`A`";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<code>A</code>");
        }

        [Test]
        public void FixParagraph_on_text_inside_screened_backticks_should_give_text_inside_backticks()
        {
            var data = "\\`A\\`";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "`A`");
        }

        [Test]
        public void FixParagraph_on_text_with_unpaired_backticks_and_paired_underscores_and_double_underscores_should_give_text_with_em_and_strong_tags()
        {
            var data = "A`B_C_D__E__F";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A`B<em>C</em>D<strong>E</strong>F", result);
        }
    }
}