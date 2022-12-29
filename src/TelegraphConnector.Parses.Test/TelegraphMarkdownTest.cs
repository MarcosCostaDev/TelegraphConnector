using FluentAssertions;

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

            var paragraph = sut.ElementAt(0);

            paragraph.Children[0].Tag.Should().Be("_text");
            paragraph.Children[0].Value.Should().ContainEquivalentOf("This is a paragraph with two words in");
            
            paragraph.Children[1].Tag.Should().Be("strong");
            paragraph.Children[1].Children[0].Tag.Should().Be("_text");
            paragraph.Children[1].Children[0].Value.Should().Be("bold");

            paragraph.Children[2].Tag.Should().Be("_text");
            paragraph.Children[2].Value.Should().ContainEquivalentOf(". The first word is");


            paragraph.Children[3].Tag.Should().Be("strong");
            paragraph.Children[3].Children[0].Tag.Should().Be("_text");
            paragraph.Children[3].Children[0].Value.Should().Be("important");

            paragraph.Children[4].Tag.Should().Be("_text");
            paragraph.Children[4].Value.Should().ContainEquivalentOf("and the second word is");

            paragraph.Children[5].Tag.Should().Be("strong");
            paragraph.Children[5].Children[0].Tag.Should().Be("_text");
            paragraph.Children[5].Children[0].Value.Should().Be("emphasized");

            paragraph.Children[6].Tag.Should().Be("_text");
            paragraph.Children[6].Value.Should().ContainEquivalentOf(".");
        }

        [Fact]
        public void MdParse_example_2_with_body_tag()
        {
            var mdContent = GetTextFromFile("example_2.md");

            var sut = TelegraphMarkdown.Parse(mdContent);

            sut.ElementAt(0).Tag.Should().Be("h3");
            sut.ElementAt(0).Children[0].Tag.Should().Be("_text");
            sut.ElementAt(0).Children[0].Value.Should().ContainEquivalentOf("Title");

            var firstParagraph = sut.ElementAt(1);
            var secondParagraph = sut.ElementAt(2);
            var thirdParagraph = sut.ElementAt(3);


            firstParagraph.Children[0].Tag.Should().Be("_text");
            firstParagraph.Children[0].Value.Should().ContainEquivalentOf("This is the first paragraph.");

            firstParagraph.Children[1].Tag.Should().Be("strong");
            firstParagraph.Children[1].Children[0].Tag.Should().Be("_text");
            firstParagraph.Children[1].Children[0].Value.Should().ContainEquivalentOf("Text in bold");


            firstParagraph.Children[2].Tag.Should().Be("_text");
            firstParagraph.Children[2].Value.Should().ContainEquivalentOf("can be created by enclosing the text in double asterisks.");


            secondParagraph.Children[0].Tag.Should().Be("_text");
            secondParagraph.Children[0].Value.Should().ContainEquivalentOf("This is the second paragraph.");

            secondParagraph.Children[1].Tag.Should().Be("strong");
            secondParagraph.Children[1].Children[0].Tag.Should().Be("_text");
            secondParagraph.Children[1].Children[0].Value.Should().ContainEquivalentOf("Another bold text");


            secondParagraph.Children[2].Tag.Should().Be("_text");
            secondParagraph.Children[2].Value.Should().ContainEquivalentOf("can be added by using the same syntax.");

            thirdParagraph.Children[0].Tag.Should().Be("_text");
            thirdParagraph.Children[0].Value.Should().ContainEquivalentOf("This is the third paragraph. You can also create lists in markdown.");


            sut.ElementAt(4).Tag.Should().Be("h4");
            sut.ElementAt(4).Children[0].Tag.Should().Be("_text");
            sut.ElementAt(4).Children[0].Value.Should().ContainEquivalentOf("Ordered List");

            sut.ElementAt(5).Tag.Should().Be("ol");

            var listOrdered = sut.ElementAt(5);
            listOrdered.Children[0].Tag.Should().Be("li");
            listOrdered.Children[0].Children[0].Value.Should().Be("Item 1");

            listOrdered.Children[1].Tag.Should().Be("li");
            listOrdered.Children[1].Children[0].Value.Should().Be("Item 2");

            listOrdered.Children[2].Tag.Should().Be("li");
            listOrdered.Children[2].Children[0].Value.Should().Be("Item 3");


            sut.ElementAt(6).Tag.Should().Be("h4");
            sut.ElementAt(6).Children[0].Tag.Should().Be("_text");
            sut.ElementAt(6).Children[0].Value.Should().ContainEquivalentOf("Unordered List");

            sut.ElementAt(7).Tag.Should().Be("ul");

            var listUnordered = sut.ElementAt(7);
            listUnordered.Children[0].Tag.Should().Be("li");
            listUnordered.Children[0].Children[0].Value.Should().Be("Item 1");

            listUnordered.Children[1].Tag.Should().Be("li");
            listUnordered.Children[1].Children[0].Value.Should().Be("Item 2");

            listUnordered.Children[2].Tag.Should().Be("li");
            listUnordered.Children[2].Children[0].Value.Should().Be("Item 3");

        }

        [Fact]
        public void MdParse_example_3_with_body_tag()
        {
            var mdContent = GetTextFromFile("example_3.md");

            var sut = TelegraphMarkdown.Parse(mdContent);

            sut.Should().HaveCount(4);

            sut.ElementAt(0).Tag.Should().Be("h3");
            sut.ElementAt(0).Children[0].Tag.Should().Be("_text");
            sut.ElementAt(0).Children[0].Value.Should().Be("Title");

            var firstParagraph = sut.ElementAt(1);
            var secondParagraph = sut.ElementAt(2);
            var thirdParagraph = sut.ElementAt(3);

            firstParagraph.Children[0].Tag.Should().Be("img");
            firstParagraph.Children[0].Attributes.Should().Contain("src", "image1.jpg");
            firstParagraph.Children[0].Attributes.Should().Contain("alt", "Image Alt Text");

            secondParagraph.Children[0].Tag.Should().Be("_text");
            secondParagraph.Children[0].Value.Should().ContainEquivalentOf("You can also add a link to an image by using the same syntax, but with the image source in parentheses:");

            thirdParagraph.Children[0].Tag.Should().Be("a");
            thirdParagraph.Children[0].Attributes.Should().Contain("href", "https://example.com");
            thirdParagraph.Children[0].Children[0].Tag.Should().Be("img");
            thirdParagraph.Children[0].Children[0].Attributes.Should().Contain("src", "image2.jpg");
            thirdParagraph.Children[0].Children[0].Attributes.Should().Contain("alt", "Linked Image Alt Text");

        }
    }
}
