﻿using Newtonsoft.Json;
using TelegraphConnector.Types;

namespace TelegraphConnector.Api
{
    public class AccountCommands : AbstractCommands
    {
        public AccountCommands(ITelegraphClient? telegraphClient = null, CancellationToken? cancellationToken = null) : base(telegraphClient, cancellationToken) { }

        public async Task<TelegraphResponse<Account>> CreateAccountAsync(Account account)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync(string.Concat("createAccount?", account.ToQueryString()), null, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Account>>(apiResult);

        }

        public async Task<TelegraphResponse<Account>> EditAccountInfoAsync(Account account)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync(string.Concat("editAccountInfo?", account.ToQueryString()), null, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Account>>(apiResult);
        }

        public async Task<TelegraphResponse<Account>> GetAccountInfoAsync(Account account)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"getAccountInfo?accessToken={account.AccessToken}&fields={JsonConvert.SerializeObject(account.ToFieldNames())}", null, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Account>>(apiResult);

        }

        public async Task<TelegraphResponse<Account>> RevokeAccessAsync(Account account)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"revokeAccessToken?accessToken={account.AccessToken}", null, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Account>>(apiResult);

        }
    }
}
