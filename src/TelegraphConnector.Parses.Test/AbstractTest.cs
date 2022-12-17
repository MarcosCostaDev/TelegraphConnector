using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TelegraphConnector.Parses.Test
{
    public class AbstractTest
    {
        protected string GetTextFromFile(string relativePath)
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            return File.ReadAllText(Path.Combine(dirPath, "ParseFiles", relativePath));
        }
    }
}
