using Newtonsoft.Json;
using TelegraphConnector.Services;

namespace TelegraphConnector.Types
{
    /// <summary>
    /// This object represents a page on Telegraph.
    /// </summary>
    public class Page : AbstractTypes
    {
        protected Page() { }
        /// <summary>
        /// Use this method to create a object to execute <see cref="Services.PageService.CreatePageAsync(string, Page)"/>.
        /// </summary>
        /// <param name="account"><see cref="Account"/></param>
        /// <param name="title">Required. Page title.</param>
        /// <param name="content">Required. <see cref="Node">Content</see> of the page. Array of <see cref="Node"/></param>
        /// <param name="returnContent">If <c>true</c>, a content field will be returned in the <see cref="Page"/> object</param>
        /// <returns><see cref="Page"/> object for using in <see cref="Services.PageService.CreatePageAsync(string, Page)"/>.</returns>
        public static Page Create(Account account, string title, Node[] content, bool returnContent = false)
        {
            ArgumentNullException.ThrowIfNull(account, nameof(account));
            ArgumentNullException.ThrowIfNullOrEmpty(title, nameof(title));

            return Create(title, account.AuthorName, account.AuthorUrl, content, returnContent);
        }

        /// <summary>
        /// Use this method to create a object to execute <see cref="Services.PageService.CreatePageAsync(string, Page)"/>.
        /// </summary>
        /// <param name="title">Required. Page title.</param>
        /// <param name="authorName">Author name, displayed below the article's title.</param>
        /// <param name="authorUrl">Profile link, opened when users click on the author's name below the title. Can be any link, not necessarily to a Telegram profile or channel.</param>
        /// <param name="content">Required. <see cref="Node">Content</see> of the page. Array of <see cref="Node"/></param>
        /// <param name="returnContent">If <c>true</c>, a content field will be returned in the <see cref="Page"/> object</param>
        /// <returns><see cref="Page"/> object for using in <see cref="Services.PageService.CreatePageAsync(string, Page)"/>.</returns>
        public static Page Create(string title, string authorName, string authorUrl, Node[] content, bool returnContent = false)
        {          
            ArgumentNullException.ThrowIfNullOrEmpty(title, nameof(title));
            ArgumentNullException.ThrowIfNullOrEmpty(authorName, nameof(authorName));

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
        /// <summary>
        /// Use this method to create a object to execute <see cref="Services.PageService.EditPageAsync(string, Page)"/>.
        /// </summary>
        /// <param name="account"><see cref="Account"/></param>
        /// <param name="path">Required. Path to the page.</param>
        /// <param name="title">Required. Page title.</param>
        /// <param name="content">Required. <see cref="Node">Content</see> of the page. Array of <see cref="Node"/></param>
        /// <param name="returnContent">If <c>true</c>, a content field will be returned in the <see cref="Page"/> object</param>
        /// <returns><see cref="Page"/> object for using in <see cref="Services.PageService.EditPageAsync(string, Page)"/>.</returns>
        public static Page Edit(Account account, string path, string title, Node[] content, bool returnContent = false)
        {
            return Edit(path, title, account.AuthorName, account.AuthorUrl, content, returnContent);
        }

        /// <summary>
        /// Use this method to create a object to execute <see cref="Services.PageService.EditPageAsync(string, Page)"/>.
        /// </summary>
        /// <param name="path">Required. Path to the page.</param>
        /// <param name="title">Required. Page title.</param>
        /// <param name="authorName">Required. Content of the page.</param>
        /// <param name="authorUrl">Profile link, opened when users click on the author's name below the title. Can be any link, not necessarily to a Telegram profile or channel.</param>
        /// <param name="content">Required. <see cref="Node">Content</see> of the page. Array of <see cref="Node"/></param>
        /// <param name="returnContent">If <c>true</c>, a content field will be returned in the <see cref="Page"/> object</param>
        /// <returns><see cref="Page"/> object for using in <see cref="Services.PageService.EditPageAsync(string, Page)"/>.</returns>
        public static Page Edit(string path, string title, string authorName, string authorUrl, Node[] content, bool returnContent = false)
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


        public void SetContent(params Node[] nodes)
        {
            Content = nodes;
        }

        /// <summary>
        /// Path to the page.
        /// </summary>
        public string Path { get; private set; }
        /// <summary>
        /// URL of the page.
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// Title of the page.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Description of the page.
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Name of the author, displayed below the title.
        /// </summary>
        [JsonProperty("author_name")]
        public string AuthorName { get; private set; }
        /// <summary>
        /// Profile link, opened when users click on the author's name below the title.  Can be any link, not necessarily to a Telegram profile or channel.
        /// </summary>
        [JsonProperty("author_url")]
        public string AuthorUrl { get; private set; }
        /// <summary>
        /// Image URL of the page.
        /// </summary>
        [JsonProperty("image_url")]
        public string ImageUrl { get; private set; }
        /// <summary>
        ///  Content of the page. Array of <see cref="Node"/>
        /// </summary>
        public Node[] Content { get; private set; }
        /// <summary>
        ///  Number of page views for the page.
        /// </summary>
        public int Views { get; private set; }
        /// <summary>
        ///  if the target Telegraph account can edit the page.
        /// </summary>
        [JsonProperty("can_edit")]
        public bool CanEdit { get; private set; }
        /// <summary>
        /// Infomed during the request to return the content of a page
        /// </summary>
        [JsonProperty("return_content")]
        public bool ReturnContent { get; private set; }


        public override string ToString()
        {
            return this.ToQueryString("Content");
        }

    }
}
