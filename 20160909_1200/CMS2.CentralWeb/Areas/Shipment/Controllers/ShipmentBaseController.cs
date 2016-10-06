using CMS2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CMS2.CentralWeb.Areas.Shipment.Controllers
{
    [Authorize]
    public class ShipmentBaseController : Controller
    {
        public string LogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
        public Logs logs = new Logs();

        public ShipmentBaseController()
        { }
    }
}
