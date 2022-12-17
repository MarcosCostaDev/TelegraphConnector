using Markdig.Extensions.Footnotes;
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
        private static readonly Regex _attributeRegex = new Regex(@"\s(?<key>[^\s=]+)=['""](?<value>.+?)['""]", RegexOptions.Singleline);
        private static readonly Regex _bodyRegex = new Regex("<body>(?<content>.+?)</body>", RegexOptions.Singleline);
        private static readonly Regex _beforeAfterTagRegex = new Regex("(?<before>.+?)<[^>]+>(?<after>.+?)", RegexOptions.Singleline);
        private static readonly string[] _supportedTags = new string[]
        {
            "a", "div", "b", "strong", "br", "hr", "code", "em", "figcaption", "figure", "h1", "h2", "h3", "h4", "h5", "h6",
            "hr", "img", "ol", "li", "i", "p", "pre", "s", "strong", "u", "ul", "video"
        };
        public static IEnumerable<Node> Parse(string html, Node rootNode = null)
        {

            var nodes = new List<Node>();
            Match bodyMatch = _bodyRegex.Match(html);
            string bodyContent = bodyMatch.Groups["content"].Value;

            Match match = _regex.Match(string.IsNullOrEmpty(bodyContent) ? html : bodyContent);
            while (match.Success)
            {
                string tag = match.Groups["tag"].Value;
                string content = match.Groups["content"].Value;
                string attributes = match.Groups["attributes"].Value;

                if (!_supportedTags.Contains(tag.ToLower()))
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

            var nodes = new List<Node>();
            Match innerMatch = _regex.Match(content);
            while (innerMatch.Success)
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

                var innerTagNodes = ExtractInnerTags(innerContent);

                innerRootTag.AddChildren(innerTagNodes.ToArray());

                Match matchBeforeAfter = _beforeAfterTagRegex.Match(content);

                if (matchBeforeAfter.Success)
                {
                    var before = matchBeforeAfter.Groups["before"].Value;
                    var after = matchBeforeAfter.Groups["after"].Value;

                    if (!string.IsNullOrEmpty(before?.Trim()))
                    {
                        var textNodeBefore = Node.CreateTextNode(before.Trim());
                        innerRootTag.InsertChildren(0, textNodeBefore);
                    }

                    if (!string.IsNullOrEmpty(after?.Trim()))
                    {
                        var textNodeAfter = Node.CreateTextNode(after);

                        innerRootTag.AddChildren(textNodeAfter);
                    }   
                }

                nodes.Add(innerRootTag);
                innerMatch = innerMatch.NextMatch();
            }
            
            if (!innerMatch.Success && !string.IsNullOrEmpty(content))
            {
                Match matchBeforeAfter = _beforeAfterTagRegex.Match(content);

                if (matchBeforeAfter.Success)
                {
                    var before = matchBeforeAfter.Groups["before"].Value;
                    var after = matchBeforeAfter.Groups["after"].Value;

                    if (!string.IsNullOrEmpty(before?.Trim()))
                    {
                        var textNodeBefore = Node.CreateTextNode(before.Trim());
                        nodes.Insert(0, textNodeBefore);
                    }

                    if (!string.IsNullOrEmpty(after?.Trim()))
                    {
                        var textNodeAfter = Node.CreateTextNode(after);

                        nodes.Add(textNodeAfter);
                    }
                }
                else
                {
                    var textNode = Node.CreateTextNode(content);
                    nodes.Add(textNode);
                }
               
            }



            return nodes;
        }
    }
}
