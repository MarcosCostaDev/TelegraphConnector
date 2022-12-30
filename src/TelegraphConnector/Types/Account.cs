using Newtonsoft.Json;

namespace TelegraphConnector.Types
{
    /// <summary>
    /// This object represents a Telegraph account. 
    /// </summary>
    public class Account : AbstractTypes
    {
        protected Account() { }
        /// <summary>
        /// Use this method to create a object to execute <see cref="Services.AccountService.CreateAccountAsync(Account)"/>.
        /// </summary>
        /// <param name="shortName">Required. Account name, helps users with several accounts remember which they are currently using. Displayed to the user above the "Edit/Publish" button on Telegra.ph, other users don't see this name.</param>
        /// <param name="authorName">Default author name used when creating new articles.</param>
        /// <param name="authorUrl">Default author name used when creating new articles.</param>
        /// <returns><see cref="Account"/></returns>
        public static Account Create(string shortName, string authorName, string authorUrl)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(shortName, nameof(shortName));
            ArgumentNullException.ThrowIfNullOrEmpty(authorName, nameof(authorName));

            return new Account()
            {
                ShortName = shortName,
                AuthorName = authorName,
                AuthorUrl = authorUrl
            };
        }
        /// <summary>
        /// Use this method to create a object to execute <see cref="Services.AccountService.EditAccountInfoAsync(Account)"/>.
        /// </summary>
        /// <param name="accessToken">Required. Access token of the Telegraph account.</param>
        /// <param name="shortName">New account name.</param>
        /// <param name="authorName">New default author name used when creating new articles.</param>
        /// <param name="authorUrl">New default profile link, opened when users click on the author's name below the title. Can be any link, not necessarily to a Telegram profile or channel.</param>
        /// <returns><see cref="Account"/></returns>
        public static Account Edit(string accessToken, string shortName,  string authorName, string authorUrl)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(accessToken, nameof(accessToken));
            ArgumentNullException.ThrowIfNullOrEmpty(shortName, nameof(shortName));
            ArgumentNullException.ThrowIfNullOrEmpty(authorName, nameof(authorName));

            return new Account()
            {
                AccessToken = accessToken,
                ShortName = shortName,
                AuthorName = authorName,
                AuthorUrl = authorUrl
            };
        }

        /// <summary>
        /// Use this method to create a object to execute <see cref="Services.AccountService.GetAccountInfoAsync(Account)"/>.
        /// </summary>
        /// <param name="accessToken">Required. Access token of the Telegraph account.</param>
        /// <returns><see cref="Account"/></returns>
        public static Account Info(string accessToken)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(accessToken));
            return new Account()
            {
                AccessToken = accessToken
            };
        }
        /// <summary>
        /// Use this method to create a object to execute <see cref="Services.AccountService.RevokeAccessAsync(Account)"/>.
        /// </summary>
        /// <param name="accessToken">Required. Access token of the Telegraph account.</param>
        /// <param name="authUrl"></param>
        /// <returns></returns>
        public static Account RevokeToken(string accessToken)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(accessToken, nameof(accessToken));

            return new Account()
            {
                AccessToken = accessToken,
            };
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; protected set; }

        /// <summary>
        /// Account name, helps users with several accounts remember which they are currently using. Displayed to the user above the "Edit/Publish" button on Telegra.ph, other users don't see this name.
        /// </summary>
        [JsonProperty("short_name")]
        public string ShortName { get; private set; }

        /// <summary>
        /// Default author name used when creating new articles.
        /// </summary>
        [JsonProperty("author_name")]
        public string AuthorName { get; private set; }

        /// <summary>
        /// Default profile link, opened when users click on the author's name below the title. Can be any link, not necessarily to a Telegram profile or channel.
        /// </summary>
        [JsonProperty("author_url")]
        public string AuthorUrl { get; private set; }

        /// <summary>
        /// URL to authorize a browser on telegra.ph and connect it to a Telegraph account. This URL is valid for only one use and for 5 minutes only.
        /// </summary>
        [JsonProperty("auth_url")]
        public string AuthUrl { get; private set; }

        /// <summary>
        ///  Number of pages belonging to the Telegraph account.
        /// </summary>
        [JsonProperty("page_count")]
        public int PageCount { get; private set; }


    }
}
