using System.Web.Mvc;

namespace WebShell.Controllers
{
    public class HistoryController : Controller
    {
        private readonly RunsHistoryService service = new RunsHistoryService();

        [Route("history")]
        public ActionResult Index()
        {
            return View(service.LoadRunsHistory());
        }
    }
}