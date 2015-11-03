using NUnit.Framework;

namespace Markdown
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
        public void TwoEnters_Should_TakeRightParagraphs()
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

    [TestFixture]
    class MarkdownProcessor_FixParagraph_Test
    {
        [Test]
        public void Ground_Should_Em()
        {
            var data = "_A_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<em>A</em>");
        }
        [Test]
        public void DoubleGround_Should_Strong()
        {
            var data = "__A__";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<strong>A</strong>");
        }
        [Test]
        public void Backtick_Should_Code()
        {
            var data = "`A`";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<code>A</code>");
        }
        [Test]
        public void StrongInsideEm_Should()
        {
            var data = "_A__B__C_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<em>A<strong>B</strong>C</em>");
        }
        [Test]
        public void EmInsideStrong_ShouldNot()
        {
            var data = "__A_B_C__";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<strong>A_B_C</strong>");
        }
        [Test]
        public void InsideCode_ShouldNot_EmStrong()
        {
            var data = "`A_B__C__D_F`";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "<code>A_B__C__D_F</code>");
        }
        [Test]
        public void Digits_ShouldNot_InsideEm()
        {
            var data = "_1_ _222_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "_1_ _222_");
        }
        [Test]
        public void Can_ScreeningGround()
        {
            var data = "\\_A\\_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "_A_");
        }
        [Test]
        public void Can_ScreeningDoubleGround()
        {
            var data = "\\_\\_A\\_\\_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "__A__");
        }
        [Test]
        public void Can_ScreeningBacktickGround()
        {
            var data = "\\`A\\`";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "`A`");
        }

        [Test]
        public void NotClosedCodeTag_Em()
        {
            var data = "A`B_C_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A`B<em>C</em>", result);
        }
        [Test]
        public void NotClosedCodeTag_Em_Strong()
        {
            var data = "A`B_C_D__E__F";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A`B<em>C</em>D<strong>E</strong>F", result);
        }
        
    }
}
