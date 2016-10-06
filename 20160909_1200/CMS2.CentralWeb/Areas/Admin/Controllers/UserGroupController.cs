using System.Web.Mvc;

namespace APCargo.Web.Areas.Admin.Controllers
{
    public class UserGroupController : Controller
    {
        // GET: Admin/UserGroup
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