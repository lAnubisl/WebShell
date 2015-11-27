using System.Web.Mvc;
using WebShell.Models;

namespace WebShell.Controllers
{
    public class ExecutablesController : Controller
    {
        private readonly ExecutableService service = new ExecutableService();

        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return View(service.LoadExecutables());
        }

        [Route("Executable")]
        [HttpGet]
        public ActionResult Executable(string name)
        {
            var model = service.LoadExecutable(name);
            return View(model ?? new ExecutableModel());
        }

        [Route("Executable")]
        [HttpPost]
        public ActionResult Executable(ExecutableModel model)
        {
            if (ModelState.IsValid)
            {
                service.SaveExecutable(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Route("Run")]
        [HttpGet]
        public ActionResult Run(string name)
        {
            return View(name as object);
        }

        [Route("Delete")]
        [HttpPost]
        public ActionResult Delete(string name)
        {
            service.DeleteExecutable(name);
            return RedirectToAction("Index");
        }
    }
}