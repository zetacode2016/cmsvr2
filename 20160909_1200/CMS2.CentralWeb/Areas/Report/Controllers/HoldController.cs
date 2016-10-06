using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Report.ViewModels;
using CMS2.DataAccess;
using CMS2.Entities;
using CMS2.Entities.TrackingEntities;
using Microsoft.Ajax.Utilities;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    public class HoldController : ReportBaseController
    {
        TrackNTraceContext context = new TrackNTraceContext();
        UserStore userService = new UserStore();
        EmployeeBL employeeService = new EmployeeBL();
        PackageNumberBL packageNumberService = new PackageNumberBL();
        EmployeePositionMappingBL employeePositionService = new EmployeePositionMappingBL();
        DeliveryBL deliveryService = new DeliveryBL();
        BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
        private BranchSatOfficeBL bsoService = new BranchSatOfficeBL();

        public ActionResult Index()
        {
            ViewBag.Locations = new SelectList(Locations(), "Value", "Text");
            ViewBag.LocationNames = new SelectList(LocationNames(), "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Index(HoldSummaryViewModel viewModel)
        {
            ViewBag.Locations = new SelectList(Locations(), "Value", "Text",viewModel.Location);
            switch (viewModel.Location)
            {
                case "BSO":
                    ViewBag.LocationNames = new SelectList(GetBso().Select(x => new { value = x.RevenueUnitName, text = x.RevenueUnitName }), "Value", "Text", viewModel.LocationName);
                    break;
                case "BCO":
                    ViewBag.LocationNames = new SelectList(GetBco().Select(x => new { value = x.BranchCorpOfficeName, text = x.BranchCorpOfficeName }), "Value", "Text", viewModel.LocationName);
                    break;
            }
            viewModel.HoldShipmentViewModels = new List<HoldShipmentViewModel>();

            string scannedBy = "";
            string endorsedBy = "";
            List<Guid> employeeIds = new List<Guid>();
            List<holdcargo> holdcargos = new List<holdcargo>();
            DateTime tempStart = viewModel.TransactionStart;
            DateTime tempEnd = viewModel.TransactionEnd;
            int awbCount = 0;
            int itemCount = 0;
            do
            {
                switch (viewModel.Location)
                {
                    case "BSO":
                        employeeIds.AddRange(employeePositionService.GetByDateRevenuUnitName(tempStart, viewModel.LocationName).Select(x => x.EmployeeId).ToList());
                        break;
                    case "BCO":
                        employeeIds.AddRange(employeePositionService.GetByDateBcoName(tempStart, viewModel.LocationName).Select(x => x.EmployeeId).ToList());
                        break;
                }

                foreach (var _item in employeeIds)
                {
                    var username = userService.GetAllUsers().FirstOrDefault(x => x.EmployeeId == _item);
                    if (username != null)
                    {
                        holdcargos.AddRange(context.holdcargoes.Where(
                          x =>
                              (x.dDateTime.Value.Year == tempStart.Year && x.dDateTime.Value.Month == tempStart.Month && x.dDateTime.Value.Day == tempStart.Day) && x.cUser.Equals(username.UserName)).DistinctBy(x => x.cCargo).ToList());
                    }
                }

                tempStart = tempStart.AddDays(1);
            }
            while (tempStart.Date <= tempEnd.Date);

            if (holdcargos.Count > 0)
            {
                string awb = "";
                string quantity = "";
                string aging = "";
                foreach (var item in holdcargos.DistinctBy(x => x.cCargo).ToList())
                {
                    var employeeId = userService.FindByName(item.cUser).EmployeeId;
                    if (employeeId != null)
                        scannedBy = employeeService.GetById(employeeId).FullName;
                    foreach (var _item in employeeService.GetAll())
                    {
                        var temp = _item.FirstName.Substring(0, 1).ToUpper() + "." + _item.LastName.ToUpper();
                        if (temp.Equals(item.cReceivedFrom))
                        {
                            endorsedBy = _item.FullName;
                            break;
                        }
                    }
                    var shipment = packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo)).Select(x => x.Shipment).FirstOrDefault();
                    if (shipment != null)
                    {
                        awb = shipment.AirwayBillNo;
                        quantity = shipment.Quantity.ToString();
                        aging = DateTime.Now.Date.Subtract(shipment.DateAccepted.Date).TotalDays.ToString();
                        if (!deliveryService.IsExist(x => x.ShipmentId == shipment.ShipmentId))
                        {
                            if (viewModel.HoldShipmentViewModels.Exists(x => x.AirwayBillNo.Equals(awb)))
                            {
                                itemCount = itemCount + 1;
                            }
                            else
                            {
                                awbCount = awbCount + 1;
                                itemCount = itemCount + 1;
                                HoldShipmentViewModel _model = new HoldShipmentViewModel();
                                _model.AirwayBillNo = awb;
                                _model.Quantity = quantity;
                                _model.TransactionDate = (item.dDateTime ?? DateTime.Now).ToString("MMM dd, yyyy");
                                _model.Aging = aging;
                                _model.Reason = item.cReason;
                                _model.Remarks = item.cRemarks;
                                _model.EndorsedBy = endorsedBy;
                                _model.ScannedBy = scannedBy;
                                viewModel.HoldShipmentViewModels.Add(_model);
                            }
                        }
                    }
                }
                viewModel.AwbCount = awbCount.ToString();
                viewModel.ItemCount = itemCount.ToString();
            }

            return View(viewModel);
        }

        private List<SelectListItem> Locations()
        {
            List<SelectListItem> locations = new List<SelectListItem>();
            locations.Add(new SelectListItem { Text = "", Value = "" });
            locations.Add(new SelectListItem { Text = "BSO", Value = "BSO" });
            locations.Add(new SelectListItem { Text = "BCO", Value = "BCO" });
            return locations;
        }

        private List<SelectListItem> LocationNames()
        {
            List<SelectListItem> locations = new List<SelectListItem>();
            locations.Add(new SelectListItem { Text = "select location", Value = "" });
            return locations;
        }
        
        public ActionResult GetLocationNames(string location)
        {
            switch (location)
            {
                case "BSO":
                    var result = GetBso().Select(x => new { value = x.RevenueUnitName, text = x.RevenueUnitName });
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;
                case "BCO":
                    result = GetBco().Select(x => new { value = x.BranchCorpOfficeName, text = x.BranchCorpOfficeName });
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;

                default:
                    return Json(null, JsonRequestBehavior.AllowGet);
                    break;
            }
        }

        private List<RevenueUnit> GetBso()
        {
            return bsoService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList();
        }

        private List<BranchCorpOffice> GetBco()
        {
            return bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();
        } 

    }
}