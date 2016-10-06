using System.Web.Mvc;

namespace CMS2.CentralWeb.Areas.CmsApi
{
    public class CmsApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CmsApi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CentralCmsApi_default",
                "CmsApi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}