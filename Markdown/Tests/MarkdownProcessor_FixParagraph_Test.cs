using NUnit.Framework;

namespace Markdown.Tests
{
    // CR (krait): 1. ������ TestFixture ������ ���� � ������ ������. +
    // CR (krait): 2. �� ������� ������ ������. ��������� ����:
    // CR (krait):     - ���������, ��� ������ ��������� ������������ +
    // CR (krait):     - ��������� ������ "<" -> "&lt;"  +
    // CR (krait):     - ���������, ��� ������ ���� code ������ �������������� �� ����������� +
    // CR (krait):     - ���������, ��� �������� _ � __ ���������� ������� ������� +
    // CR (krait):     - ���������, ��� _ � __ �������� ���� ����� � ������ � ������� +
    // CR (krait): 3. �������� ������ ����� ������� ����� ���������. ����� ���� �� ��������� ����������: OneEnter_ShouldNot_NewParagraph -> OneEnter_ShouldNot_ProduceNewParagraph. +
    // CR (krait):    �� ������ ���� �������� ���� StrongInsideEm_Should. +
    [TestFixture]
    class MarkdownProcessor_FixParagraph_Test
    {
        [Test]
        public void MoreSign_Should_Greater()
        {
            string data = @"A\>B";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A&gt;B", result);
        }

        [Test]
        public void LessSign_Should_Lesser()
        {
            string data = @"A\<B";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("A&lt;B", result);
        }

        [Test]
        public void NotPairedGround_ShouldNot_Em()
        {
            string data = "_ABC";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("_ABC", result);
        }

        [Test]
        public void NotPairedDoubleGround_ShouldNot_Strong()
        {
            string data = "__A";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("__A", result);
        }

        [Test]
        public void GroundAndDoubleGroundOnDifferentSide_ShouldNot_ProduceTags()
        {
            string data = "_A__";
            var processor = new MarkdownProcessor();
        
            var result = processor.FixParagraph(data);
        
            Assert.AreEqual("_A__", result);
        }

        [Test]
        public void NotPairedBacktick_ShouldNot_Code()
        {
            string data = "`A";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("`A", result);
        }

        [Test]
        public void DigitsInsideGround_ShouldNot_ProduceEm()
        {
            string data = "_123_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("_123_", result);
        }
        [Test]
        public void DigitsWithCharInsideGround_Should_ProduceEm()
        {
            string data = "_123ABC_";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("<em>123ABC</em>", result);
        }

        [Test]
        public void DigitsInsideDoubleGround_ShouldNot_ProduceStrong()
        {
            string data = "__123__";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("__123__", result);
        }
        [Test]
        public void DigitsWithCharInsideDoubleGround_Should_ProduceStrong()
        {
            string data = "__123ABC__";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual("<strong>123ABC</strong>", result);
        }
    }
}