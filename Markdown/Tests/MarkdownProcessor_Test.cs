using System.Collections.Generic;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class MarkdownProcessor_Test
    {
        [Test]
        public void GetParagraphs_on_text_with_2_enters_and_spaces_should_give_2_paragraphs()
        {
            string data = "A\r\n    \r\nB";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(result.Length, 2);
        }

        [Test]
        public void GetParagraps_on_text_with_2_enters_and_spaces_should_give_right_text()
        {
            string data = "A\r\n    \r\nB";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(result[0], "A");
            Assert.AreEqual(result[1], "B");
        }

        [Test]
        public void GetParagraphs_on_text_with_one_enter_should_give_one_paragraph()
        {
            string data = "A\r\n B";
            var processor = new MarkdownProcessor();

            var result = processor.GetParagraphs(data);

            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void GetParagraphs_on_string_with_4_enters_and_text_should_give_2_paragraphs()
        {
            string data = "Paragraph1\r\n \r\n \r\n \r\nParagraph2";
            var processor = new MarkdownProcessor();
            var expectedResult = new List<string>() { "Paragraph1", "Paragraph2" };

            var result = processor.GetParagraphs(data);
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetTokens_on_text_with_tokens_should_give_right_tokens()
        {
            string data = "a_b__c\\d`e";
            var expectedResult = new[] {"a", "_", "b", "__", "c", "\\", "d", "`", "e"};
            var processor = new MarkdownProcessor();

            var result = processor.GetTokens(data);

            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void RemoveSlashes_on_text_with_slashes_and_tokens_should_remove_slashes_before_tokens()
        {
            var processor = new MarkdownProcessor();
            string data = @"\A\_\b\\\`\C";

            var result = processor.RemoveSlashes(data);
            Assert.AreEqual(@"\A_\b\\`\C", result);
        }

        [Test]
        public void ReverseStackToToken_on_stack_with_token_and_text_should_give_list_with_text_surrounded_token()
        {
            var stack = new Stack<string>();
            stack.Push("__");
            stack.Push("text");
            var expectedTokens = new List<string>() {"__","text","__"};

            var processor = new MarkdownProcessor();
            var result = processor.ReverseStackToToken(ref stack, "__");

            CollectionAssert.AreEqual(expectedTokens, result);
        }

        [Test]
        public void ReverseStackToToken_on_stack_with_token_and_two_texts_should_give_list_of_tokens_in_right_order()
        {
            var stack = new Stack<string>();
            stack.Push("__");
            stack.Push("text1");
            stack.Push("text2");
            var expectedTokens = new List<string>() { "__", "text1", "text2", "__" };

            var processor = new MarkdownProcessor();
            var result = processor.ReverseStackToToken(ref stack, "__");

            CollectionAssert.AreEqual(expectedTokens, result);
        }
    }
}
