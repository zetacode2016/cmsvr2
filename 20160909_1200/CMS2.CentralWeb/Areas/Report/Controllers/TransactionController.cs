using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.CentralWeb.Areas.Report.ViewModels;
using CMS2.CentralWeb.Models;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    public class TransactionController : ReportBaseController
    {
        MobileTrackingContext context = new MobileTrackingContext();

        public ActionResult Index()
        {
            List<CMS2_Acceptance> models = new List<CMS2_Acceptance>();
            models = context.CMS2_Acceptance.ToList();
            foreach (var item in models)
            {
                int tracks = context.CMS2_Track.Where(x => x.awb.Equals(item.awb)).Count();
                item.Quantity = tracks;
                item.TruckInfo = context.CMS2_TruckInfo.SingleOrDefault(x => x.truckno.Equals("XCS345"));
                item.vatamount = Convert.ToDecimal(item.subtotal) * (Convert.ToDecimal(12) / 100);
            }
            return View(models);
        }

        //[HttpPost]
        //public ActionResult Index(FormCollection form)
        //{
        //    return View();
        //}

        public ActionResult Details(string awbNo)
        {
            ViewBag.AWBNo = awbNo;

            AcceptanceViewModel viewModel = new AcceptanceViewModel();
            List<CMS2_Track> tracks = new List<CMS2_Track>();
            tracks = context.CMS2_Track.Where(x => x.awb.Equals(awbNo)).ToList();
            var acceptance = context.CMS2_Acceptance.SingleOrDefault(x => x.awb.Equals(awbNo));
            acceptance.TruckInfo = context.CMS2_TruckInfo.SingleOrDefault(x => x.truckno.Equals("XCS345"));
            viewModel.Acceptance = acceptance;
            viewModel.Tracks = tracks;

            return View(viewModel);
        }
    }
}