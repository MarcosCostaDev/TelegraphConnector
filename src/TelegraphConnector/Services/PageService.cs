using Newtonsoft.Json;
using System.Text;
using TelegraphConnector.Types;

namespace TelegraphConnector.Services
{
    public class PageService : AbstractService
    {
        public PageService(ITelegraphClient? telegraphClient = null, CancellationToken? cancellationToken = null) : base(telegraphClient, cancellationToken) { }


        public async Task<TelegraphResponse<Page>> CreatePageAsync(string accessToken, Page page)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var body = new
            {
                AccessToken = accessToken,
                page.Title,
                page.Content,
                page.ReturnContent,
                page.AuthorName,
            };

            var response = await httpClient.PostAsync($"createPage", GetJsonContent(body), _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Page>>(apiResult);
        }

        public async Task<TelegraphResponse<Page>> EditPageAsync(string accessToken, Page page)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var body = new
            {
                AccessToken = accessToken,
                page.Title,
                page.Content,
                page.ReturnContent,
                page.AuthorName,
            };

            var response = await httpClient.PostAsync($"editPage", GetJsonContent(body), _cancellationToken);

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Page>>(apiResult);
        }

        public async Task<TelegraphResponse<Page>> GetPageAsync(string path)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.GetAsync($"getPage/{path}?return_content=true", _cancellationToken);

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<Page>>(apiResult);
        }

        public async Task<TelegraphResponse<PageTotal>> GetPageListAsync(string accessToken, int offset = 0, int limit = 50)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.GetAsync($"getPageList?access_token={accessToken}&offset={offset}&limit={limit}", _cancellationToken);

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<PageTotal>>(apiResult);
        }

        public async Task<TelegraphResponse<PageViews>> GetViewsAsync(string accessToken, string path, DateTime date)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&path={path}&year={date.Year}&month={date.Month}&day={date.Day}&hour={date.Hour}", null, _cancellationToken);

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: _cancellationToken);

            return JsonConvert.DeserializeObject<TelegraphResponse<PageViews>>(apiResult);
        }


        public async Task<TelegraphResponse<PageViews>> GetViewsAsync(string accessToken, string path, int? year = null, int? month = null, int? day = null, int? hour = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var sb = new StringBuilder($"editPage?access_token={accessToken}&path={path}");

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
