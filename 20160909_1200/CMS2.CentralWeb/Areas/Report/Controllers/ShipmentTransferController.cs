using System;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Report.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using CMS2.DataAccess;
using CMS2.Entities;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    public class ShipmentTransferController : ReportBaseController
    {
        private BranchCorpOfficeBL bcoService;
        private TrackNTraceContext trackingContext;
        List<EmployeePositionMapping> userAssignments;
        private EmployeePositionMappingBL employeePositionService;
        private UserStore userService;
        private EmployeeBL employeeService;

        public ShipmentTransferController()
        {
            trackingContext = new TrackNTraceContext();
            bcoService = new BranchCorpOfficeBL();
            employeePositionService = new EmployeePositionMappingBL();
            userService = new UserStore();
            employeeService = new EmployeeBL();
        }

        [HttpGet]
        public ActionResult BsoBco()
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            return View();
        }

        [HttpPost]
        public ActionResult BsoBco(BsoBcoTransferSummaryViewModel model)
        {
            var bcos = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();
            ViewBag.BCOs = new SelectList(bcos, "BranchCorpOfficeId", "BranchCorpOfficeName", model.BranchCorpOfficeId);
            model.BsoBcoTransferViewModels = new List<BsoBcoTransferViewModel>();

            List<string> userLists = new List<string>();
            List<User> users = new List<User>();
            userAssignments = employeePositionService.GetByDateBco(model.TransactionDate, model.BranchCorpOfficeId);
            if (userAssignments != null)
            {
                var employeeIds = userAssignments.Select(x => x.EmployeeId).ToList();
                users = userService.GetAllUsers();
                foreach (var item in employeeIds)
                {
                    if (users.FirstOrDefault(x => x.EmployeeId == item) != null)
                        userLists.Add(users.FirstOrDefault(x => x.EmployeeId == item).UserName);
                }
                var transfers = trackingContext.transfers.Where(
                        x =>
                            (x.dDateTime.Value.Year == model.TransactionDate.Year &&
                             x.dDateTime.Value.Month == model.TransactionDate.Month &&
                             x.dDateTime.Value.Day == model.TransactionDate.Day) && userLists.Contains(x.cUser) &&
                            x.cTransferTo.Equals("Branch")).OrderBy(x => x.nIdentity).ToList();
                if (transfers != null)
                {
                    var employees = employeeService.GetAll();
                    string transferFrom = "";
                    string driver = "";
                    foreach (var item in transfers)
                    {
                        driver = item.cDriver;
                        foreach (var _item in employees)
                        {
                            if (
                                item.cDriver.Equals(
                                    (_item.FirstName.Substring(0, 1) + "." + _item.LastName).ToUpper().Trim()))
                            {
                                driver = _item.FullName;
                                break;
                            }
                        }
                        var user = users.FirstOrDefault(x => x.UserName.Equals(item.cUser));
                        transferFrom =
                            userAssignments.FirstOrDefault(x => x.EmployeeId == user.EmployeeId)
                                .AssignedLocation.RevenueUnitName;
                        if (model.BsoBcoTransferViewModels.Exists(x => x.Driver.Equals(driver) && x.TransferTo.Equals(item.cBranchAirlines)))
                        {
                        }
                        else
                        {
                            BsoBcoTransferViewModel vm = new BsoBcoTransferViewModel();
                            vm.TransactionDate = item.dDateTime ?? DateTime.Now;
                            vm.TransferFrom = transferFrom;
                            vm.TransferTo = item.cBranchAirlines;
                            vm.Driver = driver;
                            vm.DriverShortcut = item.cDriver;
                            vm.Truck = "no data";
                            vm.ScannedBy = item.cUser;
                            model.BsoBcoTransferViewModels.Add(vm);
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult BcoGateway()
        {
            //GatewayBL gatewayService = new GatewayBL();
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            //ViewBag.Gateways = new SelectList(gatewayService.FilterActive().OrderBy(x => x.GatewayName).ToList(), "GatewayId", "GatewayName");
            return View();
        }

        [HttpGet]
        public PartialViewResult Shipments(DateTime transferDate, string scannedby, string transferTo, string driver)
        {
            PackageNumberBL packageNumberService = new PackageNumberBL();
            ShipmentTransferShipmentViewModel transferShipments = new ShipmentTransferShipmentViewModel();
            transferShipments.Shipments = new List<ShipmentViewModel>();

            var user = userService.FindByName(scannedby);
            string _driver = "";
            var employees = employeeService.GetAll();
            foreach (var _item in employees)
            {
                if (driver.Equals((_item.FirstName.Substring(0, 1) + "." + _item.LastName).ToUpper().Trim()))
                {
                    _driver = _item.FullName;
                    break;
                }
            }
            var assignment = employeePositionService.GetByEmployeeDate(user.Employee.EmployeeId, transferDate);
            transferShipments.TransactionDate = transferDate.ToString("MMM dd, yyyy");
            transferShipments.TransferFrom = assignment.AssignedLocation.RevenueUnitName;
            transferShipments.TransferTo = transferTo;
            transferShipments.Driver = _driver;
            transferShipments.Truck = "no data";
            transferShipments.ScannedBy = employeeService.FilterBy(x => x.EmployeeId == user.Employee.EmployeeId).FirstOrDefault().FullName;
            
            var transfers = trackingContext.transfers.Where(
                        x =>
                            (x.dDateTime.Value.Year == transferDate.Year &&
                             x.dDateTime.Value.Month == transferDate.Month &&
                             x.dDateTime.Value.Day == transferDate.Day) && x.cUser.Equals(scannedby) &&
                            x.cTransferTo.Equals("Branch") && x.cBranchAirlines.Equals(transferTo) && x.cDriver.Equals(driver)).OrderBy(x => x.nIdentity).ToList();

            List<string> cargoNos = new List<string>();
            foreach (var item in transfers)
            {
                cargoNos.Add(item.cCargo);
            }
            List<PackageNumber> packageNumbers = packageNumberService.FilterBy(x => cargoNos.Contains(x.PackageNo)).OrderBy(x => x.ShipmentId).ToList();
            if (packageNumbers != null)
            {
                int itemCount = 0;
                int shipmentCount = 0;
                
                foreach (var item in transfers)
                {
                    if (packageNumbers.Any(x => x.PackageNo.Equals(item.cCargo)))
                    {
                        var _shipment = packageNumbers.FirstOrDefault(x => x.PackageNo.Equals(item.cCargo)).Shipment;
                        if (transferShipments.Shipments.Exists(x => x.AirwayBillNo.Equals(_shipment.AirwayBillNo)))
                        {
                            itemCount = itemCount + 1;
                        }
                        else
                        {
                            ShipmentViewModel _model = new ShipmentViewModel();
                            _model.AirwayBillNo = _shipment.AirwayBillNo;
                            _model.Quantity = _shipment.PackageNumbers.Count().ToString();
                            _model.Remarks = _shipment.Remarks;
                            transferShipments.Shipments.Add(_model);
                            shipmentCount = shipmentCount + 1;
                            itemCount = itemCount + 1;
                        }
                    }
                }
                transferShipments.AwbCount = shipmentCount.ToString();
                transferShipments.ItemCount = itemCount.ToString();
            }
            return PartialView("_Shipments", transferShipments);
        }
    }
}
