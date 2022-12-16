using Newtonsoft.Json;
using System.Linq;
using System.Xml.Linq;
using TelegraphConnector.Helpers;

namespace TelegraphConnector.Types
{
    [JsonConverter(typeof(NodeConverter))]
    public class Node : AbstractTypes
    {
        public Node() { }

        public Node(string text)
        {
            Tag = "_text";
            Attributes = new Dictionary<string, string>
            {
                { "value", text }
            };

        }

        public void AddChildren(IEnumerable<Node> nodeElements)
        {
            Children ??= new List<Node>();
            Children.AddRange(nodeElements);
        }

        public static implicit operator string(Node node) => node.Tag == "_text" ? node.Attributes["value"] : null;
        public static implicit operator Node(string value) => string.IsNullOrEmpty(value) ? null : new(value);
        public string Tag { get; private set; }
        [JsonProperty("attrs")]
        public IDictionary<string, string> Attributes { get; private set; }
        public List<Node> Children { get; private set; }

        [JsonIgnore]
        public string Value
        {
            get => Tag == "_text" ? Attributes["value"] : null;
            set
            {
                Tag = "_text";
                Attributes = new Dictionary<string, string>
                {
                    { "value", value }
                 };
            }
        }



    }
}
