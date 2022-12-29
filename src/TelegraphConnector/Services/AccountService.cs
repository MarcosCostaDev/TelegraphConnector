using Newtonsoft.Json;
using TelegraphConnector.Types;

namespace TelegraphConnector.Services
{
    /// <summary>
    /// Use <c>AccountService</c> for managing your account in the Telegraph. 
    /// </summary>
    public class AccountService : AbstractService
    {
        public AccountService(ITelegraphClient? telegraphClient = null, CancellationToken? cancellationToken = null) : base(telegraphClient, cancellationToken) { }

        /// <summary>
        /// Use this method to create a new Telegraph account. Most users only need one account, but this can be useful for channel administrators who would like to keep individual author names and profile links for each of their channels. 
        /// that you can see the <see cref="TelegraphConnector.Types.Account">Account object</see> with the regular fields and an additional <c>AccessToken</c> field.
        /// </summary>
        /// <param name="account">Create a <see cref="TelegraphConnector.Types.Account">Account.Create</see></param>
        /// <returns>On Success: <see cref="TelegraphConnector.Services.TelegraphResponse">TelegraphResponse</see> in which has a property <c>Result</c> with a <see cref="Account"/> object.</returns>
        public async Task<TelegraphResponse<Account>> CreateAccountAsync(Account account)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync(string.Concat("createAccount?", account.ToQueryString()), null, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Account>>(apiResult);

        }
        /// <summary>
        /// Use this method to update information about a Telegraph account. <c>AccessToken</c> is required
        /// </summary>
        /// <param name="account">Edit account using <see cref="TelegraphConnector.Types.Account">Account.Edit</see></param>
        /// <returns>On Success: <see cref="TelegraphConnector.Services.TelegraphResponse">TelegraphResponse</see> in which has a property <c>Result</c> with a <see cref="Account"/> object.</returns>
        public async Task<TelegraphResponse<Account>> EditAccountInfoAsync(Account account)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync(string.Concat("editAccountInfo?", account.ToQueryString()), null, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Account>>(apiResult);
        }

        /// <summary>
        /// Use this method to get information about a Telegraph account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>On Success: <see cref="TelegraphConnector.Services.TelegraphResponse">TelegraphResponse</see> in which has a property <c>Result</c> with a <see cref="Account"/> object.</returns>
        public async Task<TelegraphResponse<Account>> GetAccountInfoAsync(Account account)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"getAccountInfo?access_token={account.AccessToken}&fields={JsonConvert.SerializeObject(account.ToFieldNames())}", null, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Account>>(apiResult);

        }
        /// <summary>
        /// Use this method to revoke the <c>AccessToken</c> and generate a new one, for example, if the user would like to reset all connected sessions, or you have reasons to believe the token was compromised. 
        /// </summary>
        /// <param name="account"></param>
        /// <returns>On Success: <see cref="TelegraphConnector.Services.TelegraphResponse">TelegraphResponse</see> in which has a property <c>Result</c> with a <see cref="Account"/> object with a new <c>AccessToken</c> and <c>AuthUrl</c>.</returns>
        public async Task<TelegraphResponse<Account>> RevokeAccessAsync(Account account)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"revokeAccessToken?access_token={account.AccessToken}", null, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Account>>(apiResult);

        }
    }
}
