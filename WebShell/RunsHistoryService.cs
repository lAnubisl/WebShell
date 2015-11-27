using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WebShell.Models;

namespace WebShell
{
    public class RunsHistoryService
    {
        private readonly ConfigurationService config = new ConfigurationService();

        public IEnumerable<RunHistoryModel> LoadRunsHistory()
        {
            return new DirectoryInfo(config.GetRunsHistoryPath())
                .GetDirectories()
                .Select(d => new RunHistoryModel
                {
                    Name = d.Name,
                    Dates = LoadRunsHistory(d.Name).Take(10).ToList()
                })
                .OrderByDescending(x => x.Dates.First());
        }

        public IEnumerable<DateTime> LoadRunsHistory(string name)
        {
            return Directory
                .GetFiles(config.GetRunsHistoryPath(name))
                .Select(Path.GetFileNameWithoutExtension)
                .Select(f => DateTime.ParseExact(f, ConfigurationService.RunsHistoryFilePattern, CultureInfo.InvariantCulture))
                .OrderByDescending(x => x);
        }
    }
}