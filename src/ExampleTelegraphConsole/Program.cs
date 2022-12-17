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
            CreateAccount();
        }
        
        static void CreateAccount()
        {
            var author = Account.Create("test account", "test author", "http://test.com");

            var accountService = new AccountService();
            
            var response = accountService.CreateAccountAsync(author);

            var jResult = JsonConvert.SerializeObject(response.Result, Formatting.Indented);

            
        }
    }
}