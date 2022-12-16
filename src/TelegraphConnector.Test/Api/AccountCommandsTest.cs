using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using System.Reflection;
using System.Text;
using TelegraphConnector.Api;
using TelegraphConnector.Types;

namespace TelegraphConnector.Test.Api
{
    public class AccountCommandsTest
    {
        public static string GetTextFromFile(string relativePath)
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            return File.ReadAllText(Path.Combine(dirPath, "Api", "MockResponses", relativePath));
        }

        public static IEnumerable<object[]> CreateAccountAsync_MemberData()
        {
            yield return new object[] { Account.Create("sandbox", "anounymous", "url") };

        }

        [Theory]
        [MemberData(nameof(CreateAccountAsync_MemberData))]
        public async void CreateAccountAsync(Account account)
        {
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(GetTextFromFile("account_create.json"), Encoding.UTF8, "application/json"),
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            var moqClient = new Mock<ITelegraphClient>();
            moqClient.Setup(m => m.GetHttpClient()).Returns(httpClient);

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
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(GetTextFromFile("account_edit.json"), Encoding.UTF8, "application/json"),
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            var moqClient = new Mock<ITelegraphClient>();
            moqClient.Setup(m => m.GetHttpClient()).Returns(httpClient);

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
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(GetTextFromFile("account_info.json"), Encoding.UTF8, "application/json"),
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            var moqClient = new Mock<ITelegraphClient>();
            moqClient.Setup(m => m.GetHttpClient()).Returns(httpClient);

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
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(GetTextFromFile("account_revoke.json"), Encoding.UTF8, "application/json"),
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            var moqClient = new Mock<ITelegraphClient>();
            moqClient.Setup(m => m.GetHttpClient()).Returns(httpClient);

            var accountCommands = new AccountCommands(moqClient.Object);

            // SUT 
            var sut = await accountCommands.RevokeAccessAsync(account);

            // Assert
            sut.Ok.Should().BeFalse();
            sut.Error.Should().Be("SANDBOX_TOKEN_REVOKE_DENIED");
        }
    }
}
