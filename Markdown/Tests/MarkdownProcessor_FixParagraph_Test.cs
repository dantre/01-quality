using NUnit.Framework;
// ReSharper disable All

namespace Markdown
{
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
        public void Digits_ShouldNot_InsideStrong()
        {
            var data = "__1__ __222__";
            var processor = new MarkdownProcessor();

            var result = processor.FixParagraph(data);

            Assert.AreEqual(result, "__1__ __222__");
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

//        [Test]
//        public void NotClosedCodeTag_Em()
//        {
//            var data = "A`B_C_";
//            var processor = new MarkdownProcessor();
//
//            var result = processor.FixParagraph(data);
//
//            Assert.AreEqual("A`B<em>C</em>", result);
//        }
//
//        [Test]
//        public void NotClosedCodeTag_Em_Strong()
//        {
//            var data = "A`B_C_D__E__F";
//            var processor = new MarkdownProcessor();
//
//            var result = processor.FixParagraph(data);
//
//            Assert.AreEqual("A`B<em>C</em>D<strong>E</strong>F", result);
//        }
    }
}