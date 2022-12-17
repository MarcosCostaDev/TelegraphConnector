using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegraphConnector.Parses.Test
{
    public class TelegraphMarkdownTest : AbstractTest
    {

        [Fact]
        public void HtmlParse_example_1_with_body_tag()
        {
            var mdContent = GetTextFromFile("example_1.md");

            var sut = TelegraphMarkDown.Parse(mdContent);

            sut.Should().HaveCount(1);

            sut.ElementAt(0).Tag.Should().Be("p");
            sut.ElementAt(0).Children.Should().HaveCount(5);
        }
    }
}
