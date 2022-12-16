using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using TelegraphConnector.Api;
using TelegraphConnector.Types;

namespace TelegraphConnector.Test.Api
{
    public class AccountCommandsTest : AbstractCommandTest
    {
      

        public static IEnumerable<object[]> CreateAccountAsync_MemberData()
        {
            yield return new object[] { Account.Create("sandbox", "anounymous", "url") };

        }

        [Theory]
        [MemberData(nameof(CreateAccountAsync_MemberData))]
        public async void CreateAccountAsync(Account account)
        {
            
            Mock<ITelegraphClient> moqClient = CreateMockClient("account_create.json");

            var accountCommands = new AccountCommands(moqClient.Object);

            var sut = await accountCommands.CreateAccountAsync(account);

            // Assert
            sut.Ok.Should().BeTrue();
            sut.Result.ShortName.Should().Be("Sandbox");
            sut.Result.AuthorName.Should().Be("Anonymous");
            sut.Result.AuthorUrl.Should().BeEmpty();
            sut.Result.AccessToken.Should().Be("29484ea254754781378d5647b14e8e0fae37b177319d97540e2c06d4a785");
            sut.Result.AuthUrl.Should().Be("https://edit.telegra.ph/auth/b2hR22oVuKOjmmMi4flDgGBFQYywfJkoJBfLBBF6lr");
        }


        public static IEnumerable<object[]> EditAccountAsync_MemberData()
        {
            yield return new object[] { Account.Edit("token", "sandbox", "anounymous", "url") };

        }


        [Theory]
        [MemberData(nameof(EditAccountAsync_MemberData))]
        public async void EditAccountAsync(Account account)
        {
           
            Mock<ITelegraphClient> moqClient = CreateMockClient("account_edit.json");

            var accountCommands = new AccountCommands(moqClient.Object);

            // SUT 
            var sut = await accountCommands.EditAccountInfoAsync(account);

            // Assert
            sut.Ok.Should().BeTrue();
            sut.Result.ShortName.Should().Be("Sandbox");
            sut.Result.AuthorName.Should().Be("Anonymous");
            sut.Result.AuthorUrl.Should().Be("https://example.com/");
        }


        public static IEnumerable<object[]> GetAccountInfoAsync_MemberData()
        {
            yield return new object[] { Account.Info("token") };

        }


        [Theory]
        [MemberData(nameof(GetAccountInfoAsync_MemberData))]
        public async void GetAccountInfoAsync(Account account)
        {
            Mock<ITelegraphClient> moqClient = CreateMockClient("account_info.json");

            var accountCommands = new AccountCommands(moqClient.Object);

            // SUT 
            var sut = await accountCommands.GetAccountInfoAsync(account);

            // Assert
            sut.Ok.Should().BeTrue();
            sut.Result.ShortName.Should().Be("Sandbox");
            sut.Result.PageCount.Should().Be(194067);
        }

        public static IEnumerable<object[]> RevokeAccessAsync_MemberData()
        {
            yield return new object[] { Account.Info("token") };

        }


        [Theory]
        [MemberData(nameof(RevokeAccessAsync_MemberData))]
        public async void RevokeAccessAsync(Account account)
        {
            Mock<ITelegraphClient> moqClient = CreateMockClient("account_revoke.json");

            var accountCommands = new AccountCommands(moqClient.Object);

            // SUT 
            var sut = await accountCommands.RevokeAccessAsync(account);

            // Assert
            sut.Ok.Should().BeFalse();
            sut.Error.Should().Be("SANDBOX_TOKEN_REVOKE_DENIED");
        }
    }
}
