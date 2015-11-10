using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class Formatter_Test
    { 
        [Test]
        public void IsOnlyDigitsBetweenTokens_on_digits_with_letters_should_give_false()
        {
            string text = "Token123asdToken";
            bool result = Formatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(false, result);
        }

        [Test]
        public void IsOnlyDigitsBetweenTokens_on_digits_should_give_true()
        {
            string text = "Token123Token";
            bool result = Formatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(true, result);
        }
    }
}