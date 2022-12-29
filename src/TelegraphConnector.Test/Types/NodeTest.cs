using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegraphConnector.Exceptions;
using TelegraphConnector.Types;

namespace TelegraphConnector.Test.Types
{
    public class NodeTest
    {
        [Fact]
        public void CreateTextNode_result_nodes()
        {
            var sut = Node.CreateTextNode("node-text");

            sut.Value.Should().Be("node-text");
            sut.Tag.Should().Be("_text");

        }

        [Fact]
        public void CreateNode_P_result_p_nodes()
        {
            var sut = Node.CreateNode("p", null);

            sut.Children.Should().BeEmpty();
            sut.Tag.Should().Be("p");
        }

        [Fact]
        public void AddAttribute_should_have_one()
        {
            var sut = Node.CreateNode("p", null);

            sut.AddAttributes(new KeyValuePair<string, string>("class", "one-class"));

            sut.Tag.Should().Be("p");
            sut.Attributes.Should().HaveCount(1);
            sut.Attributes["class"].Should().Be("one-class");
        }

        [Fact]
        public void AddAttribute_should_have_three()
        {
            var sut = Node.CreateNode("p", null);

            sut.AddAttributes(new KeyValuePair<string, string>[] {
             new KeyValuePair<string, string>("class", "one-class"),
             new KeyValuePair<string, string>("style", "two-style"),
             new KeyValuePair<string, string>("data", "three-style"),
            });

            sut.Tag.Should().Be("p");
            sut.Attributes.Should().HaveCount(3);
            sut.Attributes["class"].Should().Be("one-class");
            sut.Attributes["style"].Should().Be("two-style");
            sut.Attributes["data"].Should().Be("three-style");
        }

        [Theory]
        [InlineData(null, "http://test.com/website?webtest")]
        public void CreateAnchor_text_not_informed_ArgumentNullException(string text, string link)
        {

            Assert.Throws<ArgumentNullException>(() => Node.CreateAnchor(text, link));
        }

        [Theory]
        [InlineData("", "http://test.com/website?webtest")]
        public void CreateAnchor_text_not_informed_ArgumentException(string text, string link)
        {

            Assert.Throws<ArgumentException>(() => Node.CreateAnchor(text, link));
        }

        [Theory]
        [InlineData("a text", "")]
        public void CreateAnchor_link_not_informed_ArgumentException(string text, string link)
        {

            Assert.Throws<ArgumentException>(() => Node.CreateAnchor(text, link));
        }

        [Theory]
        [InlineData("a text", null)]
        public void CreateAnchor_link_not_informed_ArgumentNullException(string text, string link)
        {

            Assert.Throws<ArgumentNullException>(() => Node.CreateAnchor(text, link));
        }

        [Theory]
        [InlineData("a text", "http://www.example.com/path/to/resource")]
        [InlineData("text", "example.com/path/to/resource")]
        public void CreateAnchor_link_well_formatted_exception(string text, string link)
        {

            var sut = Node.CreateAnchor(text, link);

            sut.Tag.Should().Be("a");
            sut.Attributes.Should().HaveCount(2);
            sut.Attributes["href"].Should().Be(link);
            sut.Attributes["target"].Should().Be("_blank");
            sut.Children.Should().HaveCount(1);
            sut.Children[0].Value.Should().Be(text);
        }

        [Fact]
        public void CreateParagraph_result_nodes()
        {
            var textNode = Node.CreateTextNode("test");

            var sut = Node.CreateParagraph(textNode);

         
            sut.Tag.Should().Be("p");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Value.Should().Be("test");
        }

        [Fact]
        public void CreateAside_result_nodes()
        {
            var textNode = Node.CreateTextNode("test");

            var sut = Node.CreateAside(textNode);


            sut.Tag.Should().Be("aside");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Value.Should().Be("test");
        }



        [Fact]
        public void CreateBreakLine_result_nodes()
        {
            var sut = Node.CreateBreakLine();


            sut.Tag.Should().Be("br");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().BeEmpty();
        }

        [Fact]
        public void CreateHorizontalRule_result_nodes()
        {
            var sut = Node.CreateHorizontalRule();


            sut.Tag.Should().Be("hr");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().BeEmpty();
        }

        [Fact]
        public void CreateListItem_result_nodes()
        {
            var textNode = Node.CreateTextNode("test");

            var sut = Node.CreateListItem(textNode);


            sut.Tag.Should().Be("li");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Value.Should().Be("test");
        }

        [Fact]
        public void CreateListItem_with_text_result_nodes()
        {
            var sut = Node.CreateListItem("test");


            sut.Tag.Should().Be("li");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Value.Should().Be("test");
        }
              

        [Fact]
        public void CreateHeader3_with_text_result_nodes()
        {
            var sut = Node.CreateHeader3("test");


            sut.Tag.Should().Be("h3");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Value.Should().Be("test");
        }

        [Fact]
        public void CreateHeader4_with_text_result_nodes()
        {
            var sut = Node.CreateHeader4("test");


            sut.Tag.Should().Be("h4");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Value.Should().Be("test");
        }

        [Fact]
        public void CreateEmphasis_with_text_result_nodes()
        {
            var sut = Node.CreateEmphasis("test");


            sut.Tag.Should().Be("em");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Value.Should().Be("test");
        }

        [Fact]
        public void CreateStrong_with_text_result_nodes()
        {
            var sut = Node.CreateStrong("test");


            sut.Tag.Should().Be("strong");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Value.Should().Be("test");
        }

        [Fact]
        public void CreateItalic_result_nodes()
        {
            var textNode = Node.CreateTextNode("test");

            var sut = Node.CreateItalic(textNode);


            sut.Tag.Should().Be("i");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Value.Should().Be("test");
        }

        [Fact]
        public void CreateOrderedList_result_nodes()
        {
            var listItem = Node.CreateListItem("test");

            var sut = Node.CreateOrderedList(listItem);


            sut.Tag.Should().Be("ol");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("li");
            sut.Children.ElementAt(0).Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Children.ElementAt(0).Value.Should().Be("test");
        }


        [Fact]
        public void CreateUnorderedList_result_nodes()
        {
            var listItem = Node.CreateListItem("test");

            var sut = Node.CreateUnorderedList(listItem);


            sut.Tag.Should().Be("ul");
            sut.Attributes.Should().BeEmpty();
            sut.Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Tag.Should().Be("li");
            sut.Children.ElementAt(0).Children.Should().HaveCount(1);
            sut.Children.ElementAt(0).Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Children.ElementAt(0).Value.Should().Be("test");
        }

        [Fact]
        public void AddChildren_result_nodes()
        {
            var breakLineNode = Node.CreateBreakLine();

            var sut = Node.CreateParagraph(Node.CreateTextNode("test"));
            sut.AddChildren(breakLineNode);


            sut.Tag.Should().Be("p");
            sut.Children.Should().HaveCount(2);
            sut.Children.ElementAt(0).Tag.Should().Be("_text");
            sut.Children.ElementAt(0).Value.Should().Be("test");
            sut.Children.ElementAt(1).Tag.Should().Be("br");
        }


        [Fact]
        public void CreateOrderedList_result_exception()
        {
            var textNode = Node.CreateTextNode("test");

            Assert.Throws<TelegraphConnectorException>(() => Node.CreateOrderedList(textNode));
        }



        [Fact]
        public void CreateUnorderedList_result_exception()
        {
            var textNode = Node.CreateTextNode("test");

            Assert.Throws<TelegraphConnectorException>(() => Node.CreateUnorderedList(textNode));
        }

    }
}
