using System.Web.Mvc;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    public class ReportController : ReportBaseController
    {
        // GET: Report/Report
        public ActionResult Index()
        {
            return View();
        }
    }
}