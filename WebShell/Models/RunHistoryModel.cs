using System;
using System.Collections.Generic;

namespace WebShell.Models
{
    public class RunHistoryModel
    {
        public string Name { get; set; }
        public ICollection<DateTime> Dates { get; set; }
    }
}