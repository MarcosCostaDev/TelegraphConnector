using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TelegraphConnector.Types;

namespace TelegraphConnector.Test.Types
{
    public class PageTest
    {

        public static IEnumerable<object[]> Create_With_Account_Must_Have_All_Fields_MemberData()
        {
            var account = Account.Edit("token", "sandbox", "anounymous", "url");
            var content = new Node[] { new Node() };
            yield return new object[] { account, "title", content, true };
            yield return new object[] { account, "title2", content, false };

        }
        [Theory]
        [MemberData(nameof(Create_With_Account_Must_Have_All_Fields_MemberData))]
        public void Create_With_Account_Must_Have_All_Fields(Account account, string title, Node[] content, bool returnContent)
        {
            var sut = Page.Create(account, title, content, returnContent);

            sut.Title.Should().Be(title);
            sut.AuthorName.Should().Be(account.AuthorName);
            sut.AuthorUrl.Should().Be(account.AuthorUrl);
            sut.Content.Should().BeEquivalentTo(content);
            sut.ReturnContent.Should().Be(returnContent);
        }


        public static IEnumerable<object[]> Create_Must_Have_All_Fields_MemberData()
        {
            var content = new Node[] { new Node() };
            yield return new object[] { "title", "sandbox", "url", content, true };
            yield return new object[] { "title", "sandbox", "url", content, false };

        }
        [Theory]
        [MemberData(nameof(Create_Must_Have_All_Fields_MemberData))]
        public void Create_Must_Have_All_Fields(string title, string authorName, string authorUrl, Node[] content, bool returnContent = false)
        {
            var sut = Page.Create(title, authorName, authorUrl, content, returnContent);

            sut.Title.Should().Be(title);
            sut.AuthorName.Should().Be(authorName);
            sut.AuthorUrl.Should().Be(authorUrl);
            sut.Content.Should().BeEquivalentTo(content);
            sut.ReturnContent.Should().Be(returnContent);
        }


        public static IEnumerable<object[]> Edit_With_Account_Must_Have_All_Fields_MemberData()
        {
            var account = Account.Edit("token", "sandbox", "anounymous", "url");
            var content = new Node[] { new Node() };
            yield return new object[] { account, "path", "title", content, true };
            yield return new object[] { account, "path", "title2", content, false };

        }
        [Theory]
        [MemberData(nameof(Edit_With_Account_Must_Have_All_Fields_MemberData))]
        public void Edit_With_Account_Must_Have_All_Fields(Account account, string path, string title, Node[] content, bool returnContent)
        {
            var sut = Page.Edit(account, path, title, content, returnContent);

            sut.Title.Should().Be(title);
            sut.AuthorName.Should().Be(account.AuthorName);
            sut.AuthorUrl.Should().Be(account.AuthorUrl);
            sut.Content.Should().BeEquivalentTo(content);
            sut.ReturnContent.Should().Be(returnContent);
        }


        public static IEnumerable<object[]> Edit_Must_Have_All_Fields_MemberData()
        {
            var content = new Node[] { new Node() };
            yield return new object[] { "path", "title", "sandbox", "url", content, true };
            yield return new object[] { "path", "title", "sandbox", "url", content, false };

        }

        [Theory]
        [MemberData(nameof(Edit_Must_Have_All_Fields_MemberData))]
        public void Edit_Must_Have_All_Fields(string path, string title, string authorName, string authorUrl, Node[] content, bool returnContent = false)
        {
            var sut = Page.Edit(path, title, authorName, authorUrl, content, returnContent);

            sut.Title.Should().Be(title);
            sut.AuthorName.Should().Be(authorName);
            sut.AuthorUrl.Should().Be(authorUrl);
            sut.Content.Should().BeEquivalentTo(content);
            sut.ReturnContent.Should().Be(returnContent);
        }

    }
}
