using System.IO;
using System.Web.Hosting;

namespace WebShell
{
    public class ConfigurationService
    {
        public const string RunsHistoryFilePattern = "yyyy.MM.dd HH.mm.ss";

        public string GetExecutablesPath()
        {
            var result = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Executables");
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }

            return result; 
        }

        public string GetRunsHistoryPath()
        {
            var result = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "RunsHistory");
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }

            return result; 
        }

        public string GetRunsHistoryPath(string name)
        {
            var result = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "RunsHistory", name);
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }

            return result;
        }

        public string GetExecutableFilePath(string name)
        {
            return Path.Combine(GetExecutablesPath(), name + ".json");
        }
    }
}