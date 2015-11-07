using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    class HtmlFormatter_Test
    {
        [Test]
        public void FormatEm_Should_String()
        {
            string text = "_A_";
            string result = Markdown.HtmlFormatter.FormatHtmlEm(text);
            Assert.AreEqual("<em>A</em>", result);
        }

        [Test]
        public void FormatEm_ShouldNot_Digits()
        {
            string text = "_123_";
            string result = Markdown.HtmlFormatter.FormatHtmlEm(text);
            Assert.AreEqual("_123_", result);
        }
        [Test]
        public void FormatStrong_Should_String()
        {
            string text = "__A__";
            string result = Markdown.HtmlFormatter.FormatHtmlStrong(text);
            Assert.AreEqual("<strong>A</strong>", result);
        }

        [Test]
        public void FormatStrong_ShouldNot_Digits()
        {
            string text = "__123__";
            string result = Markdown.HtmlFormatter.FormatHtmlStrong(text);
            Assert.AreEqual("__123__", result);
        }

        [Test]
        public void FormatCode_Should_String()
        {
            string text = "`A`";
            string result = Markdown.HtmlFormatter.FormatHtmlCode(text);
            Assert.AreEqual("<code>A</code>", result);
        }

        [Test]
        public void FormatGreateAndLesser_Should_MoreSign()
        {
            string text = "\\>";
            string result = Markdown.HtmlFormatter.FormatGreaterAndLesserHtml(text);
            Assert.AreEqual("&gt;", result);
        }

        [Test]
        public void FormatGreateAndLesser_Should_LessSign()
        {
            string text = "\\<";
            string result = Markdown.HtmlFormatter.FormatGreaterAndLesserHtml(text);
            Assert.AreEqual("&lt;", result);
        }

        [Test]
        public void CheckOnlyDigits_ShouldNot_StringAndDigits()
        {
            string text = "Token123asdToken";
            bool result = Markdown.HtmlFormatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CheckOnlyDigits_Should_Digits()
        {
            string text = "Token123Token";
            bool result = Markdown.HtmlFormatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(true, result);
        }
    }
}
