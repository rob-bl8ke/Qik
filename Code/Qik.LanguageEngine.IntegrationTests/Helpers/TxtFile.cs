using System.IO;
using System.Reflection;

namespace Qik.LanguageEngine.IntegrationTests.Helpers
{
    public class TxtFile
    {
        
        public static string GetFolder()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Scripts");
        }

        public static string ResolvePath(string fileName)
        {
            return Path.Combine(GetFolder(), fileName);
        }

        public static string ReadText(string fileName)
        {
            
            return File.ReadAllText(ResolvePath(fileName));
        }
    }
}
