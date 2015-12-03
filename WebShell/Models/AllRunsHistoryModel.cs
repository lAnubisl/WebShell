using System.Collections.Generic;

namespace WebShell.Models
{
    public class AllRunsHistoryModel
    {
        public IEnumerable<SingleRunHistoryModel> Items { get; set; }
    }
}