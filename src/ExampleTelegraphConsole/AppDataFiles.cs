using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTelegraphConsole
{
    internal class AppDataFiles
    {
        private static string GetAppDataFolderPath()
        {

            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            var folderPath = Path.Combine(dirPath, "AppData");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }

        internal static void CreateFile(string filename, string fileText)
        {
            var folderPath = GetAppDataFolderPath();
            var filePath = Path.Combine(folderPath, filename);

            if (File.Exists(filePath)) File.Delete(filePath);

            File.WriteAllText(filePath, fileText, Encoding.UTF8);
        }

        internal static string ReadFileText(string filename)
        {
            var folderPath = GetAppDataFolderPath();
            var filePath = Path.Combine(folderPath, filename);

            if(!File.Exists(filePath)) return string.Empty;

            return File.ReadAllText(filePath);
        }
    }

}
