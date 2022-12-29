using Newtonsoft.Json;
using TelegraphConnector.Exceptions;
using TelegraphConnector.Helpers;

namespace TelegraphConnector.Types
{
    /// <summary>
    ///  This object represents a DOM element node. When it is a Node text, you can see the actual value in the property <c>Value</c>.
    /// </summary>
    [JsonConverter(typeof(NodeConverter))]
    public class Node : AbstractTypes
    {   
        public static implicit operator string(Node node) => node.Tag == "_text" ? node.Attributes["value"] : null;
        public static implicit operator Node(string value) => string.IsNullOrEmpty(value) ? null : new(value);

        [JsonProperty("tag")]
        public string Tag { get; private set; }

        [JsonProperty("attrs")]
        public IDictionary<string, string> Attributes { get; private set; }
        [JsonProperty("children")]
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

        public override string ToString()
        { 
            return $"<{Tag}/> {Value}";
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
            if (node == null) return;
            Children.AddRange(node);
        }

        public void InsertChildren(int position, params Node[] node)
        {
            if (node == null) return;
            Children.InsertRange(position, node);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag">Available Tags: <c>a, aside, b, blockquote, br, code, em, figcaption, figure, h3, h4, hr, i, iframe, img, li, ol, p, pre, s, strong, u, ul, video</c></param>
        /// <param name="attributes">A array of <see cref="KeyValuePair"/> that represent property and value of a property </param>
        /// <param name="nodes">A single or array of children <see cref="Node"/></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create a node equivalent to a html anchor. HTML Tag <c>a</c>
        /// </summary>
        /// <param name="content">content</param>
        /// <param name="href">Address of your link</param>
        /// <returns><see cref="Node"/> created</returns>
        /// <exception cref="ArgumentNullException">when <c>content</c> or <c>href</c> is not informed</exception>
        public static Node CreateAnchor(string content, string href)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(content, nameof(content));
            ArgumentNullException.ThrowIfNullOrEmpty(href, nameof(href));

            var attrs = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("href", href),
                new KeyValuePair<string, string>("target", "_blank")
            };
            var node = Node.CreateNode("a", attrs, Node.CreateTextNode(content));
            return node;
        }

        /// <summary>
        /// Create a node equivalent to a html header 3. HTML Tag <c>h3</c>
        /// </summary>
        /// <param name="content">content</param>
        /// <returns><see cref="Node"/> created</returns>
        /// <exception cref="ArgumentNullException">when <c>content</c> is not informed</exception>
        public static Node CreateHeader3(string content)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(content, nameof(content));

            return Node.CreateNode("h3", null, Node.CreateTextNode(content));
        }

        /// <summary>
        /// Create a node equivalent to a html header 4. HTML Tag <c>h4</c>
        /// </summary>
        /// <param name="content">content</param>
        /// <returns><see cref="Node"/> created</returns>
        /// <exception cref="ArgumentNullException">when <c>content</c> is not informed</exception>
        public static Node CreateHeader4(string content)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(content, nameof(content));

            return Node.CreateNode("h4", null, Node.CreateTextNode(content));
        }

        /// <summary>
        /// Create a node equivalent to a html Emphasis. HTML Tag <c>em</c>
        /// </summary>
        /// <param name="content">content</param>
        /// <returns><see cref="Node"/> created</returns>
        /// <exception cref="ArgumentNullException">when <c>content</c> is not informed</exception>
        public static Node CreateEmphasis(string content)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(content, nameof(content));
            return Node.CreateNode("em", null, Node.CreateTextNode(content));
        }

        /// <summary>
        /// Create a node equivalent to a html Strong. HTML Tag <c>strong</c>
        /// </summary>
        /// <param name="content">content</param>
        /// <returns><see cref="Node"/> created</returns>
        /// <exception cref="ArgumentNullException">when <c>content</c> is not informed</exception>
        public static Node CreateStrong(string content)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(content, nameof(content));
            return Node.CreateNode("strong", null, Node.CreateTextNode(content));
        }

        /// <summary>
        /// Create a node equivalent to a html ilalic. HTML Tag <c>i</c>
        /// </summary>
        /// <param name="content">content</param>
        /// <returns><see cref="Node"/> created</returns>
        /// <exception cref="ArgumentNullException">when <c>content</c> is not informed</exception>
        public static Node CreateItalic(string content)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(content, nameof(content));
            return Node.CreateNode("i", null, Node.CreateTextNode(content));
        }

        /// <summary>
        /// Create a node equivalent to a html paragraph. HTML Tag <c>p</c> 
        /// </summary>
        /// <param name="innerNodes"><see cref="Node"/> to be inside the paragraph created with its node children in the <c>Children</c> property</param>
        /// <returns><see cref="Node"/> created</returns>
        public static Node CreateParagraph(params Node[] innerNodes)
        {
            return Node.CreateNode("p", null, innerNodes);
        }

        /// <summary>
        /// Create a node equivalent to a html Unordered List. HTML Tag <c>ul</c> 
        /// </summary>
        /// <param name="nodes">A single or array of <see cref="Node"/> that accept just list item(s) (use <c>Node.CreateListItem()</c> to create a list item) </param>
        /// <returns><see cref="Node"/> created</returns>
        /// <exception cref="TelegraphConnectorException"> When you inform node different of a list item</exception>
        public static Node CreateUnorderedList(params Node[] nodes)
        {
            if (!nodes.All(p => p.Tag == "li")) throw new TelegraphConnectorException("Must be a List Item nodes");
            return Node.CreateNode("ul", null, nodes);
        }

        /// <summary>
        /// Create a node equivalent to a html Ordered List. HTML Tag <c>ol</c> 
        /// </summary>
        /// <param name="nodes">A single or array of <see cref="Node"/> that accept just list item(s) (use <c>Node.CreateListItem()</c> to create a list item) </param>
        /// <returns><see cref="Node"/> created</returns>
        /// <exception cref="TelegraphConnectorException"> When you inform node different of a list item</exception>
        public static Node CreateOrderedList(params Node[] nodes)
        {
            if (!nodes.All(p => p.Tag == "li")) throw new TelegraphConnectorException("Must be a List Item nodes");
            return Node.CreateNode("ol", null, nodes);
        }

        /// <summary>
        /// Create a node equivalent to a html List Item. HTML Tag <c>li</c> 
        /// </summary>
        /// <param name="nodes">A single or array of <see cref="Node"/></param>
        /// <returns><see cref="Node"/> created</returns>
        public static Node CreateListItem(params Node[] nodes)
        {
            return Node.CreateNode("li", null, nodes);
        }

        /// <summary>
        /// Create a node equivalent to a html List Item. HTML Tag <c>li</c>  
        /// </summary>
        /// <param name="content">content</param>
        /// <returns><see cref="Node"/> created</returns>
        /// <exception cref="ArgumentNullException">when <c>content</c> is not informed</exception>
        public static Node CreateListItem(string content)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(content, nameof(content));

            return Node.CreateNode("li", null, Node.CreateTextNode(content));
        }

        /// <summary>
        /// Create a node equivalent to a html Aside. HTML Tag <c>aside</c>  
        /// </summary>
        /// <param name="nodes">A single or array of <see cref="Node"/></param>
        /// <returns><see cref="Node"/> created</returns>
        public static Node CreateAside(params Node[] nodes)
        {
            return Node.CreateNode("aside", null, nodes);
        }

        /// <summary>
        /// Create a node equivalent to a html Break line. HTML Tag <c>br</c>  
        /// </summary>
        /// <returns><see cref="Node"/> created</returns>
        public static Node CreateBreakLine()
        {
            return Node.CreateNode("br", null);
        }

        /// <summary>
        /// Create a node equivalent to a html Horizontal Rule. HTML Tag <c>hr</c>  
        /// </summary>
        /// <returns></returns>
        public static Node CreateHorizontalRule()
        {
            return Node.CreateNode("hr", null);
        }

    }
}
