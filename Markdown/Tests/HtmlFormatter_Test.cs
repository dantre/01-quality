using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    internal class HtmlFormatter_Test
    {
        [Test]
        public void FormatHtmlEm_should_replace_underscores_to_em_tags()
        {
            string text = "_A_";
            string result = HtmlFormatter.FormatHtmlEm(text);
            Assert.AreEqual("<em>A</em>", result);
        }

        [Test]
        public void FormatHtmlEm_should_not_replace_underscores_around_text_containing_only_digits()
        {
            string text = "_123_";
            string result = HtmlFormatter.FormatHtmlEm(text);
            Assert.AreEqual("_123_", result);
        }

        [Test]
        public void FormatHtmlStrong_should_replace_double_underscores_to_strong_tags()
        {
            string text = "__A__";
            string result = HtmlFormatter.FormatHtmlStrong(text);
            Assert.AreEqual("<strong>A</strong>", result);
        }

        [Test]
        public void FormatStrong_should_not_replace_digits_between_double_underscore()
        {
            string text = "__123__";
            string result = HtmlFormatter.FormatHtmlStrong(text);
            Assert.AreEqual("__123__", result);
        }

        [Test]
        public void FormatCode_should_replace_backticks_around_text_to_code_tags()
        {
            string text = "`A`";
            string result = HtmlFormatter.FormatHtmlCode(text);
            Assert.AreEqual("<code>A</code>", result);
        }

        [Test]
        public void FormatGreateAndLesser_should_replace_more_sign_to_amp_gt()
        {
            string text = "\\>";
            string result = HtmlFormatter.FormatGreaterAndLesserHtml(text);
            Assert.AreEqual("&gt;", result);
        }

        [Test]
        public void FormatGreateAndLesser_should_replace_less_sign_to_amp_lt()
        {
            string text = "\\<";
            string result = HtmlFormatter.FormatGreaterAndLesserHtml(text);
            Assert.AreEqual("&lt;", result);
        }
    }
}
