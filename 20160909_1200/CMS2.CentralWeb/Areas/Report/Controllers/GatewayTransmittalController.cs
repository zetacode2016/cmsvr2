using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Report.ViewModels;
using CMS2.DataAccess;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    public class GatewayTransmittalController : ReportBaseController
    {
        private TrackNTraceContext trackingContext;
        private BranchCorpOfficeBL bcoService;
        private CommodityTypeBL commodityTypeService;
        private UserStore userService;
        private EmployeePositionMappingBL employeePositionService;

        public GatewayTransmittalController()
        {
            trackingContext = new TrackNTraceContext();
            bcoService = new BranchCorpOfficeBL();
            commodityTypeService = new CommodityTypeBL();
            userService = new UserStore();
            employeePositionService = new EmployeePositionMappingBL();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeName", "BranchCorpOfficeName");
            ViewBag.Gateways = new SelectList(GetGateway(), "Value", "Text");
            ViewBag.Commodities = new SelectList(commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList(), "CommodityTypeName", "CommodityTypeName");

            return View();

        }

        [HttpPost]
        public ActionResult Index(GatewayTransmittalSummaryViewModel model)
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeName", "BranchCorpOfficeName", model.Origin);
            ViewBag.Gateways = new SelectList(GetGateway(), "Value", "Text",model.Gateway);
            ViewBag.Commodities = new SelectList(commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList(), "CommodityTypeName", "CommodityTypeName", model.CommodityType);

            var transmittals =
                trackingContext.gatewaytransmittals.Where(
                    x =>
                        (x.dDateTime.Value.Year == model.TransactionDate.Year &&
                         x.dDateTime.Value.Month == model.TransactionDate.Month &&
                         x.dDateTime.Value.Day == model.TransactionDate.Day) &&
                        x.cGateway.Equals(model.Gateway.Substring(0, 20)) && x.cCommodity.Equals(model.CommodityType.Substring(0, 20))).ToList();
            if (transmittals != null)
            {
                EmployeePositionMappingBL employeePositionService = new EmployeePositionMappingBL();
                UserStore userservice = new UserStore();
                var usernames =
                   employeePositionService.GetByDateBcoName(model.TransactionDate, model.Origin)
                           .Join(userservice.GetAllUsers(), emp => emp.EmployeeId, user => user.EmployeeId, (emp, user) => new { user }).Select(x => x.user.UserName).ToList();
                if (usernames != null)
                {
                    transmittals = transmittals.Where(x => usernames.Contains(x.cUser)).ToList();
                    if (transmittals != null && transmittals.Count > 0)
                    {
                        PackageNumberBL packageNumberService = new PackageNumberBL();
                        model.GatewayTransmittalViewModels = new List<GatewayTransmittalViewModel>();
                        Guid previousShipmentId = new Guid();
                        Guid currentShipmentId = new Guid();
                        foreach (var item in transmittals)
                        {
                            var packageNumber = packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo)).FirstOrDefault();
                            if (packageNumber != null && (packageNumber.ShipmentId != null || packageNumber.ShipmentId == Guid.Empty))
                                currentShipmentId = packageNumber.ShipmentId;
                            else
                                currentShipmentId = new Guid();
                            if (previousShipmentId != currentShipmentId)
                            {
                                previousShipmentId = currentShipmentId;
                                GatewayTransmittalViewModel vm = new GatewayTransmittalViewModel();
                                vm.TransactionDate = item.dDateTime ?? new DateTime();
                                vm.AirwayBillNo = item.cAwb;
                                vm.ScannedBy = item.cUser;
                                vm.Origin = item.cBranch;
                                vm.Destination = item.cDestination;
                                vm.Gateway = item.cGateway;
                                vm.Commodity = item.cCommodity;
                                vm.Driver = item.cDriver;
                                vm.Truck = item.cPlateNo;
                                model.GatewayTransmittalViewModels.Add(vm);
                            }
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public PartialViewResult Shipments(DateTime transmittalDate, string destination, string gateway, string commodity, string driver, string truck, string user, string awbno)
        {
            GatewayTransmittalShipmentViewModel tranmittalShipment = new GatewayTransmittalShipmentViewModel();
            tranmittalShipment.Shipments = new List<ShipmentViewModel>();

            var transmittals =
                trackingContext.gatewaytransmittals.Where(
                    x =>
                        (x.dDateTime.Value.Year == transmittalDate.Year && x.dDateTime.Value.Month == transmittalDate.Month && x.dDateTime.Value.Day == transmittalDate.Day) &&
                        x.cGateway.Equals(gateway) && x.cCommodity.Equals(commodity) && x.cDestination.Equals(destination) && x.cDriver.Equals(driver) && x.cPlateNo.Equals(truck) && x.cUser.Equals(user) && x.cAwb.Equals(awbno)).ToList();
            if (transmittals != null)
            {
                ShipmentBL shipmentservice = new ShipmentBL();
                decimal totalWeight = 0;
                int itemCount = 0;
                string bco = "";
                var model = transmittals.FirstOrDefault();
                var _user = userService.FindByName(model.cUser);
                if (_user != null)
                {
                    var employee = employeePositionService.GetByEmployeeDate(_user.EmployeeId , transmittalDate);
                    if (employee != null)
                    {
                        bco = employee.AssignedLocation.Cluster.BranchCorpOffice.BranchCorpOfficeName;
                    }
                }
                
                tranmittalShipment.BranchCorpOffice = bco;
                tranmittalShipment.TransactionDate = model.dDateTime.GetValueOrDefault().ToString("MMM dd, yyyy");
                tranmittalShipment.ScannedBy = model.cUser;
                tranmittalShipment.Destination = model.cDestination;
                tranmittalShipment.Driver = model.cDriver;
                tranmittalShipment.Truck = model.cPlateNo;
                tranmittalShipment.Gateway = model.cGateway;
                tranmittalShipment.Commodity = model.cCommodity;
                var cargoNos = transmittals.Select(y => y.cCargo).ToList();
                PackageNumberBL packageNumberService = new PackageNumberBL();
                var shipmentIds = packageNumberService.FilterBy(x => cargoNos.Contains(x.PackageNo)).Select(x => x.ShipmentId).ToList();
                var shipments =
                    shipmentservice.FilterBy(x => shipmentIds.Contains(x.ShipmentId));
                tranmittalShipment.AwbCount = shipments.Count().ToString();
                if (shipments != null)
                {
                    foreach (var item in shipments)
                    {
                        ShipmentViewModel _model = new ShipmentViewModel();
                        _model.AirwayBillNo = item.AirwayBillNo;
                        _model.Weight = item.Weight.ToString("N");
                        _model.Quantity = item.Quantity.ToString();
                        _model.PaymentMode = "";
                        if (item.PaymentMode != null)
                        {
                            _model.PaymentMode = item.PaymentMode.PaymentModeCode;
                        }
                        _model.ServiceMode = "";
                        if (item.ServiceMode != null)
                        {
                            _model.ServiceMode = item.ServiceMode.ServiceModeCode;
                        }
                        _model.Shipper = item.Shipper.FullName;
                        _model.Consignee = item.Consignee.FullName;
                        _model.ConsigneeAddress = item.Consignee.Address1;
                        tranmittalShipment.Shipments.Add(_model);
                        totalWeight = totalWeight + item.Weight;
                        itemCount = itemCount + item.PackageNumbers.Count();
                    }
                }
                tranmittalShipment.TotalWeight = totalWeight.ToString("N");
                tranmittalShipment.ItemCount = itemCount.ToString();
            }
            return PartialView("_Shipments", tranmittalShipment);
        }

        public List<SelectListItem> GetGateway()
        {
            var results= trackingContext.gateways.Where(x=>x.cGatewayCode.Equals("gatewaytransmittal")).OrderBy(x=>x.cGatewayName).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            if (results != null)
            {
                foreach(var item in results)
    
                list.Add(new SelectListItem { Text = item.cGatewayName, Value = item.cGatewayName });
            }
            
            return list;
        }

    }
}
