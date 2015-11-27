using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WebShell.Models;

namespace WebShell
{
    public class ExecutableService
    {
        private readonly ConfigurationService configurationService = new ConfigurationService();

        public IEnumerable<string> LoadExecutables()
        {
            return Directory
                .GetFiles(configurationService.GetExecutablesPath(), "*.json")
                .Select(Path.GetFileNameWithoutExtension);
        }

        public ExecutableModel LoadExecutable(string name)
        {
            var path = configurationService.GetExecutableFilePath(name);
            if (!File.Exists(path))
            {
                return null;
            }

            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ExecutableModel>(json);
        }

        public void DeleteExecutable(string name)
        {
            var path = configurationService.GetExecutableFilePath(name);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void SaveExecutable(ExecutableModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            File.WriteAllText(configurationService.GetExecutableFilePath(model.Name), json);
        }
    }
}