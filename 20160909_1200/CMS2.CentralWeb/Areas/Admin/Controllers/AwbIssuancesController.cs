using System.Web.Mvc;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class AwbIssuancesController : AdminBaseController
    {
        // GET: Admin/AwbIssuances
        public ActionResult Index()
        {
            //if (Session["User"] == null)
            //{
            //    return RedirectToActionPermanent("LogIn", "User");
            //}
            //else
            //{
            //    return View();
            //}
            return View();
        }
    }
}