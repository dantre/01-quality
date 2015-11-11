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
            var htmlFormatter = new HtmlFormatter();
            string result = htmlFormatter.FormatUnderscore(text);
            Assert.AreEqual("<em>A</em>", result);
        }

        [Test]
        public void FormatHtmlEm_should_not_replace_underscores_around_text_containing_only_digits()
        {
            string text = "_123_";
            var htmlFormatter = new HtmlFormatter();
            string result = htmlFormatter.FormatUnderscore(text);
            Assert.AreEqual("_123_", result);
        }

        [Test]
        public void FormatHtmlStrong_should_replace_double_underscores_to_strong_tags()
        {
            string text = "__A__";
            var htmlFormatter = new HtmlFormatter();
            string result = htmlFormatter.FormatDoubleUnderscore(text);
            Assert.AreEqual("<strong>A</strong>", result);
        }

        [Test]
        public void FormatStrong_should_not_replace_digits_between_double_underscore()
        {
            string text = "__123__";
            var htmlFormatter = new HtmlFormatter();
            string result = htmlFormatter.FormatDoubleUnderscore(text);
            Assert.AreEqual("__123__", result);
        }

        [Test]
        public void FormatCode_should_replace_backticks_around_text_to_code_tags()
        {
            string text = "`A`";
            var htmlFormatter = new HtmlFormatter();
            string result = htmlFormatter.FormatBacktick(text);
            Assert.AreEqual("<code>A</code>", result);
        }

        [Test]
        public void FormatGreateAndLesser_should_replace_more_sign_to_amp_gt()
        {
            string text = "\\>";
            var htmlFormatter = new HtmlFormatter();
            string result = htmlFormatter.FormatMoreLess(text);
            Assert.AreEqual("&gt;", result);
        }

        [Test]
        public void FormatGreateAndLesser_should_replace_less_sign_to_amp_lt()
        {
            string text = "\\<";
            var htmlFormatter = new HtmlFormatter();
            string result = htmlFormatter.FormatMoreLess(text);
            Assert.AreEqual("&lt;", result);
        }

        [Test]
        public void IsOnlyDigitsBetweenTokens_on_digits_with_letters_should_give_false()
        {
            string text = "Token123asdToken";
            var htmlFormatter = new HtmlFormatter();
            bool result = htmlFormatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(false, result);
        }

        [Test]
        public void IsOnlyDigitsBetweenTokens_on_digits_should_give_true()
        {
            string text = "Token123Token";
            var htmlFormatter = new HtmlFormatter();
            bool result = htmlFormatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(true, result);
        }

        [Test]
        public void FormatParagaraph_on_text_should_give_text_inside_p_tags_with_enter_on_the_end()
        {
            string text = "text";
            var htmlFormatter = new HtmlFormatter();
            string result = htmlFormatter.FormatParagraph(text);
            Assert.AreEqual("<p>text</p>\r\n", result);
        }
    }
}
