using Newtonsoft.Json;
using System.Reflection;
using TelegraphConnector.Parses;
using TelegraphConnector.Services;
using TelegraphConnector.Types;

namespace ExampleTelegraphConsole
{
    internal class PageData
    {
        private static string GetTextFromFile(string relativePath)
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            return File.ReadAllText(Path.Combine(dirPath, "AppData", "ExampleFiles", relativePath));
        }

        internal static async Task<Page> CreateSinglePageAsync(Account account)
        {
            var title = Faker.Lorem.GetFirstWord();
            var nodes = new Node[] {
                Node.CreateHeader3(title),
                Node.CreateParagraph(Node.CreateTextNode(Faker.Lorem.Paragraph())),
                Node.CreateHeader4(Faker.Lorem.Sentence()),
                Node.CreateParagraph(Node.CreateTextNode(Faker.Lorem.Paragraph()))
           };

            var page = Page.Create(account, Faker.Lorem.Sentence(), nodes, true);

            var pageService = new PageService();

            var createdPage = await pageService.CreatePageAsync(account.AccessToken, page);

            var jResult = JsonConvert.SerializeObject(createdPage.Result, Formatting.Indented);
            AppDataFiles.CreateFile($"{title}.json", jResult);

            return createdPage.Result;
        }

        internal static async Task<Page> EditSinglePageAsync(Account account, Page pageEditable)
        {
            var title = Faker.Lorem.Sentence();
            var nodes = new Node[] {
                Node.CreateHeader3(title),
                
                Node.CreateParagraph(Node.CreateTextNode(Faker.Lorem.Paragraph())),
                Node.CreateParagraph(Node.CreateTextNode(Faker.Lorem.Paragraph())),
                Node.CreateParagraph(Node.CreateTextNode(Faker.Lorem.Paragraph())),
                
                Node.CreateHeader4(Faker.Lorem.Sentence()),
                
                Node.CreateParagraph(Node.CreateTextNode(Faker.Lorem.Paragraph())),
                Node.CreateParagraph(Node.CreateTextNode(Faker.Lorem.Paragraph()))

           };

            var page = Page.Edit(account, pageEditable.Path, Faker.Lorem.Sentence(), nodes, true);

            var pageService = new PageService();

            var createdPage = await pageService.EditPageAsync(account.AccessToken, page);

            var jResult = JsonConvert.SerializeObject(createdPage.Result, Formatting.Indented);
            AppDataFiles.CreateFile($"{title}.json", jResult);

            return createdPage.Result;
        }

        internal static async Task<Page> CreatePageFromHtmlAsync(Account account)
        {
            var title = Faker.Lorem.GetFirstWord();
            var fileText = GetTextFromFile("example_2.html");
            var nodes = TelegraphHtml.Parse(fileText).ToArray();

            var page = Page.Create(account, Faker.Lorem.Sentence(), nodes, true);

            var pageService = new PageService();

            var createdPage = await pageService.CreatePageAsync(account.AccessToken, page);

            var jResult = JsonConvert.SerializeObject(createdPage.Result, Formatting.Indented);
            AppDataFiles.CreateFile($"{title}.json", jResult);

            return createdPage.Result;
        }

        internal static async Task<Page> CreatePageFromMarkdownAsync(Account account)
        {
            var title = Faker.Lorem.GetFirstWord();
            var fileText = GetTextFromFile("example_1.md");
            var nodes = TelegraphMarkdown.Parse(fileText).ToArray();

            var page = Page.Create(account, Faker.Lorem.Sentence(), nodes, true);

            var pageService = new PageService();

            var createdPage = await pageService.CreatePageAsync(account.AccessToken, page);

            var jResult = JsonConvert.SerializeObject(createdPage.Result, Formatting.Indented);
            AppDataFiles.CreateFile($"{title}.json", jResult);

            return createdPage.Result;
        }
    }
}
