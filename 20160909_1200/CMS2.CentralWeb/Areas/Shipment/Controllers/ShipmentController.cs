using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;

namespace CMS2.CentralWeb.Areas.Shipment.Controllers
{
    public class ShipmentController : ShipmentBaseController
    {
        private ShipmentBL service = new ShipmentBL();
        public ActionResult Index()
        {
            var shipments = service.GetShipments();
            return View(shipments);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            return View();
        }
    }
}