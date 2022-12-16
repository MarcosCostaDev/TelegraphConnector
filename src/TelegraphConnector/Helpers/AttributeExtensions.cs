using Newtonsoft.Json;
using System.ComponentModel;
using System.Reflection;
using TelegraphConnector.Types;

namespace TelegraphConnector.Helpers
{
    internal static class AttributeExtensions
    {
        public static string DescriptionAttr<T>(this T source) where T : AbstractTypes
        {
            var fi = source.GetType().GetField(source.ToString());

            JsonPropertyAttribute[] attributes = (JsonPropertyAttribute[])fi.GetCustomAttributes(
                typeof(JsonPropertyAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].PropertyName;
            else return source.ToString();
        }
    }
}
