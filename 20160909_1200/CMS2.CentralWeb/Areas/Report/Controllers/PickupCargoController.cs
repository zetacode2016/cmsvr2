using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Report.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.DataAccess;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    public class PickupCargoController : ReportBaseController
    {
        private ShipmentBL shipmentService;
        private BranchCorpOfficeBL bcoService;
        private TrackNTraceContext trackingContext;
        private EmployeeBL employeeService;

        public PickupCargoController()
        {
            shipmentService = new ShipmentBL();
            bcoService = new BranchCorpOfficeBL();
            trackingContext =new TrackNTraceContext();
            employeeService = new EmployeeBL();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            return View();
        }

        [HttpPost]
        public ActionResult Index(PickupShipmentSummaryViewModel model)
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName",model.BranchCorpOfficeId);
            var shipments = shipmentService.GetPickupCargoByDateBco(model.TransactionDate, model.BranchCorpOfficeId);
            List<ShipmentByDateBcoViewModel> viewModels = new List<ShipmentByDateBcoViewModel>();
            string plateNo = "";
            string driver = "";
            string checker = "";
            if (shipments != null)
            {
                foreach (var item in shipments)
                {
                    //var cargoNos = item.Shipment.PackageNumbers.Select(x => x.PackageNo).ToList();
                    //var branchAcceptances =
                    //    trackingContext.branchacceptances.Where(x => cargoNos.Contains(x.cCargo)).ToList();
                    //if (branchAcceptances != null)
                    //{
                    //    var branchAcceptance = branchAcceptances.FirstOrDefault();
                    //    checker = branchAcceptance.cChecker;
                    //    driver = branchAcceptance.cDriver;
                    //    plateNo = branchAcceptance.cPlateNo;
                    //}
                    //foreach (var employee in employeeService.GetAll())
                    //{
                    //    var employeeName = (employee.FirstName.Substring(0, 1) + "." + employee.LastName).ToUpper().Trim();
                    //    if (employeeName.Equals(checker))
                    //    {
                    //        checker = employee.FullName;
                    //    }
                    //    if (employeeName.Equals(driver))
                    //    {
                    //        driver = employee.FullName;
                    //    }
                    //}
                    if (viewModels.Exists(x => x.AreaId == item.AreaId && x.Truck.Equals(plateNo)))
                    {
                        int shipmentcount = viewModels.Where(x => x.AreaId == item.AreaId && x.FieldRepId == item.Shipment.AcceptedById).FirstOrDefault().ShipmentCount;
                        viewModels.Where(x => x.AreaId == item.AreaId && x.FieldRepId == item.Shipment.AcceptedById).FirstOrDefault().ShipmentCount = shipmentcount + 1;
                    }
                    else
                    {
                        ShipmentByDateBcoViewModel _model = new ShipmentByDateBcoViewModel();
                        _model.PickupDate = item.Shipment.DateAccepted;
                        _model.BranchCorpOfficeId = item.BranchCorpOfficeId;
                        _model.BranchCorpOffice = item.BranchCorpOffice;
                        _model.AreaId = item.AreaId;
                        _model.Area = item.Area;
                        _model.Truck = plateNo;
                        _model.Driver = driver;
                        _model.FieldRepId = item.Shipment.AcceptedById;
                        _model.FieldRep = checker;
                        _model.ShipmentCount = 1;
                        viewModels.Add(_model);
                    }
                }
            }
            model.ShipmentDateBcoSummary = viewModels;
            return View(model); 
        }

        [HttpGet]
        public PartialViewResult Shipments(Guid areaid, Guid checkerid, DateTime pickupDate, Guid bcoid, string checker)
        {
            PickupShipmentViewModel pickupShipment = new PickupShipmentViewModel();
            pickupShipment.Shipments = new List<ShipmentViewModel>();
            var _shipments = shipmentService.GetPickupCargoByDateBco(pickupDate, bcoid).Where(x => x.AreaId == areaid && x.Shipment.AcceptedById == checkerid).ToList();
            if (_shipments != null)
            {
                pickupShipment.Checker = "";
                decimal totalWeight = 0;
                int itemCount = 0;
                var model = _shipments.FirstOrDefault();
                pickupShipment.BranchCorpOffice = model.BranchCorpOffice;
                pickupShipment.TransactionDate = model.Shipment.DateAccepted.ToString("MMM dd, yyyy");
                pickupShipment.Checker = checker;
                pickupShipment.Area = model.Area.RevenueUnitName;
                pickupShipment.Driver = model.Driver.FullName;
                if (model.DriverId == Guid.Empty)
                    pickupShipment.Driver = "no data";
                pickupShipment.AwbCount = _shipments.Count.ToString();
                foreach (var item in _shipments)
                {
                    ShipmentViewModel _model = new ShipmentViewModel();
                    _model.AirwayBillNo = item.Shipment.AirwayBillNo;
                    _model.Weight = item.Shipment.Weight.ToString("N");
                    _model.Quantity = item.Shipment.Quantity.ToString();
                    _model.Shipper = item.Shipment.Shipper.FullName;
                    _model.ShipperAddress = item.Shipment.Shipper.Address1;
                    _model.OriginCity = item.Shipment.OriginCity.CityName;
                    _model.Consignee = item.Shipment.Consignee.FullName;
                    _model.DestinationCity = item.Shipment.DestinationCity.CityName;
                    _model.PickedUpBy = item.Shipment.AcceptedBy.FullName;
                    pickupShipment.Shipments.Add(_model);
                    totalWeight = totalWeight + item.Shipment.Weight;
                    itemCount = itemCount + item.Shipment.Quantity;
                }
                pickupShipment.TotalWeight = totalWeight.ToString("N");
                pickupShipment.ItemCount = itemCount.ToString();
            }
            return PartialView("_Shipments", pickupShipment);
        }
    }
}
