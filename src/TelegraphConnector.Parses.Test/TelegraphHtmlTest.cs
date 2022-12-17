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

            sut.Tag.Should().Be("div");
            sut.Children.ElementAt(0).Tag.Should().Be("p");
        }
    }
}