using System.Web.Mvc;
using CMS2.BusinessLogic;

namespace CMS2.CentralWeb.Areas.Shipment.Controllers
{
    public class BookingController : ShipmentBaseController
    {
        BookingBL service = new BookingBL();

        public ActionResult Index()
        {
            var bookings = service.FilterActive();
            return View(bookings);
        }
    }
}