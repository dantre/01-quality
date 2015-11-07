using NUnit.Framework;

namespace Markdown.Tests
{
    // CR (krait): 1. Разные TestFixture должны быть в разных файлах.
    // CR (krait): 2. Не хватает многих тестов. Несколько идей:
    // CR (krait):     - проверить, что пустые параграфы пропускаются
    // CR (krait):     - проверить замену "<" -> "&lt;"
    // CR (krait):     - проверить, что внутри тега code другое форматирование не применяется
    // CR (krait):     - проверить, что непарные _ и __ становятся обычным текстом
    // CR (krait):     - проверить, что _ и __ остаются сами собой в тексте с цифрами
    // CR (krait): 3. Названия тестов можно сделать более понятными. Стоит хотя бы пофиксить грамматику: OneEnter_ShouldNot_NewParagraph -> OneEnter_ShouldNot_ProduceNewParagraph.
    // CR (krait):    Не должно быть названий вида StrongInsideEm_Should.

    [TestFixture]
    class MarkdownProcessor_Test
    {
        [Test]
        public void TwoEntersWithSpaces_Should_TwoParagraphs()
        {
            string data = "A\r\n    \r\nB";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(result.Length, 2);
        }

        [Test]
        public void TwoEnters_Should_TakeTwoParagraphs()
        {
            string data = "A\r\n    \r\nB";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(result[0], "A");
            Assert.AreEqual(result[1], "B");
        }

        [Test]
        public void OneEnter_ShouldNot_NewParagraph()
        {
            string data = "A\r\n B";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(result.Length, 1);
        }

        [Test]
        public void TokenString_Should_ParseRight()
        {
            var data = "a_b__c\\d`e";
            var expectedResult = new[] { "a", "_", "b", "__", "c", "\\", "d", "`", "e" };
            var processor = new MarkdownProcessor();
            
            var result = processor.GetTokens(data);

            CollectionAssert.AreEqual(result, expectedResult);
        }
    }
}
