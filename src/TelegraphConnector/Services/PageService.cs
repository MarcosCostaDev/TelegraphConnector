using Newtonsoft.Json;
using System.Text;
using TelegraphConnector.Exceptions;
using TelegraphConnector.Types;

namespace TelegraphConnector.Services
{
    /// <summary>
    /// Use <c>PageService</c> for managing your pages in the Telegraph. 
    /// </summary>
    public class PageService : AbstractService
    {
        public PageService(ITelegraphClient? telegraphClient = null, CancellationToken? cancellationToken = null) : base(telegraphClient, cancellationToken) { }

        /// <summary>
        /// Use this method to create a new Telegraph page.
        /// </summary>
        /// <param name="accessToken">Required. Access token of the Telegraph account. Use your access token retrieve when you <see cref="AccountService.CreateAccountAsync(Account)">create an account</see> or <see cref="AccountService.RevokeAccessAsync(Account)">revoke a token</see></param>
        /// <param name="page">Retrieve the object needed using: <see cref="Page.Create(Account, string, Node[], bool)"/> or <see cref="Page.Create(string, string, string, Node[], bool)"/></param>
        /// <returns>On Success: <see cref="TelegraphConnector.Services.TelegraphResponse">TelegraphResponse</see> in which has a property <c>Result</c> with a <see cref="Page"/> object.</returns>
        /// <exception cref="TelegraphConnectorException"></exception>
        public async Task<TelegraphResponse<Page>> CreatePageAsync(string accessToken, Page page)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(accessToken, nameof(accessToken));
            ArgumentNullException.ThrowIfNull(page, nameof(page));  

            using var httpClient = _telegraphClient.GetHttpClient();

            var body = new
            {
                AccessToken = accessToken,
                page.Title,
                page.Content,
                page.ReturnContent,
                page.AuthorName,
            };

            var content = GetJsonContent(body);

            var response = await httpClient.PostAsync($"createPage", content, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            var objResult = JsonConvert.DeserializeObject<TelegraphResponse<Page>>(apiResult);
            if(!objResult.Ok)
            {
                throw new TelegraphConnectorException(objResult.Error);
            }
            return objResult;
                      
        }

        /// <summary>
        /// Use this method to edit an existing Telegraph page.
        /// </summary>
        /// <param name="accessToken">Required. Access token of the Telegraph account. Use your access token retrieve when you <see cref="AccountService.CreateAccountAsync(Account)">create an account</see> or <see cref="AccountService.RevokeAccessAsync(Account)">revoke a token</see></param>
        /// <param name="page">Retrieve the object needed using: <see cref="Page.Edit(Account, string, string, Node[], bool)"/></param>
        /// <returns>On Success: <see cref="TelegraphConnector.Services.TelegraphResponse">TelegraphResponse</see> in which has a property <c>Result</c> with a <see cref="Page"/> object.</returns>
        public async Task<TelegraphResponse<Page>> EditPageAsync(string accessToken, Page page)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(accessToken, nameof(accessToken));
            ArgumentNullException.ThrowIfNull(page, nameof(page));

            using var httpClient = _telegraphClient.GetHttpClient();

            var body = new
            {
                AccessToken = accessToken,
                page.Title,
                page.Content,
                page.ReturnContent,
                page.AuthorName,
                page.Path
            };

            var response = await httpClient.PostAsync($"editPage", GetJsonContent(body), _cancellationToken);

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Page>>(apiResult);
        }

        /// <summary>
        /// Use this method to get a Telegraph page. 
        /// </summary>
        /// <param name="path">The path of the Page, you can use <see cref="Page.Path"/> that is retrieved when you create a page.</param>
        /// <returns>On Success: <see cref="TelegraphConnector.Services.TelegraphResponse">TelegraphResponse</see> in which has a property <c>Result</c> with a <see cref="Page"/> object.</returns>
        public async Task<TelegraphResponse<Page>> GetPageAsync(string path)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(path, nameof(path));

            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.GetAsync($"getPage/{path}?return_content=true", _cancellationToken);

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Page>>(apiResult);
        }

        /// <summary>
        /// Use this method to get a list of pages belonging to a Telegraph account.
        /// </summary>
        /// <param name="accessToken">Required. Access token of the Telegraph account. Use your access token retrieve when you <see cref="AccountService.CreateAccountAsync(Account)">create an account</see> or <see cref="AccountService.RevokeAccessAsync(Account)">revoke a token</see> </param>
        /// <param name="offset">Sequential number of the first page to be returned.</param>
        /// <param name="limit">Sequential number of the first page to be returned.</param>
        /// <returns>On Success: <see cref="TelegraphConnector.Services.TelegraphResponse">TelegraphResponse</see> in which has a property <c>Result</c> with a <see cref="PageTotal"/> object.</returns>
        public async Task<TelegraphResponse<PageTotal>> GetPageListAsync(string accessToken, int offset = 0, int limit = 50)
        {

            ArgumentNullException.ThrowIfNullOrEmpty(accessToken, nameof(accessToken));

            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.GetAsync($"getPageList?access_token={accessToken}&offset={offset}&limit={limit}", _cancellationToken);

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<PageTotal>>(apiResult);
        }

        /// <summary>
        /// Use this method to get the number of views for a Telegraph article. 
        /// </summary>
        /// <param name="accessToken">Required. Access token of the Telegraph account. Use your access token retrieve when you <see cref="AccountService.CreateAccountAsync(Account)">create an account</see> or <see cref="AccountService.RevokeAccessAsync(Account)">revoke a token</see></param>
        /// <param name="path">Required. Path to the Telegraph page. <see cref="Page.Path"/></param>
        /// <param name="date">The date used here will be use for the overloaded method <see cref="PageService.GetViewsAsync(string, string, int?, int?, int?, int?)"/></param>
        /// <returns>On Success: <see cref="TelegraphConnector.Services.TelegraphResponse">TelegraphResponse</see> in which has a property <c>Result</c> with a <see cref="PageViews"/> object.</returns>
        public async Task<TelegraphResponse<PageViews>> GetViewsAsync(string accessToken, string path, DateTime date)
        {

            ArgumentNullException.ThrowIfNullOrEmpty(accessToken, nameof(accessToken));
            ArgumentNullException.ThrowIfNullOrEmpty(path, nameof(path));

            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"getViews?access_token={accessToken}&path={path}&year={date.Year}&month={date.Month}&day={date.Day}&hour={date.Hour}", null, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<PageViews>>(apiResult);
        }

        /// <summary>
        /// Use this method to get the number of views for a Telegraph article. 
        /// </summary>
        /// <param name="accessToken">Required. Access token of the Telegraph account. Use your access token retrieve when you <see cref="AccountService.CreateAccountAsync(Account)">create an account</see> or <see cref="AccountService.RevokeAccessAsync(Account)">revoke a token</see></param>
        /// <param name="path">Required. Path to the Telegraph page. <see cref="Page.Path"/></param>
        /// <param name="year">Required if month is passed. If passed, the number of page views for the requested year will be returned.</param>
        /// <param name="month">Required if day is passed. If passed, the number of page views for the requested month will be returned.</param>
        /// <param name="day">Required if hour is passed. If passed, the number of page views for the requested day will be returned.</param>
        /// <param name="hour">Required if hour is passed.</param>
        /// <returns>On Success: <see cref="TelegraphConnector.Services.TelegraphResponse">TelegraphResponse</see> in which has a property <c>Result</c> with a <see cref="PageViews"/> object.</returns>
        public async Task<TelegraphResponse<PageViews>> GetViewsAsync(string accessToken, string path, int? year = null, int? month = null, int? day = null, int? hour = null)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(accessToken, nameof(accessToken));
            ArgumentNullException.ThrowIfNullOrEmpty(path, nameof(path));

            using var httpClient = _telegraphClient.GetHttpClient();

            var sb = new StringBuilder($"getViews?access_token={accessToken}&path={path}");

            if (year != null) sb.Append($"&year={year}");
            if (month != null) sb.Append($"&month={month}");
            if (day != null) sb.Append($"&day={day}");
            if (hour != null) sb.Append($"&hour={day}");


            var response = await httpClient.PostAsync(sb.ToString(), null, _cancellationToken);

            response.EnsureSuccessStatusCode();
                
            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<PageViews>>(apiResult);
        }


    }
}
