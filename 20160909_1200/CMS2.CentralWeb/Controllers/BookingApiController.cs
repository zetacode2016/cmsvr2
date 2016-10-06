using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APCargo.BusinessLogic;
using APCargo.Web.ViewModels;

namespace APCargo.Web.Controllers
{
    public class BookingApiController : ApiController
    {
        BookingBL bookingService = new BookingBL();

        [HttpGet]
        public HttpResponseMessage GetBookings()
        {
            List<BookingApiViewModel> vm = new List<BookingApiViewModel>();
            var entities = bookingService.GetAll();
            foreach (var item in entities)
            {
                BookingApiViewModel newVm = new BookingApiViewModel();
                newVm.BookingId = item.BookingId;
                vm.Add(newVm);
            }
            return Request.CreateResponse(HttpStatusCode.OK, vm);
        }
    }
}
