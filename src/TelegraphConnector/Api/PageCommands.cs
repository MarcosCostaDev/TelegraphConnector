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
        public static async Task<Page> CreatePageAsync(string accessToken, Page page, CancellationToken? cancellationToken = null)
        {
            using var httpClient = ApiHelpers.GetHttpClient();

            var response = await httpClient.PostAsync($"createPage?access_token={accessToken}&{page.ToQueryString()}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadFromJsonAsync<ApiResult<Page>>(cancellationToken: cancellationToken.GetValueOrDefault());

            return apiResult?.Result;
        }

        public static async Task<Page> EditPageAsync(string accessToken, Page page, CancellationToken? cancellationToken = null)
        {
            using var httpClient = ApiHelpers.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&{page.ToQueryString()}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadFromJsonAsync<ApiResult<Page>>(cancellationToken: cancellationToken.GetValueOrDefault());

            return apiResult?.Result;
        }

        public static async Task<Page> GetPageAsync(string accessToken, Page page, CancellationToken? cancellationToken = null)
        {
            using var httpClient = ApiHelpers.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&{page.ToQueryString()}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadFromJsonAsync<ApiResult<Page>>(cancellationToken: cancellationToken.GetValueOrDefault());

            return apiResult?.Result;
        }

        public static async Task<PageTotal> GetPageListAsync(string accessToken, Page page, CancellationToken? cancellationToken = null)
        {
            using var httpClient = ApiHelpers.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&{page.ToQueryString()}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadFromJsonAsync<ApiResult<PageTotal>>(cancellationToken: cancellationToken.GetValueOrDefault());

            return apiResult?.Result;
        }

        public static async Task<PageViews> GetViewsAsync(string accessToken, Page page, DateTime date, CancellationToken? cancellationToken = null)
        {
            using var httpClient = ApiHelpers.GetHttpClient();

            var response = await httpClient.PostAsync($"editPage?access_token={accessToken}&path={page.Path}&year={date.Year}&month={date.Month}&day={date.Day}&hour={date.Hour}", null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadFromJsonAsync<ApiResult<PageViews>>(cancellationToken: cancellationToken.GetValueOrDefault());

            return apiResult?.Result;
        }


        public static async Task<PageViews> GetViewsAsync(string accessToken, Page page, int? year = null, int? month = null, int? day = null, int? hour = null, CancellationToken? cancellationToken = null)
        {
            using var httpClient = ApiHelpers.GetHttpClient();

            var sb = new StringBuilder($"editPage?access_token={accessToken}&path={page.Path}");

            if (year != null) sb.Append($"&year={year}");
            if (month != null) sb.Append($"&month={month}");
            if (day != null) sb.Append($"&day={day}");
            if (hour != null) sb.Append($"&hour={day}");


            var response = await httpClient.PostAsync(sb.ToString(), null, cancellationToken.GetValueOrDefault());

            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadFromJsonAsync<ApiResult<PageViews>>(cancellationToken: cancellationToken.GetValueOrDefault());

            return apiResult?.Result;
        }


    }
}
