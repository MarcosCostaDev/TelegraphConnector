using FluentAssertions;
using TelegraphConnector.Types;

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
            firstParagraph.Children[0].Value.Should().Be("This is the first paragraph. It contains a red span and a blue span.");

            secondParagraph.Children[1].Tag.Should().Be("ol");

            var listOrdered = secondParagraph.Children.ElementAt(1);
            listOrdered.Children[0].Tag.Should().Be("li");
            listOrdered.Children[0].Children[0].Value.Should().Be("Item 1");

            listOrdered.Children[1].Tag.Should().Be("li");
            listOrdered.Children[1].Children[0].Value.Should().Be("Item 2");

            listOrdered.Children[2].Tag.Should().Be("li");
            listOrdered.Children[2].Children[0].Value.Should().Be("Item 3");


            thirdParagraph.Children[0].Tag.Should().Be("p");
            thirdParagraph.Children[0].Children[0].Tag.Should().Be("_text");
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

            //sut.Tag.Should().Be("div");
        }

        [Fact]
        public void HtmlParse_example_4_without_body_tag_3_paragraphs()
        {
            var htmlContent = GetTextFromFile("example_4.html");

            var sut = TelegraphHtml.Parse(htmlContent);

            //sut.Tag.Should().Be("div");
        }
    }
}