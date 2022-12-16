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
    public class AccountCommands
    {
        private readonly ITelegraphClient _telegraphClient;

        public AccountCommands(ITelegraphClient? telegraphClient = null)
        {
            _telegraphClient = telegraphClient ?? new TelegraphClient();
        }
        public async Task<TelegraphResult<Account>> CreateAccountAsync(Account account, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync(string.Concat("createAccount?", account.ToQueryString()), null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResult<Account>>(apiResult);

        }

        public async Task<TelegraphResult<Account>> EditAccountInfoAsync(Account account, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync(string.Concat("editAccountInfo?", account.ToQueryString()), null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResult<Account>>(apiResult);
        }

        public async Task<TelegraphResult<Account>> GetAccountInfoAsync(Account account, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"getAccountInfo?accessToken={account.AccessToken}&fields={JsonConvert.SerializeObject(account.ToFieldNames())}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResult<Account>>(apiResult);

        }

        public async Task<TelegraphResult<Account>> RevokeAccessAsync(Account account, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"revokeAccessToken?accessToken={account.AccessToken}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResult<Account>>(apiResult);

        }
    }
}
