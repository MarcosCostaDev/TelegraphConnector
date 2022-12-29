using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegraphConnector.Types;

namespace TelegraphConnector.Test.Types
{
    public class AccountTest
    {

        [Theory]
        [InlineData("sandbox", "Anonymous", "url")]
        public void Create_Must_Have_All_Fields(string shortName, string authorName, string authorUrl)
        {
            var sut = Account.Create(shortName, authorName, authorUrl); 

            sut.ShortName.Should().Be(shortName);
            sut.AuthorName.Should().Be(authorName);
            sut.AuthorUrl.Should().Be(authorUrl);
        }

        [Theory]
        [InlineData("access_token_example", "sandbox", "Anonymous", "url")]
        public void Edit_Must_Have_All_Fields(string accessToken, string shortName, string authorName, string authorUrl)
        {
            var sut = Account.Edit(accessToken, shortName, authorName, authorUrl);

            sut.AccessToken.Should().Be(accessToken);
            sut.ShortName.Should().Be(shortName);
            sut.AuthorName.Should().Be(authorName);
            sut.AuthorUrl.Should().Be(authorUrl);
        }

        [Theory]
        [InlineData("access_token_example")]
        public void Info_Must_Have_All_Fields(string accessToken)
        {
            var sut = Account.Info(accessToken);

            sut.AccessToken.Should().Be(accessToken);
        }

        [Theory]
        [InlineData("access_token_example")]
        public void RevokeToken_Must_Have_All_Fields(string accessToken)
        {
            var sut = Account.RevokeToken(accessToken);

            sut.AccessToken.Should().Be(accessToken);
        }
    }
}
