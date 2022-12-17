using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegraphConnector.Types;

namespace TelegraphConnector.Parses.Extensions
{
    public static class PageExtension
    {
        public static Page SetHtmlContent(this Page page, string html)
        {
            var htmlNodes = TelegraphHtml.Parse(html);
            page.SetContent(htmlNodes.ToArray());
            return page;
        }

        public static Page SetMarkdownContent(this Page page, string markdown)
        {
            var mdNodes = TelegraphMarkdown.Parse(markdown);
            page.SetContent(mdNodes.ToArray());
            return page;
        }

        public static Page SetHtmlContentFromFile(this Page page, string htmlFilePath)
        {
            if(!File.Exists(htmlFilePath)) throw new FileNotFoundException();
            var fileText = File.ReadAllText(htmlFilePath);

            var htmlNodes = TelegraphHtml.Parse(fileText);
            page.SetContent(htmlNodes.ToArray());
            return page;
        }


        public static Page SetMarkdownContentFromFile(this Page page, string markdownFilePath)
        {
            if (!File.Exists(markdownFilePath)) throw new FileNotFoundException();
            var fileText = File.ReadAllText(markdownFilePath);

            var htmlNodes = TelegraphMarkdown.Parse(fileText);
            page.SetContent(htmlNodes.ToArray());
            return page;
        }

    }
}
