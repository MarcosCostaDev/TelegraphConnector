using Markdig;
using TelegraphConnector.Types;

namespace TelegraphConnector.Parses
{
    public class TelegraphMarkdown
    {
        public static IEnumerable<Node> Parse(string markdown)
        {
            string html = Markdown.ToHtml(markdown);
            return TelegraphHtml.Parse(html);
        }
    }
}
