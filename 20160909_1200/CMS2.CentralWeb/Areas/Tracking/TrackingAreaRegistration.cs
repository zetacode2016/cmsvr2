using System.Web.Mvc;

namespace CMS2.CentralWeb.Areas.Tracking
{
    public class TrackingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Tracking";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Tracking_default",
                "Tracking/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}