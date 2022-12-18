using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegraphConnector.Services;
using TelegraphConnector.Types;

namespace ExampleTelegraphConsole
{
    internal class AccountData
    {
        public static async Task<Account> CreateAccountAsync()
        {
            var author = Account.Create(Faker.Name.First(), Faker.Name.FullName(), Faker.Internet.SecureUrl());

            var accountService = new AccountService();

            var response = await accountService.CreateAccountAsync(author);

            var jResult = JsonConvert.SerializeObject(response.Result, Formatting.Indented);

            AppDataFiles.CreateFile("account.json", jResult);

            return response.Result;
        }

        public static async Task<bool> IsTokenValidAsync(Account account)
        {
            var accountService = new AccountService();

            var response = await accountService.GetAccountInfoAsync(account);

            return response.Ok;

        }

        public static async Task<Account> CreateAccountIfNotRegisteredAsync()
        {
            var accountText = AppDataFiles.ReadFileText("account.json");

            if (!string.IsNullOrEmpty(accountText))
            {
                var account = JsonConvert.DeserializeObject<Account>(accountText);

                if (account != null)
                {
                    var tokenValid = await IsTokenValidAsync(account);
                    if (tokenValid)
                    {
                        return account;
                    }
                }
            }

            return await CreateAccountAsync();
        }
    }
}
