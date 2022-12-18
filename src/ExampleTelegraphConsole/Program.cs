using Newtonsoft.Json;
using System.Reflection;
using TelegraphConnector.Services;
using TelegraphConnector.Types;

namespace ExampleTelegraphConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var account = AccountData.CreateAccountIfNotRegisteredAsync().GetAwaiter().GetResult();
                var pageSingle = PageData.CreateSinglePageAsync(account).GetAwaiter().GetResult();
                var pageHtml = PageData.CreatePageFromHtmlAsync(account).GetAwaiter().GetResult();

                Console.WriteLine(JsonConvert.SerializeObject(pageSingle, Formatting.Indented));
                Console.WriteLine(JsonConvert.SerializeObject(pageHtml, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
           
            }
            Console.ReadKey();

        }


    }
}