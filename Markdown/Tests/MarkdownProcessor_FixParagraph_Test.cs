using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    class MarkdownProcessor_FixParagraph_Test
    {
        [Test]
        public void FixParagraph_on_text_with_more_sign_should_give_text_with_amp_gt()
        {
            string data = @"A\>B";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A&gt;B", result);
        }

        [Test]
        public void FixParagraph_on_text_with_less_sing_should_give_text_with_amp_lt()
        {
            string data = @"A\<B";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A&lt;B", result);
        }

        [Test]
        public void FixParagraph_on_text_with_one_underscore_should_not_give_em_tag()
        {
            string data = "_ABC";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("_ABC", result);
        }

        [Test]
        public void FixParagraph_on_text_with_one_double_underscore_should_not_give_strong_tag()
        {
            string data = "__A";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("__A", result);
        }

        [Test]
        public void FixParagraph_on_text_with_underscore_and_double_underscore_on_different_sides_should_give_the_same_text()
        {
            string data = "_A__";
            var processor = new MarkdownProcessor(new HtmlFormatter());
        
            var result = processor.FixParagraph(data);
        
            Assert.AreEqual("_A__", result);
        }

        [Test]
        public void FixParagraph_on_text_with_one_backtick_should_give_the_same_text()
        {
            string data = "`A";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("`A", result);
        }

        [Test]
        public void FixParagraph_on_digits_inside_underscores_should_give_the_same_text()
        {
            string data = "_123_";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("_123_", result);
        }

        [Test]
        public void FixParagraph_on_digits_with_letters_inside_underscores_should_give_text_inside_em_tags()
        {
            string data = "_123ABC_";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("<em>123ABC</em>", result);
        }

        [Test]
        public void FixParagraph_on_digits_inside_double_underscores_should_give_the_same_text()
        {
            string data = "__123__";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("__123__", result);
        }
        [Test]
        public void FixParagraph_on_digits_with_letters_inside_double_underscores_should_give_text_inside_strong_tags()
        {
            string data = "__123ABC__";
            var processor = new MarkdownProcessor(new HtmlFormatter());

            var result = processor.FixParagraph(data);

            Assert.AreEqual("<strong>123ABC</strong>", result);
        }
    }
}