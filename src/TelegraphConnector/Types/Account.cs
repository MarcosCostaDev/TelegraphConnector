using Newtonsoft.Json;

namespace TelegraphConnector.Types
{
    public class Account : AbstractTypes
    {
        protected Account() { }

        public static Account Create(string shortName, string authorName, string authorUrl)
        {
            return new Account()
            {
                ShortName = shortName,
                AuthorName = authorName,
                AuthorUrl = authorUrl
            };        
        }

        public static Account Edit(string accessToken, string shortName,  string authorName, string authorUrl)
        {
            return new Account()
            {
                AccessToken = accessToken,
                ShortName = shortName,
                AuthorName = authorName,
                AuthorUrl = authorUrl
            };
        }

        public static Account Info(string accessToken)
        {
            return new Account()
            {
                AccessToken = accessToken
            };
        }

        public static Account RevokeToken(string accessToken, string authUrl)
        {
            return new Account()
            {
                AccessToken = accessToken,
                AuthUrl = authUrl
            };
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; protected set; }


        [JsonProperty("short_name")]
        public string ShortName { get; private set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; private set; }

        [JsonProperty("author_url")]
        public string AuthorUrl { get; private set; }

        [JsonProperty("auth_url")]
        public string AuthUrl { get; private set; }

        [JsonProperty("page_count")]
        public int PageCount { get; private set; }


    }
}
