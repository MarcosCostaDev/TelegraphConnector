using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TelegraphConnector.Types;

namespace TelegraphConnector.Api
{
    public static class AccountCommands
    {
        public static async Task<Account> CreateAccountAsync(Account account, CancellationToken? cancellationToken = null)
        {
            using var httpClient = ApiHelpers.GetHttpClient();

            var response = await httpClient.PostAsync(string.Concat("createAccount?", account.ToQueryString()), null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadFromJsonAsync<ApiResult<Account>>(cancellationToken: cancellationToken.GetValueOrDefault());

            return apiResult?.Result;
        }

        public static async Task<Account> EditAccountInfoAsync(Account account, CancellationToken? cancellationToken = null)
        {
            using var httpClient = ApiHelpers.GetHttpClient();

            var response = await httpClient.PostAsync(string.Concat("editAccountInfo?", account.ToQueryString()), null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadFromJsonAsync<ApiResult<Account>>(cancellationToken: cancellationToken.GetValueOrDefault());

            return apiResult?.Result;
        }

        public static async Task<Account> GetAccountInfoAsync(Account account, CancellationToken? cancellationToken = null)
        {
            using var httpClient = ApiHelpers.GetHttpClient();

            var response = await httpClient.PostAsync($"getAccountInfo?accessToken={account.AccessToken}&fields={JsonConvert.SerializeObject(account.ToFieldNames())}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadFromJsonAsync<ApiResult<Account>>(cancellationToken: cancellationToken.GetValueOrDefault());

            return apiResult?.Result;
        }
    }
}
