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

        public AllRunsHistoryModel GetAllRunsHistoryModel()
        {
            return new AllRunsHistoryModel
            {
                Items =
                    new DirectoryInfo(config.GetRunsHistoryPath())
                        .GetDirectories()
                        .Select(d => GetSingleRunHistoryModel(d.Name))
                        .OrderByDescending(x => x.Dates.First())
            };
        }

        public SingleRunHistoryModel GetSingleRunHistoryModel(string name)
        {
            return new SingleRunHistoryModel
            {
                Name = name,
                Dates = LoadRunsHistory(name)
            };
        }

        public Stream GetRunHistoryDetails(string name, DateTime time)
        {
            return File.Open(
                config.GetRunsHistoryPath(name, time), 
                FileMode.Open, 
                FileAccess.Read, 
                FileShare.ReadWrite);
        }

        private ICollection<DateTime> LoadRunsHistory(string name)
        {
            return Directory
                .GetFiles(config.GetRunsHistoryPath(name))
                .Select(Path.GetFileNameWithoutExtension)
                .Select(f => DateTime.ParseExact(f, ConfigurationService.RunsHistoryFilePattern, CultureInfo.InvariantCulture))
                .OrderByDescending(x => x)
                .ToList();
        }
    }
}