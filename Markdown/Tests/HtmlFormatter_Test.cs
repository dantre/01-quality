using NUnit.Framework;

namespace Markdown.Tests
{
    // CR (krait): С названиями тестов беда. Надо сделать, чтобы было понятно.
    // CR (krait): Переименовал несколько тестов, чтобы было понятно, к чему нужно стремиться.

    [TestFixture]
    class HtmlFormatter_Test
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
        public void FormatStrong_ShouldNot_Digits()
        {
            string text = "__123__";
            string result = HtmlFormatter.FormatHtmlStrong(text);
            Assert.AreEqual("__123__", result);
        }

        [Test]
        public void FormatCode_Should_String()
        {
            string text = "`A`";
            string result = HtmlFormatter.FormatHtmlCode(text);
            Assert.AreEqual("<code>A</code>", result);
        }

        [Test]
        public void FormatGreateAndLesser_Should_MoreSign()
        {
            string text = "\\>";
            string result = HtmlFormatter.FormatGreaterAndLesserHtml(text);
            Assert.AreEqual("&gt;", result);
        }

        [Test]
        public void FormatGreateAndLesser_Should_LessSign()
        {
            string text = "\\<";
            string result = HtmlFormatter.FormatGreaterAndLesserHtml(text);
            Assert.AreEqual("&lt;", result);
        }

        [Test]
        public void CheckOnlyDigits_ShouldNot_StringAndDigits()
        {
            string text = "Token123asdToken";
            bool result = HtmlFormatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CheckOnlyDigits_Should_Digits()
        {
            string text = "Token123Token";
            bool result = HtmlFormatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(true, result);
        }
    }
}
