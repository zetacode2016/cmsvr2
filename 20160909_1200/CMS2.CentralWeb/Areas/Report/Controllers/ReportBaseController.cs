using System;
using System.Web.Mvc;
using CMS2.Common;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    [Authorize]
    public class ReportBaseController : Controller
    {
        public string LogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
        public Logs logs = new Logs();

        public ReportBaseController()
        { }
    }
}
