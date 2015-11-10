using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class Formatter_Test
    { 
        [Test]
        public void CheckOnlyDigits_ShouldNot_StringAndDigits()
        {
            string text = "Token123asdToken";
            bool result = Formatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CheckOnlyDigits_Should_Digits()
        {
            string text = "Token123Token";
            bool result = Formatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(true, result);
        }
        
        [Test]
        public void IsOnlyDigitsBetweenTokens_should()
        {
            string text = "Token";
            bool result = Formatter.IsOnlyDigitsBetweenTokens(text, "Token");
            Assert.AreEqual(false, result);
        }
    }
}