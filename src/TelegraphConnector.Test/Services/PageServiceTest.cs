using FluentAssertions;
using Moq;
using TelegraphConnector.Services;
using TelegraphConnector.Types;

namespace TelegraphConnector.Test.Services
{
    public class PageServiceTest : AbstractServiceTest
    {
        public static IEnumerable<object[]> CreatePageAsync_MemberData()
        {
            var account = Account.Edit("token", "sandbox", "anounymous", "url");
            var content = new Node[] { new Node() };
            var page = Page.Create(account, "title", content, false);

            yield return new object[] { account.AccessToken, page };

        }

        [Theory]
        [MemberData(nameof(CreatePageAsync_MemberData))]
        public async void CreatePageAsync(string accessToken, Page page)
        {
            
            Mock<ITelegraphClient> moqClient = CreateMockClient("page_create.json");

            var pageCommands = new PageService(moqClient.Object);

            var sut = await pageCommands.CreatePageAsync(accessToken, page);

            // Assert
            sut.Ok.Should().BeTrue();
            sut.Result.Path.Should().Be("Sample-Page-12-16-67");
            sut.Result.Url.Should().Be("https://telegra.ph/Sample-Page-12-16-67");
            sut.Result.Title.Should().Be("Sample Page");
            sut.Result.Description.Should().BeEmpty();
            sut.Result.AuthorName.Should().Be("Anonymous");
            sut.Result.Views.Should().Be(0);
            sut.Result.CanEdit.Should().BeTrue();
            sut.Result.Content[0].Tag.Should().Be("p");
            sut.Result.Content[0].Children[0].Tag.Should().Be("_text");
            sut.Result.Content[0].Children[0].Value.Should().Be("Hello, world!");

        }

        public static IEnumerable<object[]> EditPageAsync_MemberData()
        {
            var account = Account.Edit("token", "sandbox", "anounymous", "url");
            var content = new Node[] { new Node() };
            var page = Page.Create(account, "title", content, false);

            yield return new object[] { account.AccessToken, page };

        }

        [Theory]
        [MemberData(nameof(EditPageAsync_MemberData))]
        public async void EditPageAsync(string accessToken, Page page)
        {
            
            Mock<ITelegraphClient> moqClient = CreateMockClient("page_edit.json");

            var pageCommands = new PageService(moqClient.Object);

            var sut = await pageCommands.EditPageAsync(accessToken, page);

            // Assert
            sut.Ok.Should().BeTrue();
            sut.Result.Path.Should().Be("Sample-Page-12-15");
            sut.Result.Url.Should().Be("https://telegra.ph/Sample-Page-12-15");
            sut.Result.Title.Should().Be("Sample Page");
            sut.Result.Description.Should().Be("Hello, world!");
            sut.Result.AuthorName.Should().Be("Anonymous");
            sut.Result.Views.Should().Be(2997);
            sut.Result.CanEdit.Should().BeTrue();
            sut.Result.Content[0].Tag.Should().Be("p");
            sut.Result.Content[0].Children[0].Tag.Should().Be("_text");
            sut.Result.Content[0].Children[0].Value.Should().Be("Hello, world!");

        }

        public static IEnumerable<object[]> GetPageAsync_MemberData()
        {
            yield return new object[] { "Sample-Page-12-15" };

        }

        [Theory]
        [MemberData(nameof(GetPageAsync_MemberData))]
        public async void GetPageAsync(string path)
        {

            Mock<ITelegraphClient> moqClient = CreateMockClient("page_get.json");

            var pageCommands = new PageService(moqClient.Object);

            var sut = await pageCommands.GetPageAsync(path);

            // Assert
            sut.Ok.Should().BeTrue();
            sut.Result.Path.Should().Be("Sample-Page-12-15");
            sut.Result.Url.Should().Be("https://telegra.ph/Sample-Page-12-15");
            sut.Result.Title.Should().Be("Sample Page");
            sut.Result.Description.Should().Be("Hello, world!");
            sut.Result.AuthorName.Should().Be("Anonymous");
            sut.Result.Views.Should().Be(2997);
            sut.Result.CanEdit.Should().BeFalse();
            sut.Result.Content[0].Tag.Should().Be("p");
            sut.Result.Content[0].Children[0].Tag.Should().Be("_text");
            sut.Result.Content[0].Children[0].Value.Should().Be("Hello, world!");

        }

        public static IEnumerable<object[]> GetPageListAsync_MemberData()
        {
            var account = Account.Edit("token", "sandbox", "anounymous", "url");
            var content = new Node[] { new Node() };
            var page = Page.Create(account, "title", content, false);

            yield return new object[] { account.AccessToken, page };

        }

        [Theory]
        [InlineData("token", 0, 50)]
        public async void GetPageListAsync(string accessToken, int offset, int limit)
        {
            // ARRANGE
            Mock<ITelegraphClient> moqClient = CreateMockClient("page_get_list.json");

            var pageCommands = new PageService(moqClient.Object);

            var sut = await pageCommands.GetPageListAsync(accessToken, offset, limit);

            // Assert
            sut.Ok.Should().BeTrue();
            sut.Result.TotalCount.Should().Be(194104);
            sut.Result.Pages.Count().Should().Be(3);
            sut.Result.Pages.ElementAt(0).Path.Should().Be("Otchet-po-zaprosu-M143VM197-12-16");
            sut.Result.Pages.ElementAt(0).Url.Should().Be("https://telegra.ph/Otchet-po-zaprosu-M143VM197-12-16");
            sut.Result.Pages.ElementAt(0).Title.Should().Be("Отчет по запросу: М143ВМ197");
            sut.Result.Pages.ElementAt(0).Description.Should().Contain("ИМЯ - СЕМЕНЧЕНКО ЕВГЕНИЙ");
            sut.Result.Pages.ElementAt(0).AuthorName.Should().Be("Anonymous");
            sut.Result.Pages.ElementAt(0).Views.Should().Be(1);
            sut.Result.Pages.ElementAt(0).CanEdit.Should().BeTrue();

            sut.Result.Pages.ElementAt(1).Path.Should().Be("Otchet-po-zaprosu-524908163902-12-16");
            sut.Result.Pages.ElementAt(1).Url.Should().Be("https://telegra.ph/Otchet-po-zaprosu-524908163902-12-16");
            sut.Result.Pages.ElementAt(1).Title.Should().Be("Отчет по запросу: 524908163902");
            sut.Result.Pages.ElementAt(1).Description.Should().Contain("ИВАН БОРИСОВИЧ ДАТА РОЖДЕНИЯ");
            sut.Result.Pages.ElementAt(1).AuthorName.Should().Be("Anonymous");
            sut.Result.Pages.ElementAt(1).Views.Should().Be(1);
            sut.Result.Pages.ElementAt(1).CanEdit.Should().BeTrue();

            sut.Result.Pages.ElementAt(2).Path.Should().Be("Otchet-po-zaprosu-502983017164-12-16");
            sut.Result.Pages.ElementAt(2).Url.Should().Be("https://telegra.ph/Otchet-po-zaprosu-502983017164-12-16");
            sut.Result.Pages.ElementAt(2).Title.Should().Be("Отчет по запросу: 502983017164");
            sut.Result.Pages.ElementAt(2).Description.Should().Contain("выдачи паспорта - 11.12.2020");
            sut.Result.Pages.ElementAt(2).AuthorName.Should().Be("Anonymous");
            sut.Result.Pages.ElementAt(2).Views.Should().Be(0);
            sut.Result.Pages.ElementAt(2).CanEdit.Should().BeTrue();
        }

       

        [Theory]
        [InlineData("token", "path", 2022, 12, 01, 12)]
        public async void GetViewsAsync(string accessToken, string path, int year, int month, int day, int hour)
        {

            Mock<ITelegraphClient> moqClient = CreateMockClient("page_get_views.json");

            var pageCommands = new PageService(moqClient.Object);

            var sut = await pageCommands.GetViewsAsync(accessToken, path, year, month, day, hour);

            // Assert
            sut.Ok.Should().BeTrue();
            sut.Result.Views.Should().Be(40);

        }


        public static IEnumerable<object[]> GetViewsWithDateTimeAsync_MemberData()
        {

            yield return new object[] { "token", "path", DateTime.Now };

        }
        [Theory]
        [MemberData(nameof(GetViewsWithDateTimeAsync_MemberData))]
        public async void GetViewsWithDateTimeAsync(string accessToken, string path, DateTime date)
        {
            Mock<ITelegraphClient> moqClient = CreateMockClient("page_get_views.json");

            var pageCommands = new PageService(moqClient.Object);

            var sut = await pageCommands.GetViewsAsync(accessToken, path, date);

            // Assert
            sut.Ok.Should().BeTrue();
            sut.Result.Views.Should().Be(40);

        }
    }
}