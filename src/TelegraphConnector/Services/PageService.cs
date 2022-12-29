using Newtonsoft.Json;
using System.Text;
using TelegraphConnector.Exceptions;
using TelegraphConnector.Types;

namespace TelegraphConnector.Services
{
    public class PageService : AbstractService
    {
        public PageService(ITelegraphClient? telegraphClient = null, CancellationToken? cancellationToken = null) : base(telegraphClient, cancellationToken) { }

        /// <summary>
        /// Use this method to create a new Telegraph page.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="page"></param>
        /// <returns></returns>
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
        /// <param name="accessToken"></param>
        /// <param name="page"></param>
        /// <returns></returns>
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
        /// <param name="path"></param>
        /// <returns></returns>
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
        /// <param name="accessToken"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
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
        /// <param name="accessToken"></param>
        /// <param name="path"></param>
        /// <param name="date"></param>
        /// <returns></returns>
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
        /// <param name="accessToken"></param>
        /// <param name="path"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
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
