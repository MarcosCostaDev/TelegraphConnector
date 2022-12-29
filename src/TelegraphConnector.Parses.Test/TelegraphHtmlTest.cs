using FluentAssertions;

namespace TelegraphConnector.Parses.Test
{
    public class TelegraphHtmlTest : AbstractTest
    {
        [Fact]
        public void HtmlParse_example_1_with_body_tag()
        {
            var htmlContent = GetTextFromFile("example_1.html");

            var sut = TelegraphHtml.Parse(htmlContent);

            sut.Should().HaveCount(1);

            sut.ElementAt(0).Tag.Should().Be("p");
            sut.ElementAt(0).Children.Should().HaveCount(1);
            sut.ElementAt(0).Children[0].Tag.Should().Be("_text");
            sut.ElementAt(0).Children[0].Value.Should().ContainEquivalentOf("This is some content with nested tags");
        }


        [Fact]
        public void HtmlParse_example_2_with_body_tag_3_paragraphs()
        {
            var htmlContent = GetTextFromFile("example_2.html");

            var sut = TelegraphHtml.Parse(htmlContent);

            sut.Should().HaveCount(5);
           
            var firstParagraph = sut.ElementAt(1);
            var secondParagraph = sut.ElementAt(2);
            var thirdParagraph = sut.ElementAt(4);

            firstParagraph.Children[0].Tag.Should().Be("_text");
            firstParagraph.Children[0].Value.Should().ContainEquivalentOf("This is the first paragraph. It contains a red span and a blue span.");

            secondParagraph.Children[1].Tag.Should().Be("ol");

            var listOrdered = secondParagraph.Children.ElementAt(1);
            listOrdered.Children[0].Tag.Should().Be("li");
            listOrdered.Children[0].Children[0].Value.Should().Be("Item 1");

            listOrdered.Children[1].Tag.Should().Be("li");
            listOrdered.Children[1].Children[0].Value.Should().Be("Item 2");

            listOrdered.Children[2].Tag.Should().Be("li");
            listOrdered.Children[2].Children[0].Value.Should().Be("Item 3");

            secondParagraph.Children[2].Tag.Should().Be("_text");
            secondParagraph.Children[2].Value.Should().ContainEquivalentOf("This is a text after the list in the second paragraph.");

            thirdParagraph.Children[0].Tag.Should().Be("_text");
            thirdParagraph.Children[0].Value.Should().ContainEquivalentOf("This is the third paragraph. It contains an unordered list:");
            thirdParagraph.Children[1].Tag.Should().Be("h4");

            thirdParagraph.Children[2].Tag.Should().Be("ul");
            var listUnordered = thirdParagraph.Children.ElementAt(2);

            listUnordered.Children[0].Tag.Should().Be("li");
            listUnordered.Children[0].Children[0].Value.Should().Be("Item A");

            listUnordered.Children[1].Tag.Should().Be("li");
            listUnordered.Children[1].Children[0].Value.Should().Be("Item B");

            listUnordered.Children[2].Tag.Should().Be("li");
            listUnordered.Children[2].Children[0].Value.Should().Be("Item C");

        }


        [Fact]
        public void HtmlParse_example_3_without_body_tag_3_paragraphs()
        {
            var htmlContent = GetTextFromFile("example_3.html");

            var sut = TelegraphHtml.Parse(htmlContent);

            sut.Should().HaveCount(3);

            var firstParagraph = sut.ElementAt(0);
            var secondParagraph = sut.ElementAt(1);
            var thirdParagraph = sut.ElementAt(2);

            firstParagraph.Children[0].Tag.Should().Be("_text");
            firstParagraph.Children[0].Value.Should().ContainEquivalentOf("This is the");

            firstParagraph.Children[1].Tag.Should().Be("strong");
            firstParagraph.Children[1].Children[0].Tag.Should().Be("_text");
            firstParagraph.Children[1].Children[0].Value.Should().ContainEquivalentOf("first paragraph");

            firstParagraph.Children[2].Tag.Should().Be("_text");
            firstParagraph.Children[2].Value.Should().ContainEquivalentOf(". It contains a red span and a blue span.");

            secondParagraph.Children[0].Tag.Should().Be("_text");
            secondParagraph.Children[0].Value.Should().ContainEquivalentOf("This is the second paragraph. It contains an ordered list:");
            
            secondParagraph.Children[1].Tag.Should().Be("ol");
            var listOrdered = secondParagraph.Children.ElementAt(1);
            listOrdered.Children[0].Tag.Should().Be("li");
            listOrdered.Children[0].Children[0].Value.Should().Be("Item 1");

            listOrdered.Children[1].Tag.Should().Be("li");
            listOrdered.Children[1].Children[0].Value.Should().Be("Item 2");

            listOrdered.Children[2].Tag.Should().Be("li");
            listOrdered.Children[2].Children[0].Value.Should().Be("Item 3");

            thirdParagraph.Children[0].Tag.Should().Be("_text");
            thirdParagraph.Children[0].Value.Should().ContainEquivalentOf("This is the third paragraph. It contains an unordered list:");

            thirdParagraph.Children[1].Tag.Should().Be("ul");
            var listUnordered = thirdParagraph.Children.ElementAt(1);

            listUnordered.Children[0].Tag.Should().Be("li");
            listUnordered.Children[0].Children[0].Value.Should().Be("Item A");

            listUnordered.Children[1].Tag.Should().Be("li");
            listUnordered.Children[1].Children[0].Value.Should().Be("Item B");

            listUnordered.Children[2].Tag.Should().Be("li");
            listUnordered.Children[2].Children[0].Value.Should().Be("Item C");
        }

        [Fact]
        public void HtmlParse_example_4_without_body_tag_3_paragraphs()
        {
            var htmlContent = GetTextFromFile("example_4.html");

            var sut = TelegraphHtml.Parse(htmlContent);

            var firstParagraph = sut.ElementAt(1);
            var secondParagraph = sut.ElementAt(2);
            var header4 = sut.ElementAt(3);
            var thirdParagraph = sut.ElementAt(4);

            firstParagraph.Children[0].Tag.Should().Be("_text");
            firstParagraph.Children[0].Value.Should().ContainEquivalentOf("This is the first paragraph. It contains a red span and a blue span.");


            secondParagraph.Children[0].Tag.Should().Be("_text");
            secondParagraph.Children[0].Value.Trim().Should().ContainEquivalentOf("This is the second paragraph. It contains an ordered list:");

            secondParagraph.Children[1].Tag.Should().Be("ol");

            var listOrdered = secondParagraph.Children.ElementAt(1);
            listOrdered.Children[0].Tag.Should().Be("li");
            listOrdered.Children[0].Children[0].Value.Should().Be("Item 1");

            listOrdered.Children[1].Tag.Should().Be("li");
            listOrdered.Children[1].Children[0].Value.Should().Be("Item 2");

            listOrdered.Children[2].Tag.Should().Be("li");
            listOrdered.Children[2].Children[0].Value.Should().Be("Item 3");

            secondParagraph.Children[2].Tag.Should().Be("_text");
            secondParagraph.Children[2].Value.Should().Contain("This is a text after the list in the second paragraph.");
            secondParagraph.Children[2].Value.Should().Contain("This is another part of a paragraph");
            

            listOrdered = secondParagraph.Children.ElementAt(3);
            listOrdered.Children[0].Tag.Should().Be("li");
            listOrdered.Children[0].Children[0].Value.Should().Be("Item 1");

            listOrdered.Children[1].Tag.Should().Be("li");
            listOrdered.Children[1].Children[0].Value.Should().Be("Item 2");

            listOrdered.Children[2].Tag.Should().Be("li");
            listOrdered.Children[2].Children[0].Value.Should().Be("Item 3");


            secondParagraph.Children[4].Tag.Should().Be("_text");
            secondParagraph.Children[4].Value.Should().ContainEquivalentOf("This is a text after the another list in the second paragraph.");

            header4.Tag.Should().Be("h4");
            header4.Children[0].Value.Should().Be("Another list");

            thirdParagraph.Children[0].Tag.Should().Be("_text");
            thirdParagraph.Children[0].Value.Should().ContainEquivalentOf("This is the third paragraph. It contains an unordered list:");

            thirdParagraph.Children[1].Tag.Should().Be("h4");
            thirdParagraph.Children[1].Children[0].Tag.Should().Be("_text");
            thirdParagraph.Children[1].Children[0].Value.Should().Be("a list");

            thirdParagraph.Children[2].Tag.Should().Be("ul");
            var listUnordered = thirdParagraph.Children.ElementAt(2);

            listUnordered.Children[0].Tag.Should().Be("li");
            listUnordered.Children[0].Children[0].Value.Should().Be("Item A");

            listUnordered.Children[1].Tag.Should().Be("li");
            listUnordered.Children[1].Children[0].Value.Should().Be("Item B");

            listUnordered.Children[2].Tag.Should().Be("li");
            listUnordered.Children[2].Children[0].Value.Should().Be("Item C");
        }
    }
}