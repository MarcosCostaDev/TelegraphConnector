using Newtonsoft.Json;
using System.Reflection;
using System.Web;
using TelegraphConnector.Types;

namespace TelegraphConnector.Api
{

    public interface ITelegraphClient
    {
        HttpClient GetHttpClient();
    }


    public class TelegraphClient : ITelegraphClient
    {
        public HttpClient GetHttpClient()
        {
            return new HttpClient()
            {
                BaseAddress = new Uri(ApiHelpers.API_TELEGRAPH)
            };
        }
    }

    internal static class ApiHelpers
    {
        internal const string API_TELEGRAPH = "https://api.telegra.ph";

        

        internal static string ToQueryString<TType>(this TType obj) where TType : AbstractTypes
        {
            var serialized = JsonConvert.SerializeObject(obj);

            var dictionary = JsonConvert.DeserializeObject<IDictionary<string, string>>(serialized);

            var step3 = dictionary.Where(p => !string.IsNullOrEmpty(p.Value)).Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value));

            return string.Join("&", step3);
        }

        internal static IEnumerable<string> ToFieldNames<TType>(this TType obj) where TType : AbstractTypes
        {
            var propertyInfos = obj.GetType().GetProperties(BindingFlags.Public);

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                var attributes = (JsonPropertyAttribute[])propertyInfo.GetCustomAttributes(
                 typeof(JsonPropertyAttribute), false);

                if (string.Equals(propertyInfo.Name, "AccessToken", StringComparison.OrdinalIgnoreCase)) continue;

                if (attributes != null && attributes.Length > 0)
                    yield return attributes[0].PropertyName;


                yield return propertyInfo.Name.ToLower();
            }
        }
    }


    public class TelegraphResult<TType> where TType : AbstractTypes
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public TType Result { get; set; }
    }
    public class TelegraphResult
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public object Result { get; set; }
    }

}
