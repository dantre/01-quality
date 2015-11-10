using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class MarkdownProcessor_FixParagrapg_Strong_Test
    {
        [Test]
        public void FixParagraph_on_text_around_double_unbderscore_should_give_text_inside_strong_tags()
        {
            var data = "__A__";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<strong>A</strong>");
        }

        [Test]
        public void FixParagraph_on_text_around_double_unbderscore_with_inside_backticks_should_not_give_text_inside_code_tags_without_strong_tags()
        {
            var data = "`A__B__C`";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<code>A__B__C</code>");
        }

        [Test]
        public void FixParagraph_on_digits_inside_underscores_should_give_same_text()
        {
            var data = "__1__ __222__";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "__1__ __222__");
        }

        [Test]
        public void FixParagraph_on_text_with_screening_tokens_should_give_same_text()
        {
            var data = "\\_\\_A\\_\\_";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "__A__");
        }

        [Test]
        public void FixParagraph_on_text_with_not_closed_backtick_and_double_underscore_should_give()
        {
            var data = "A`B__C__";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A`B<strong>C</strong>", result);
        }
    }
}