using System.Web.Mvc;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class AdminController : AdminBaseController
    {
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