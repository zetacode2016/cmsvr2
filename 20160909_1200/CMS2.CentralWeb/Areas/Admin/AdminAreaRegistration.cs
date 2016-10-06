using System.Web.Mvc;

namespace CMS2.CentralWeb.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminCentral_Default",
                "Admin/{controller}/{action}/{id}",
                new { controller="Admin", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}