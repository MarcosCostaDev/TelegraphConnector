using System.Text.RegularExpressions;
using TelegraphConnector.Types;

namespace TelegraphConnector.Parses
{
    public class TelegraphHtml
    {
        private static readonly Regex _regex = new("<(?<tag>[^\\s>]+)(?<attributes>\\s[^>]+)?>(?<content>.+?)</\\k<tag>>", RegexOptions.Singleline);
        private static readonly Regex _attributeRegex = new(@"\s(?<key>[^\s=]+)=['""](?<value>.+?)['""]", RegexOptions.Singleline);
        private static readonly Regex _bodyRegex = new("<body>(?<content>.+?)</body>", RegexOptions.Singleline);
        private static readonly Regex _textOutsideTag = new(@"(?<innerTags><([^>]+?)([^>]*?)>(.*?)<\/\1>)", RegexOptions.Singleline); // <([^>]+?)([^>]*?)>(.*?)<\/\1>
        private static readonly string[] _allowedTags = new string[]
        {
            "a", "aside", "b", "blockquote", "br", "code", "em", "figcaption",
            "figure", "h3", "h4", "hr", "i", "iframe", "img", "li", "ol", "p", "pre", "strong", "u", "ul", "video"
        };

        private static string AdjustTitles(string html)
        {
            var result = Regex.Replace(html, "<h[2-6](.*?)>(.*?)</h[2-6]>", "<h4$1>$2</h4>", RegexOptions.Singleline);
            result = Regex.Replace(result, "<h1(.*?)>(.*?)</h1>", "<h3$1>$2</h3>", RegexOptions.Singleline);

            return result;//.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }
        private static string SanitizeHtml(string html)
        {
            html = AdjustTitles(html);
            var acceptable = string.Join("|", _allowedTags);
            var stringPattern = $"<(?!((?:\\/\\s*)?(?:{acceptable})))([^>])+>";
            return Regex.Replace(html, stringPattern, "", RegexOptions.Multiline);
        }
        public static IEnumerable<Node> Parse(string html)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(html, nameof(html));

            var nodes = new List<Node>();
            Match bodyMatch = _bodyRegex.Match(html);
            string bodyContent = bodyMatch.Groups["content"].Value;
            string validContent = SanitizeHtml(string.IsNullOrEmpty(bodyContent) ? html : bodyContent);

            Match match = _regex.Match(validContent);
            while (match.Success)
            {
                string tag = match.Groups["tag"].Value;
                string content = match.Groups["content"].Value;
                string attributes = match.Groups["attributes"].Value;

                if (!_allowedTags.Contains(tag.ToLower()))
                {
                    match = match.NextMatch();
                    continue;
                }

                Dictionary<string, string> attributeDict = new Dictionary<string, string>();

                MatchCollection attributeMatches = _attributeRegex.Matches(attributes);

                foreach (Match attributeMatch in attributeMatches)
                {
                    string key = attributeMatch.Groups["key"].Value;
                    string value = attributeMatch.Groups["value"].Value;
                    attributeDict.Add(key, value);
                }

                var rootTag = Node.CreateNode(tag, attributeDict.ToArray(), null);

                var innerTags = ExtractInnerTags(content);

                rootTag.AddChildren(innerTags.ToArray());

                nodes.Add(rootTag);

                match = match.NextMatch();
            }

            return nodes;

        }

        private static IEnumerable<Node> ExtractInnerTags(string content)
        {

            content = content.Trim();
            var nodes = new List<Node>();


            var matches = _textOutsideTag.Matches(content);
            for (int i = 0; i < matches.Count; i++)
            {
                Match current = matches.ElementAt(i);
                

                if (i > 0)
                {
                    Match previous = matches.ElementAt(i - 1);
                    var initial = previous.Index + previous.Length;
                    var final = current.Index - initial;


                    var insideContent = content.Substring(initial, final);

                    if (!string.IsNullOrEmpty(insideContent.Trim()))
                    {
                        nodes.Add(Node.CreateTextNode(insideContent));
                    }

                }
                else if (current.Index > 0)
                {
                    var insideContent = content.Substring(0, current.Index);
                    if (!string.IsNullOrEmpty(insideContent.Trim()))
                    {
                        nodes.Add(Node.CreateTextNode(insideContent));
                    }
                }

                var innerMatch = _regex.Match(current.Value);
                while (innerMatch.Success)
                {
                    var innerTag = innerMatch.Groups["tag"].Value;
                    var innerContent = innerMatch.Groups["content"].Value;
                    var innerAttributes = innerMatch.Groups["attributes"].Value;

                    if (!_allowedTags.Contains(innerTag.ToLower()))
                    {
                        innerMatch = innerMatch.NextMatch();
                        continue;
                    }

                    Dictionary<string, string> innerAttributeDict = new Dictionary<string, string>();

                    MatchCollection innerAttributeMatches = _attributeRegex.Matches(innerAttributes);

                    foreach (Match innerAttributeMatch in innerAttributeMatches)
                    {
                        string key = innerAttributeMatch.Groups["key"].Value;
                        string value = innerAttributeMatch.Groups["value"].Value;
                        innerAttributeDict.Add(key, value);
                    }

                    var innerRootTag = Node.CreateNode(innerTag, innerAttributeDict.ToArray(), null);

                    var innerTagNodes = ExtractInnerTags(innerContent);

                    innerRootTag.AddChildren(innerTagNodes.ToArray());

                    nodes.Add(innerRootTag);
                    innerMatch = innerMatch.NextMatch();
                }


                if (i == matches.Count - 1)
                {
                    var initial = current.Index + current.Length;
                    var insideContent = content.Substring(initial);
                    if (!string.IsNullOrEmpty(insideContent.Trim()))
                    {
                        nodes.Add(Node.CreateTextNode(insideContent));
                    }
                }
            }

            if (!nodes.Any())
                nodes.Add(Node.CreateTextNode(content));

            return nodes;
        }
    }
}
