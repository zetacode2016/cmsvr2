using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Entities.TrackingEntities.Models;

namespace CMS2.CentralWeb.Areas.Tracking.Controllers
{
    public class TrackingController : Controller
    {
        private TrackingBL service = new TrackingBL();

        public ActionResult Insack()
        {
            ViewBag.Branches = new SelectList(service.GetOriginBranches() as List<string>, "BranchName", "BranchName");

            var vm = new BundleQueryViewModel();
            vm.TransactionDate=DateTime.Now.Date;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Insack(BundleQueryViewModel viewModel)
        {
            ViewBag.Branches = ViewBag.Branches = new SelectList(service.GetOriginBranches() as List<string>, "BranchName", "BranchName", viewModel.OriginCity);

            var models = service.GetBundlesByDateOriginBranch(viewModel.TransactionDate, viewModel.OriginCity);
            viewModel.BundleViewModels = models;

            return View(viewModel);
        }

        public ActionResult PrintInsack(string sackNo)
        {
            return View();
        }

        public ActionResult Transmittal()
        {
            ViewBag.Branches = new SelectList(service.GetOriginBranches() as List<string>, "BranchName", "BranchName");
            ViewBag.Airlines = new SelectList(service.GetAirlines(), "cAirlineName", "cAirlineName");
            ViewBag.Status = new SelectList(service.GetStatus(), "cStatusName", "cStatusName");

            var vm = new TransmittalQueryViewModel();
            vm.TransmittalDate = DateTime.Now.Date;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Transmittal(TransmittalQueryViewModel viewModel)
        {
            ViewBag.Branches = ViewBag.Branches = new SelectList(service.GetOriginBranches() as List<string>, "BranchName", "BranchName", viewModel.OriginBranch);
            ViewBag.Airlines = new SelectList(service.GetAirlines(), "cAirlineName", "cAirlineName",viewModel.cAirlineName);
            ViewBag.Status = new SelectList(service.GetStatus(), "cStatusName", "cStatusName",viewModel.cStatusName);

            var models = service.GetTransmittalByDateAirlineOriginStatusMAwb(viewModel.TransmittalDate,
                viewModel.cAirlineName, viewModel.OriginBranch,viewModel.cStatusName, viewModel.MasterAirwayBill);
            viewModel.TransmittalViewModels = models;

            return View(viewModel);
        }

        public ActionResult PrintMasterAwb(string masterAwb)
        {
           

            return View();
        }

        public ActionResult Inbound()
        {
            ViewBag.Branches = new SelectList(service.GetOriginBranches() as List<string>, "BranchName", "BranchName");
            ViewBag.Airlines = new SelectList(service.GetAirlines(), "cAirlineName", "cAirlineName");

            return View();
        }

        [HttpPost]
        public ActionResult Inbount(InboundQueryViewModel viewModel)
        {
            ViewBag.Branches = ViewBag.Branches = new SelectList(service.GetOriginBranches() as List<string>, "BranchName", "BranchName", viewModel.DestinationBranch);
            ViewBag.Airlines = new SelectList(service.GetAirlines(), "cAirlineName", "cAirlineName", viewModel.cAirlineName);

            return View(viewModel);
        }


    }
}