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
    public class PageCommands
    {
        private readonly ITelegraphClient _telegraphClient;

        public PageCommands(ITelegraphClient? telegraphClient = null)
        {
            _telegraphClient = telegraphClient ?? new TelegraphClient();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new NonPublicPropertiesResolver()
            };
        }
        public async Task<TelegraphResponse<Page>> CreatePageAsync(string accessToken, Page page, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"createPage?access_token={accessToken}&{page.ToQueryString()}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResponse<Page>>(apiResult);
        }

        public async Task<TelegraphResponse<Page>> EditPageAsync(string accessToken, Page page, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&{page.ToQueryString()}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResponse<Page>>(apiResult);
        }

        public async Task<TelegraphResponse<Page>> GetPageAsync(string path, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.GetAsync($"getPage/{path}?return_content=true", cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResponse<Page>>(apiResult);
        }

        public async Task<TelegraphResponse<PageTotal>> GetPageListAsync(string accessToken, int offset = 0, int limit = 50, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"getPageList?access_token={accessToken}&offset={offset}&limit={limit}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResponse<PageTotal>>(apiResult);
        }

        public async Task<TelegraphResponse<PageViews>> GetViewsAsync(string accessToken, string path, DateTime date, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&path={path}&year={date.Year}&month={date.Month}&day={date.Day}&hour={date.Hour}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResponse<PageViews>>(apiResult);
        }


        public async Task<TelegraphResponse<PageViews>> GetViewsAsync(string accessToken, string path, int? year = null, int? month = null, int? day = null, int? hour = null, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var sb = new StringBuilder($"editPage?access_token={accessToken}&path={path}");

            if (year != null) sb.Append($"&year={year}");
            if (month != null) sb.Append($"&month={month}");
            if (day != null) sb.Append($"&day={day}");
            if (hour != null) sb.Append($"&hour={day}");


            var response = await httpClient.PostAsync(sb.ToString(), null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResponse<PageViews>>(apiResult);
        }


    }
}
