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
        public void MdParse_example_1_with_body_tag()
        {
            var mdContent = GetTextFromFile("example_1.md");

            var sut = TelegraphMarkdown.Parse(mdContent);

            sut.Should().HaveCount(1);

            sut.ElementAt(0).Tag.Should().Be("p");
            sut.ElementAt(0).Children.Should().HaveCount(5);
        }

        [Fact]
        public void MdParse_example_2_with_body_tag()
        {
            var mdContent = GetTextFromFile("example_2.md");

            var sut = TelegraphMarkdown.Parse(mdContent);

            sut.Should().HaveCount(8);

            sut.ElementAt(0).Tag.Should().Be("h1");
        }

        [Fact]
        public void MdParse_example_3_with_body_tag()
        {
            var mdContent = GetTextFromFile("example_3.md");

            var sut = TelegraphMarkdown.Parse(mdContent);

            sut.Should().HaveCount(12);

            sut.ElementAt(0).Tag.Should().Be("h1");
        }
    }
}
