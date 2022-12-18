using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Web;
using TelegraphConnector.Types;

namespace TelegraphConnector.Services
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

        

        internal static string ToQueryString<TType>(this TType obj, params string[] ignoreProperties) where TType : AbstractTypes
        {
            var serialized = JsonConvert.SerializeObject(obj);

            var dictionary = JsonConvert.DeserializeObject<IDictionary<string, object>>(serialized);

            var step3 = dictionary.Where(p => !ignoreProperties.Contains(p.Key))
                                  .Where(p => !string.IsNullOrEmpty(p.Value?.ToString()))
                                  .Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value?.ToString()));

            return string.Join("&", step3);
        }

        internal static IEnumerable<string> ToFieldNames<TType>(this TType obj) where TType : AbstractTypes
        {
            var propertyInfos = obj.GetType().GetProperties();

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

    public class NonPublicPropertiesResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if (member is PropertyInfo pi)
            {
                prop.Readable = (pi.GetMethod != null);
                prop.Writable = (pi.SetMethod != null);
            }
            return prop;
        }
    }


    public class TelegraphResponse<TType> where TType : AbstractTypes
    {
        public bool Ok { get; private set; }
        public string Error { get; private set; }
        public TType Result { get; private set; }
    }
    public class TelegraphResponse
    {
        public bool Ok { get; private set; }
        public string Error { get; private set; }
        public dynamic Result { get; private set; }
    }

}
