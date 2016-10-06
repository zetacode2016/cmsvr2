using System.Web.Mvc;

namespace CMS2.CentralWeb.Areas.Report
{
    public class ReportAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Report";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CentralReport_default",
                "Report/{controller}/{action}/{id}",
                new { controller="Report", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}