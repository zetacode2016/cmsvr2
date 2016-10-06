using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class ShipmentController : AdminBaseController
    {
        private ShipmentBL service = new ShipmentBL();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var shipment = service.ComputeCharges(service.EntityToModel(service.GetById(id)));
            return View(shipment);
        }
    }
}