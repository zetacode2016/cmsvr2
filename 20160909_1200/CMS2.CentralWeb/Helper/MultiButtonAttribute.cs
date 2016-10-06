using System;
using System.Reflection;
using System.Web.Mvc;

namespace CMS2.CentralWeb.Helper
{
    /// <summary>
    /// This class facilitates multiple submit button
    /// in the same form. Each button is independent of
    /// each other and calls its own Action Method. 
    /// </summary>
    public class MultiButtonAttribute : ActionNameSelectorAttribute
    {
        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
                return true;

            if (!actionName.Equals("Action", StringComparison.InvariantCultureIgnoreCase))
                return false;

            var request = controllerContext.RequestContext.HttpContext.Request;
            return request[methodInfo.Name] != null;
        }
    }
}