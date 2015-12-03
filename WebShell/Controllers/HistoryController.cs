using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace WebShell.Controllers
{
    public class HistoryController : Controller
    {
        private readonly RunsHistoryService service = new RunsHistoryService();

        [Route("history")]
        public ActionResult AllRunsHistory()
        {
            return View(service.GetAllRunsHistoryModel());
        }

        [Route("history/{name}")]
        public ActionResult SingleRunHistory(string name)
        {
            return View(service.GetSingleRunHistoryModel(name));
        }

        [Route("history/{name}/details/{time}")]
        public ActionResult RunHistoryDetails(string name, string time)
        {
            using (var sr = new StreamReader(service.GetRunHistoryDetails(name, DateTime.ParseExact(time, "yyyy_MM_dd_-_HH_mm_ss", CultureInfo.InvariantCulture))))
            {
                var sb = new StringBuilder();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    sb.AppendFormat(string.Format("<span>{0}</span><br/>", line));
                }

                return View(sb.ToString() as object);
            }
        }
    }
}