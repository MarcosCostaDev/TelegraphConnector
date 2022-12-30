using Newtonsoft.Json;
using TelegraphConnector.Services;

namespace ExampleTelegraphConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var account = AccountData.CreateAccountIfNotRegisteredAsync().GetAwaiter().GetResult();

                Console.WriteLine("Account:");
                Console.WriteLine(JsonConvert.SerializeObject(account, Formatting.Indented));

                var pageSingle = PageData.CreateSinglePageAsync(account).GetAwaiter().GetResult();

                Console.WriteLine("Page Single:");
                Console.WriteLine(JsonConvert.SerializeObject(pageSingle, Formatting.Indented));
                
                pageSingle = PageData.EditSinglePageAsync(account, pageSingle).GetAwaiter().GetResult();

                Console.WriteLine("Edited Page Single:");
                Console.WriteLine(JsonConvert.SerializeObject(pageSingle, Formatting.Indented));

                var pageHtml = PageData.CreatePageFromHtmlAsync(account).GetAwaiter().GetResult();

                Console.WriteLine("Html Page:");
                Console.WriteLine(JsonConvert.SerializeObject(pageHtml, Formatting.Indented));


                var pageMarkdown = PageData.CreatePageFromMarkdownAsync(account).GetAwaiter().GetResult();

                Console.WriteLine("Markdown Page:");
                Console.WriteLine(JsonConvert.SerializeObject(pageMarkdown, Formatting.Indented));
                               

                Console.WriteLine("\r\n\r\n");
                Console.WriteLine("-----------------");
                Console.WriteLine("---PageService---");
                Console.WriteLine("-----------------");
                Console.WriteLine("\r\n\r\n");

                var pageService = new PageService();

                var pageViews = pageService.GetViewsAsync(account.AccessToken, pageSingle.Path).GetAwaiter().GetResult();

                Console.WriteLine("GetViewsAsync 1:");
                Console.WriteLine(JsonConvert.SerializeObject(pageViews, Formatting.Indented));

                Console.WriteLine("\r\n\r\n");
                Console.WriteLine("-----------------");
                Console.WriteLine("\r\n\r\n");

                var pageViews2 = pageService.GetViewsAsync(account.AccessToken, pageSingle.Path, 2022, 12).GetAwaiter().GetResult();

                Console.WriteLine("GetViewsAsync 2:");
                Console.WriteLine(JsonConvert.SerializeObject(pageViews2, Formatting.Indented));

                Console.WriteLine("\r\n\r\n");
                Console.WriteLine("-----------------");
                Console.WriteLine("\r\n\r\n");

                var returnedPage = pageService.GetPageAsync(pageSingle.Path).GetAwaiter().GetResult();

                Console.WriteLine("GetPageAsync:");
                Console.WriteLine(JsonConvert.SerializeObject(returnedPage, Formatting.Indented));

                Console.WriteLine("\r\n\r\n");
                Console.WriteLine("-----------------");
                Console.WriteLine("\r\n\r\n");

                var pageList = pageService.GetPageListAsync(account.AccessToken).GetAwaiter().GetResult();

                Console.WriteLine("GetPageListAsync:");
                Console.WriteLine(JsonConvert.SerializeObject(pageList, Formatting.Indented));


                Console.WriteLine("\r\n\r\n");
                Console.WriteLine("-----------------");
                Console.WriteLine("--AccountService-");
                Console.WriteLine("-----------------");
                Console.WriteLine("\r\n\r\n");

                Console.WriteLine("Created Account:");
                Console.WriteLine(JsonConvert.SerializeObject(account, Formatting.Indented));

                var accountService = new AccountService();

                var accountInfo = accountService.GetAccountInfoAsync(account).GetAwaiter().GetResult();

                Console.WriteLine("GetAccountInfoAsync:");
                Console.WriteLine(JsonConvert.SerializeObject(accountInfo, Formatting.Indented));

                var accountRevokeToken = accountService.RevokeAccessAsync(account).GetAwaiter().GetResult();

                Console.WriteLine("RevokeAccessAsync:");
                Console.WriteLine(JsonConvert.SerializeObject(accountRevokeToken, Formatting.Indented));


                var getAccountInfoAfterRevokeToken = accountService.GetAccountInfoAsync(accountRevokeToken.Result).GetAwaiter().GetResult();

                Console.WriteLine("GetAccountInfoAsync after token revoked:");
                Console.WriteLine(JsonConvert.SerializeObject(getAccountInfoAfterRevokeToken, Formatting.Indented));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
           
            }
            Console.ReadKey();

        }


    }
}