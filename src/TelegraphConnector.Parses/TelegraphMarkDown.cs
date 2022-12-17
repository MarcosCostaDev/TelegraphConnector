using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegraphConnector.Types;

namespace TelegraphConnector.Parses
{
    public class TelegraphMarkDown
    {
        public static Node Parse(string markdown, Node rootNode = null)
        {
            rootNode ??= Node.CreateDiv(null);

            string html = Markdown.ToHtml(markdown);
            TelegraphHtml.Parse(rootNode, html);

            return rootNode;

        }
    }
}
