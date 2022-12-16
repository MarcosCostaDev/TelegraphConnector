using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TelegraphConnector.Types
{
    public class Page : AbstractTypes
    {
        protected Page() { }

        public static Page Create(Account account, string title, Node content, bool returnContent = false)
        {
            return Create(title, account.AuthorName, account.AuthorUrl, content, returnContent);
        }
        public static Page Create(string title, string authorName, string authorUrl, Node content, bool returnContent = false)
        {
            var page = new Page()
            {
                Title = title,
                AuthorName = authorName,
                AuthorUrl = authorUrl,
                Content = content,
                ReturnContent = returnContent
            };

            return page;
        }

        public static Page Edit(Account account, string path, string title, Node content, bool returnContent = false)
        {
            return Edit(path, title, account.AuthorName, account.AuthorUrl, content, returnContent);
        }

        public static Page Edit(string path, string title, string authorName, string authorUrl, Node content, bool returnContent = false)
        {
            var page = new Page()
            {
                Path = path,
                Title = title,
                AuthorName = authorName,
                AuthorUrl = authorUrl,
                Content = content,
                ReturnContent = returnContent
            };
            return page;
        }

        public static Page Get(string path, bool returnContent = false)
        {
            var page = new Page()
            {
                Path = path,
                ReturnContent = returnContent
            };
            return page;
        }

        public static Page GetViews(string path)
        {
            var page = new Page()
            {
                Path = path
            };
            return page;
        }


        public string Path { get; private set; }
        public string Url { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        [JsonProperty("author_name")]
        public string AuthorName { get; private set; }
        [JsonProperty("author_url")]
        public string AuthorUrl { get; private set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; private set; }
        public Node Content { get; private set; }
        public int Views { get; private set; }
        [JsonProperty("can_edit")]
        public bool CanEdit { get; private set; }
        [JsonProperty("return_content")]
        public bool ReturnContent { get; private set; }

    }
}
