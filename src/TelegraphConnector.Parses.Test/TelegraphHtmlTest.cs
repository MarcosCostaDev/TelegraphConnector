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
            sut.ElementAt(0).Children.Should().HaveCount(3);
            sut.ElementAt(0).Children.ElementAt(0).Tag.Should().Be("_text");
            sut.ElementAt(0).Children.ElementAt(1).Tag.Should().Be("span");
            sut.ElementAt(0).Children.ElementAt(2).Tag.Should().Be("_text");
        }


        [Fact]
        public void HtmlParse_example_2_with_body_tag_3_paragraphs()
        {
            var htmlContent = GetTextFromFile("example_2.html");

            var sut = TelegraphHtml.Parse(htmlContent);

            sut.Should().HaveCount(3);
            sut.All(p => p.Tag == "p").Should().BeTrue();

            var firstParagraph = sut.ElementAt(0);
            var secondParagraph = sut.ElementAt(1);
            var thirdParagraph = sut.ElementAt(2);

            firstParagraph.Children.ElementAt(0).Tag.Should().Be("_text");
            firstParagraph.Children.ElementAt(1).Tag.Should().Be("span");
            firstParagraph.Children.ElementAt(2).Tag.Should().Be("span");

            secondParagraph.Children.ElementAt(0).Tag.Should().Be("_text");
            secondParagraph.Children.ElementAt(1).Tag.Should().Be("ol");

            var listOrdered = secondParagraph.Children.ElementAt(1);
            listOrdered.Children.ElementAt(0).Tag.Should().Be("_text");
            listOrdered.Children.ElementAt(1).Tag.Should().Be("li");
            listOrdered.Children.ElementAt(2).Tag.Should().Be("li");
            listOrdered.Children.ElementAt(3).Tag.Should().Be("li");


            thirdParagraph.Children.ElementAt(0).Tag.Should().Be("_text");
            thirdParagraph.Children.ElementAt(1).Tag.Should().Be("ul");

            var listUnordered = secondParagraph.Children.ElementAt(1);
            listOrdered.Children.ElementAt(0).Tag.Should().Be("_text");
            listOrdered.Children.ElementAt(1).Tag.Should().Be("li");
            listOrdered.Children.ElementAt(2).Tag.Should().Be("li");
            listOrdered.Children.ElementAt(3).Tag.Should().Be("li");
        }


        [Fact]
        public void HtmlParse_example_3_without_body_tag_3_paragraphs()
        {
            var htmlContent = GetTextFromFile("example_3.html");

            var sut = TelegraphHtml.Parse(htmlContent);

            //sut.Tag.Should().Be("div");
        }
    }
}