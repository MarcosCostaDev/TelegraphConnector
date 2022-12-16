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
        }
        public async Task<TelegraphResult<Page>> CreatePageAsync(string accessToken, Page page, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"createPage?access_token={accessToken}&{page.ToQueryString()}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResult<Page>>(apiResult);
        }

        public async Task<TelegraphResult<Page>> EditPageAsync(string accessToken, Page page, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&{page.ToQueryString()}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResult<Page>>(apiResult);
        }

        public async Task<TelegraphResult<Page>> GetPageAsync(string accessToken, Page page, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&{page.ToQueryString()}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResult<Page>>(apiResult);
        }

        public async Task<TelegraphResult<PageTotal>> GetPageListAsync(string accessToken, Page page, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&{page.ToQueryString()}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();


            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResult<PageTotal>>(apiResult);
        }

        public async Task<TelegraphResult<PageViews>> GetViewsAsync(string accessToken, Page page, DateTime date, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&path={page.Path}&year={date.Year}&month={date.Month}&day={date.Day}&hour={date.Hour}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResult<PageViews>>(apiResult);
        }


        public async Task<TelegraphResult<PageViews>> GetViewsAsync(string accessToken, Page page, int? year = null, int? month = null, int? day = null, int? hour = null, CancellationToken? cancellationToken = null)
        {
            using var httpClient = _telegraphClient.GetHttpClient();

            var sb = new StringBuilder($"editPage?access_token={accessToken}&path={page.Path}");

            if (year != null) sb.Append($"&year={year}");
            if (month != null) sb.Append($"&month={month}");
            if (day != null) sb.Append($"&day={day}");
            if (hour != null) sb.Append($"&hour={day}");


            var response = await httpClient.PostAsync(sb.ToString(), null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken.GetValueOrDefault());

            return JsonConvert.DeserializeObject<TelegraphResult<PageViews>>(apiResult);
        }


    }
}
