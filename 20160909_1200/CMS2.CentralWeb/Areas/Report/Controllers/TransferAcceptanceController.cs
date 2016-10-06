using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Report.ViewModels;
using CMS2.Common.Constants;
using CMS2.Common.Identity;
using CMS2.DataAccess;
using CMS2.Entities;
using CMS2.Entities.TrackingEntities;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    public class TransferAcceptanceController : ReportBaseController
    {
        private BranchCorpOfficeBL bcoService;
        private TrackNTraceContext trackingContext;
        private UserStore userService;
        private EmployeeBL employeeService;
        private EmployeePositionMappingBL employeePositionService;
        private IdentityUser user;
        private List<Employee> employees;
        List<EmployeePositionMapping> userAssignments;

        public TransferAcceptanceController()
        {
            bcoService = new BranchCorpOfficeBL();
            trackingContext = new TrackNTraceContext();
            userService = new UserStore();
            employeeService = new EmployeeBL();
            employeePositionService = new EmployeePositionMappingBL();
        }

        [HttpGet]
        public ActionResult AreaBcoAcceptance()
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            return View();
        }

        [HttpPost]
        public ActionResult AreaBcoAcceptance(AreaBcoAcceptanceSummaryViewModel model)
        {
            user = new IdentityUser();
            employees = new List<Employee>();

            var bcos = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();
            ViewBag.BCOs = new SelectList(bcos, "BranchCorpOfficeId", "BranchCorpOfficeName", model.BranchCorpOfficeId);
            model.AreaBcoAcceptanceViewModels = new List<AreaBcoAcceptanceViewModel>();

            List<string> usernames = new List<string>();
            userAssignments = employeePositionService.GetByDateBco(model.AcceptanceDate, model.BranchCorpOfficeId);
            if (userAssignments != null)
            {
                var users = userService.GetAllUsers();
                var employeeIds = userAssignments.Select(x => x.EmployeeId).ToList();
                foreach (var _item in employeeIds)
                {
                    if (users.Exists(x => x.EmployeeId == _item))
                    {
                        usernames.Add(users.FirstOrDefault(x => x.EmployeeId == _item).UserName);
                    }
                }
            }
            var _bcoAcceptances = trackingContext.branchacceptances.Where(x => (x.dDateTime.Value.Year == model.AcceptanceDate.Year && x.dDateTime.Value.Month == model.AcceptanceDate.Month && x.dDateTime.Value.Day == model.AcceptanceDate.Day) && usernames.Contains(x.cUser)).OrderBy(x => x.nIdentity).ToList();
            if (_bcoAcceptances != null)
            {
                string employeeName = "";
                string checker = "";
                string driver = "";
                string username = "";
                string area = "";
                employees = employeeService.GetAll();

                foreach (var item in _bcoAcceptances)
                {
                    checker = item.cChecker;
                    driver = item.cDriver;
                    foreach (var employee in employees)
                    {
                        employeeName = (employee.FirstName.Substring(0, 1) + "." + employee.LastName).ToUpper().Trim();
                        if (employeeName.Equals(checker))
                        {
                            checker = employee.FullName;
                        }
                        if (employeeName.Equals(driver))
                        {
                            driver = employee.FullName;
                        }
                    }

                    user = userService.FindByNameAsync(item.cUser).Result;
                    username = employeeService.FilterBy(x => x.EmployeeId == user.EmployeeId).FirstOrDefault().FullName;
                    userAssignments = employeePositionService.GetByDateBco(item.dDateTime ?? DateTime.Now, model.BranchCorpOfficeId).Where(x => x.EmployeeId == user.EmployeeId).ToList();
                    if (userAssignments != null)
                    {
                        var _area = userAssignments.FirstOrDefault();
                        if (_area != null)
                        { area = _area.AssignedLocation.RevenueUnitName; }
                    }

                    if (model.AreaBcoAcceptanceViewModels.Exists(x => x.Area.Equals(area) && x.Driver.Equals(driver)))
                    {
                    }
                    else
                    {
                        AreaBcoAcceptanceViewModel vm = new AreaBcoAcceptanceViewModel();
                        vm.AcceptanceDate = item.dDateTime ?? DateTime.Now;
                        vm.Area = area;
                        vm.ScannedBy = username;
                        vm.Driver = driver;
                        vm.Checker = checker;
                        vm.CheckerShorcut = item.cChecker;
                        vm.Truck = item.cPlateNo;
                        vm.AcceptanceType = AcceptanceTypeConstant.AreaToBco;
                        model.AreaBcoAcceptanceViewModels.Add(vm);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult BcoGatewayAcceptance()
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            ViewBag.Gateways = new SelectList(GetGateway(), "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult BcoGatewayAcceptance(BcoGatewayAcceptanceSummaryViewModel model)
        {
            user = new IdentityUser();
            employees = new List<Employee>();

            var bcos = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();
            ViewBag.BCOs = new SelectList(bcos, "BranchCorpOfficeId", "BranchCorpOfficeName");
            ViewBag.Gateways = new SelectList(GetGateway(), "Value", "Text",model.Gateway);
            model.BcoGatewayAcceptanceViewModels = new List<BcoGatewayAcceptanceViewModel>();
            List<gatewayacceptance> acceptances = new List<gatewayacceptance>();

            var bcoAssignments = employeePositionService.GetByDateBco(model.AcceptanceDate, model.BranchCorpOfficeId).Select(x => x.EmployeeId).ToList();
            var bcoUsers = userService.GetAllUsers().Where(x => bcoAssignments.Contains(x.EmployeeId)).Select(x => x.UserName).ToList();
            var transmittals =
                trackingContext.gatewaytransmittals.Where(
                    x =>
                        (x.dDateTime.Value.Year == model.AcceptanceDate.Year &&
                         x.dDateTime.Value.Month == model.AcceptanceDate.Month &&
                         x.dDateTime.Value.Day == model.AcceptanceDate.Day) && x.cGateway.Equals(model.Gateway) && bcoUsers.Contains(x.cUser)).ToList();
            acceptances = trackingContext.gatewayacceptances.Where(x =>
                (x.dDateTime.Value.Year == model.AcceptanceDate.Year &&
                 x.dDateTime.Value.Month == model.AcceptanceDate.Month &&
                 x.dDateTime.Value.Day == model.AcceptanceDate.Day) && x.cGateway.Equals(model.Gateway) &&
                bcoUsers.Contains(x.cUser)).ToList();
            if (acceptances != null)
            {
                foreach (var item in acceptances)
                {
                    if (model.BcoGatewayAcceptanceViewModels.Exists(x => x.Gateway.Equals(item.cGateway) && x.Driver.Equals(item.cDriver)))
                    {
                    }
                    else
                    {
                        BcoGatewayAcceptanceViewModel vm = new BcoGatewayAcceptanceViewModel();
                        vm.AcceptanceDate = item.dDateTime ?? new DateTime();
                        vm.Gateway = item.cGateway;
                        vm.Driver = item.cDriver;
                        vm.Truck = "";
                        if (transmittals.Exists(x => x.cDriver.Equals(item.cDriver) && x.cGateway.Equals(item.cGateway)))
                        {
                            vm.Truck = transmittals.FirstOrDefault((x => x.cDriver.Equals(item.cDriver) && x.cGateway.Equals(item.cGateway))).cPlateNo;
                        }
                        vm.ScannedBy = userService.FindByName(item.cUser).Employee.FullName;
                        vm.AcceptanceType = AcceptanceTypeConstant.BcoToGateWay;
                        model.BcoGatewayAcceptanceViewModels.Add(vm);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult GatewayBcoAcceptance()
        {
            //ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            //ViewBag.Gateways = new SelectList(gatewayService.FilterActive().OrderBy(x => x.GatewayName).ToList(), "GatewayId", "GatewayName");
            return View();
        }

        [HttpPost]
        public ActionResult GatewayBcoAcceptance(BcoGatewayAcceptanceSummaryViewModel model) // TODO: not yet implemented fully
        {
            //tntService.AcceptanceGatewayBco(model.AcceptanceDate);
            //var bcos = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();
            //ViewBag.BCOs = new SelectList(bcos, "BranchCorpOfficeId", "BranchCorpOfficeName");
            //ViewBag.Gateways = new SelectList(gatewayService.FilterActive().OrderBy(x => x.GatewayName).ToList(), "GatewayId", "GatewayName");
            //model.BcoGatewayAcceptanceViewModels = new List<BcoGatewayAcceptanceViewModel>();

            //TransferAcceptanceBL acceptanceService = new TransferAcceptanceBL();
            //var acceptances = acceptanceService.GetByDateAcceptanceTypeBcoId(model.AcceptanceDate, AcceptanceTypeConstant.GatewayToBco, model.BranchCorpOfficeId, model.GatewayId);
            //if (acceptances != null)
            //{
            //    TruckDriverMappingBL truckDriverService = new TruckDriverMappingBL();
            //    var trucks = truckDriverService.GetByDate(model.AcceptanceDate);
            //    foreach (var item in acceptances)
            //    {
            //        var truck = trucks.Where(x => x.Employee.FullName.Equals(item.Driver.FullName)).Select(x => x.Truck).FirstOrDefault();
            //        BranchCorpOffice bco = (BranchCorpOffice)item.TransferFrom;
            //        Gateway gateway = (Gateway)item.TransferTo;
            //        if (model.BcoGatewayAcceptanceViewModels.Exists(x => x.Truck.Equals(truck.PlateNo) && x.Driver.Equals(item.Driver.FullName)))
            //        {
            //        }
            //        else
            //        {
            //            BcoGatewayAcceptanceViewModel vm = new BcoGatewayAcceptanceViewModel();
            //            vm.AcceptanceDate = item.AcceptanceDate;
            //            //var bco = bcos.FirstOrDefault(x => x.BranchCorpOfficeId == model.BranchCorpOfficeId);
            //            vm.BranchCorpOfficeId = item.TransferFromId;
            //            vm.BranchCorpOffice = bco.BranchCorpOfficeName;
            //            vm.GatewayId = item.TransferToId;
            //            vm.Gateway = gateway.GatewayName;
            //            vm.DriverId = item.DriverId;
            //            vm.Driver = item.Driver.FullName;
            //            vm.ScannedById = item.ScannedById;
            //            vm.ScannedBy = item.ScannedBy.FullName;
            //            vm.TruckId = truck.TruckId;
            //            vm.Truck = truck.PlateNo;
            //            model.BcoGatewayAcceptanceViewModels.Add(vm);
            //        }
            //    }
            //}

            //return View(model);
            return View();
        }

        [HttpGet]
        public PartialViewResult Shipments(DateTime acceptanceDate, string checkershortcut, string truck, string scannedby, string driver, string checker, string gateway, string acceptancetype)
        {
            PackageNumberBL packageNumberService = new PackageNumberBL();
            ReceivedAcceptanceShipmentViewModel receivedShipments = new ReceivedAcceptanceShipmentViewModel();
            receivedShipments.Shipments = new List<ShipmentViewModel>();

            receivedShipments.AcceptanceDate = acceptanceDate.ToString("MMM dd, yyyy");
            receivedShipments.Driver = driver;
            receivedShipments.ScannedBy = scannedby;
            receivedShipments.Truck = truck;
            receivedShipments.Checker = checker;

            List<string> cargoNos = new List<string>();
            switch (acceptancetype)
            {
                case AcceptanceTypeConstant.AreaToBco:
                    var branchacceptances = trackingContext.branchacceptances.Where(x => (x.dDateTime.Value.Year == acceptanceDate.Year && x.dDateTime.Value.Month == acceptanceDate.Month && x.dDateTime.Value.Day == acceptanceDate.Day) && x.cChecker.Equals(checkershortcut) && x.cPlateNo.Equals(truck)).OrderBy(x => x.nIdentity).ToList();
                    foreach (var item in branchacceptances)
                    {
                        cargoNos.Add(item.cCargo);
                    }
                    break;
                case AcceptanceTypeConstant.BcoToGateWay:
                    var gatewayacceptances = trackingContext.gatewayacceptances.Where(x => (x.dDateTime.Value.Year == acceptanceDate.Year && x.dDateTime.Value.Month == acceptanceDate.Month && x.dDateTime.Value.Day == acceptanceDate.Day) && x.cGateway.Equals(gateway) && x.cDriver.Equals(driver)).OrderBy(x => x.nIdentity).ToList();
                    foreach (var item in gatewayacceptances)
                    {
                        cargoNos.Add(item.cCargo);
                    }
                    break;
            }

            List<PackageNumber> packageNumbers = packageNumberService.FilterBy(x => cargoNos.Contains(x.PackageNo)).OrderBy(x => x.ShipmentId).ToList();
            if (packageNumbers != null)
            {
                int itemCount = 0;
                int shipmentCount = 0;
                Guid currentId = new Guid();
                Guid previousId = new Guid();
                foreach (var item in packageNumbers)
                {
                    var shipment = item.Shipment;
                    currentId = shipment.ShipmentId;
                    if (previousId != currentId)
                    {
                        previousId = currentId;
                        ShipmentViewModel _model = new ShipmentViewModel();
                        _model.AirwayBillNo = shipment.AirwayBillNo;
                        _model.Quantity = shipment.Quantity.ToString();
                        _model.Remarks = shipment.Remarks;
                        receivedShipments.Shipments.Add(_model);
                        shipmentCount = shipmentCount + 1;
                        itemCount = itemCount + shipment.Quantity;
                    }
                }
                receivedShipments.AwbCount = shipmentCount.ToString();
                receivedShipments.ItemCount = itemCount.ToString();
            }
            return PartialView("_Shipments", receivedShipments);
        }

        public List<SelectListItem> GetGateway()
        {
            var results = trackingContext.gateways.Where(x => x.cGatewayCode.Equals("gatewaytransmittal")).OrderBy(x => x.cGatewayName).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            if (results != null)
            {
                foreach (var item in results)

                    list.Add(new SelectListItem { Text = item.cGatewayName, Value = item.cGatewayName });
            }

            return list;
        }
    }
}
