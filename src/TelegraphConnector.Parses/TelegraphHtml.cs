using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TelegraphConnector.Types;

namespace TelegraphConnector.Parses
{
    public class TelegraphHtml
    {
        private static readonly Regex _regex = new Regex("<(?<tag>[^\\s>]+)(?<attributes>\\s[^>]+)?>(?<content>.+?)</\\k<tag>>", RegexOptions.Singleline);
        private static readonly Regex _attributeRegex = new Regex("<(?<tag>[^\\s>]+)(?<attributes>\\s[^>]+)?>(?<content>.+?)</\\k<tag>>", RegexOptions.Singleline);
        private static readonly Regex _bodyRegex = new Regex("<body>(?<content>.+?)</body>", RegexOptions.Singleline);

        public static Node Parse(string html, Node rootNode = null)
        {
            rootNode ??= Node.CreateDiv(null);

            Match bodyMatch = _bodyRegex.Match(html);
            string bodyContent = bodyMatch.Groups["content"].Value;

            Match match = _regex.Match(string.IsNullOrEmpty(bodyContent) ? html : bodyContent);
            while (match.Success)
            {
                string tag = match.Groups["tag"].Value;
                string content = match.Groups["content"].Value;
                string attributes = match.Groups["attributes"].Value;

                Dictionary<string, string> attributeDict = new Dictionary<string, string>();

                MatchCollection attributeMatches = _attributeRegex.Matches(attributes);

                foreach (Match attributeMatch in attributeMatches)
                {
                    string key = attributeMatch.Groups["key"].Value;
                    string value = attributeMatch.Groups["value"].Value;
                    attributeDict.Add(key, value);
                }

                var rootTag = Node.CreateNode(tag, attributeDict.ToArray(), null);

               ExtractInnerTags(rootTag, content);

                rootNode.AddChildren(rootTag);

                match = match.NextMatch();
            }

            return rootNode;

        }

        private static Node? ExtractInnerTags(Node rootTag, string content)
        {
            Match innerMatch = _regex.Match(content);
            if (innerMatch.Success)
            {
                string innerTag = innerMatch.Groups["tag"].Value;
                string innerContent = innerMatch.Groups["content"].Value;
                string innerAttributes = innerMatch.Groups["attributes"].Value;


                Dictionary<string, string> innerAttributeDict = new Dictionary<string, string>();

                MatchCollection innerAttributeMatches = _attributeRegex.Matches(innerAttributes);

                foreach (Match innerAttributeMatch in innerAttributeMatches)
                {
                    string key = innerAttributeMatch.Groups["key"].Value;
                    string value = innerAttributeMatch.Groups["value"].Value;
                    innerAttributeDict.Add(key, value);
                }

                var innerRootTag = Node.CreateNode(innerTag, innerAttributeDict.ToArray(), null);

                ExtractInnerTags(innerRootTag, innerContent);                

                return innerRootTag;
            }
            else if(!string.IsNullOrEmpty(content)) {
                var textNode = Node.CreateTextNode(content);
                rootTag.AddChildren(textNode);
            }

            return rootTag;
        }
    }
}
