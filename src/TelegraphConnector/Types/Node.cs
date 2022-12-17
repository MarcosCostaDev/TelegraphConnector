﻿using Newtonsoft.Json;
using System.Linq;
using System.Xml.Linq;
using TelegraphConnector.Exceptions;
using TelegraphConnector.Helpers;

namespace TelegraphConnector.Types
{
    [JsonConverter(typeof(NodeConverter))]
    public class Node : AbstractTypes
    {   
        public static implicit operator string(Node node) => node.Tag == "_text" ? node.Attributes["value"] : null;
        public static implicit operator Node(string value) => string.IsNullOrEmpty(value) ? null : new(value);
        public string Tag { get; private set; }

        [JsonProperty("attrs")]
        public IDictionary<string, string> Attributes { get; private set; }
        public List<Node> Children { get; private set; }

        [JsonIgnore]
        internal string Value
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

        internal Node()
        {
            Children = Enumerable.Empty<Node>().ToList();
            Attributes = new Dictionary<string, string>();
        }

        private Node(string text)
        {
            Tag = "_text";
            Attributes = new Dictionary<string, string>
            {
                { "value", text }
            };

        }

        public void AddChildren(params Node[] node)
        {
            Children.AddRange(node);
        }

        public void AddAttributes(params KeyValuePair<string, string>[] attributes)
        {
            if (attributes == null) return;
            foreach (var item in attributes)
            {
                Attributes.Add(item);
            }
        }

        public static Node CreateTextNode(string text)
        {
            return new Node(text);
        }

        public static Node CreateNode(string tag, KeyValuePair<string, string>[]? attributes, params Node[] nodes)
        {
            var node = new Node
            {
                Tag = tag.ToLower()
            };
            node.AddAttributes(attributes);
            node.AddChildren(nodes);
            return node;
        }

        public static Node CreateHeader1(string text)
        {
            return Node.CreateNode("h1", null, Node.CreateTextNode(text));
        }
        public static Node CreateHeader2(string text)
        {
            return Node.CreateNode("h2", null, Node.CreateTextNode(text));
        }
        public static Node CreateHeader3(string text)
        {
            return Node.CreateNode("h3", null, Node.CreateTextNode(text));
        }

        public static Node CreateHeader4(string text)
        {
            return Node.CreateNode("h4", null, Node.CreateTextNode(text));
        }
        public static Node CreateEmphasis(string text)
        {
            return Node.CreateNode("em", null, Node.CreateTextNode(text));
        }

        public static Node CreateStrong(string text)
        {
            return Node.CreateNode("strong", null, Node.CreateTextNode(text));
        }

        public static Node CreateItalic(string text)
        {
            return Node.CreateNode("i", null, Node.CreateTextNode(text));
        }

        public static Node CreateParagraph(params Node[] nodes)
        {
            return Node.CreateNode("p", null, nodes);
        }

        public static Node CreateSpan(params Node[] nodes)
        {
            return Node.CreateNode("span", null, nodes);
        }

        public static Node CreateUnorderedList(params Node[] nodes)
        {
            if (!nodes.All(p => p.Tag == "li")) throw new ConnectorException("Must be a List Item nodes");
            return Node.CreateNode("ul", null, nodes);
        }

        public static Node CreateOrderedList(params Node[] nodes)
        {
            if (!nodes.All(p => p.Tag == "li")) throw new ConnectorException("Must be a List Item nodes");
            return Node.CreateNode("ol", null, nodes);
        }

        public static Node CreateListItem(params Node[] nodes)
        {
            return Node.CreateNode("li", null, nodes);
        }

        public static Node CreateListItem(string innerText)
        {
            return Node.CreateNode("li", null, Node.CreateTextNode(innerText));
        }

        public static Node CreateAside(params Node[] nodes)
        {
            return Node.CreateNode("aside", null, nodes);
        }

        
        public static Node CreateBreakLine()
        {
            return Node.CreateNode("br", null);
        }

        public static Node CreateHorizontalRule()
        {
            return Node.CreateNode("hr", null);
        }


    }
}
