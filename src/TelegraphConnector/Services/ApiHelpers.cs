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

    /// <summary>
    /// The default response from API Telegraph Services, See <see cref="TelegraphResponse">TelegraphResponse</see> for leaning more.
    /// </summary>
    /// <typeparam name="TType">This type defines the Result property type of this object. 
    /// The result can be a 
    /// <see cref="Account">Account</see>, 
    /// <see cref="Page">Page</see>,
    /// <see cref="PageViews">PageViews</see>
    /// <see cref="PageTotal">PageTotal</see>
    /// <see cref="Node">Node</see>
    /// </typeparam>
    public class TelegraphResponse<TType> : TelegraphResponse where TType : AbstractTypes
    {
        public TType Result { get; private set; }
    }

    /// <summary>
    /// It has the default messages from the API. Expect to receive a boolean in the <c>Ok</c> property, this means that the request was successfully executed.
    /// otherwise, the property will be false and the <c>Error</c> property will have the error message.
    /// </summary>
    public abstract class TelegraphResponse
    {
        public bool Ok { get; protected set; }
        public string Error { get; protected set; }
    }

}
