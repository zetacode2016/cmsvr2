using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Report.ViewModels;
using CMS2.DataAccess;
using CMS2.Entities;
using CMS2.Entities.TrackingEntities;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    public class GatewayInboundController : ReportBaseController
    {
        TrackNTraceContext trackingContext = new TrackNTraceContext();
        BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
        //GatewayBL gatewayService = new GatewayBL();

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeName", "BranchCorpOfficeName");
            ViewBag.Gateways = new SelectList(GetGateway(), "Value", "Text");

            return View();

        }

        [HttpPost]
        public ActionResult Index(GatewayInboundSummaryViewModel model)
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeName", "BranchCorpOfficeName", model.Origin);
            ViewBag.Gateways = new SelectList(GetGateway(), "Value", "Text",model.Gateway);

            EmployeePositionMappingBL employeePositionService = new EmployeePositionMappingBL();
            UserStore userService = new UserStore();
            var employeeIds = employeePositionService.GetByDateBcoName(model.TransactionDate, model.Origin).Select(x=>x.EmployeeId).ToList();
            var user = userService.GetAllUsers().Where(x=>employeeIds.Contains(x.EmployeeId)).ToList();
            var usernames = user.Select(x => x.UserName).ToList();

            var inbounds =
                trackingContext.inbounds.Where(
                    x =>
                        (x.dDateTime.Value.Year == model.TransactionDate.Year &&
                         x.dDateTime.Value.Month == model.TransactionDate.Month &&
                         x.dDateTime.Value.Day == model.TransactionDate.Day) &&
                        x.cAirline.Equals(model.Gateway.Substring(0, 20))).ToList();
            if (inbounds != null && usernames !=null)
            {
                inbounds = inbounds.Where(x => usernames.Contains(x.cUser)).OrderBy(x=>x.cMawb).ToList();
                if (inbounds != null)
                {
                    PackageNumberBL packageNumberService = new PackageNumberBL();
                    model.GatewayInboundViewModels = new List<GatewayInboundViewModel>();
                    
                    PackageNumber packageNumber = null;
                    string previousMawb = "";
                    string currentMawb = "";
                    gatewayacceptance _gatewayacceptance = null;
                    string originUsername = "";
                    string origin = "";
                    foreach (var item in inbounds)
                    {
                        currentMawb = item.cMawb;
                        packageNumber = packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo)).FirstOrDefault();
                        if (packageNumber != null && packageNumber.ShipmentId != null)
                        {
                            origin = packageNumber.Shipment.OriginCity.CityName;
                        }

                        if (!previousMawb.Equals(currentMawb))
                        {
                            previousMawb = currentMawb;
                            GatewayInboundViewModel vm = new GatewayInboundViewModel();
                            vm.TransactionDate = item.dDateTime ?? new DateTime();
                            vm.ScannedBy = item.cUser;
                            vm.Origin = origin;
                            vm.Gateway = item.cAirline;
                            vm.FlightNo = "no data";
                            vm.ManifestAwb = item.cMawb;
                            model.GatewayInboundViewModels.Add(vm);
                        }
                    }
                }
            }

            return View(model);
        }

        private void GetInbound(GatewayInboundSummaryViewModel model, inbound item, PackageNumber packageNumber, Guid previousShipmentId, Guid currentShipmentId, string driver)
        {
            if (packageNumber != null && packageNumber.ShipmentId != null)
                currentShipmentId = packageNumber.ShipmentId;
            else
                currentShipmentId = new Guid();

            EmployeePositionMappingBL employeeAssignService = new EmployeePositionMappingBL();
            var employeeAssignments = employeeAssignService.GetByDate(item.dDateTime ?? new DateTime());
            string origin = "";
            if (employeeAssignments != null)
            {
                string employeeName = "";
                foreach (var _item in employeeAssignments)
                {
                    employeeName = (_item.Employee.FirstName.Substring(0, 1) + "." + _item.Employee.LastName).ToUpper().Trim();
                    if (employeeName.Equals(driver))
                    {
                        origin = _item.AssignedLocation.City.CityName;
                    }
                }
            }
            if (previousShipmentId != currentShipmentId)
            {
                previousShipmentId = currentShipmentId;
                GatewayInboundViewModel vm = new GatewayInboundViewModel();
                vm.TransactionDate = item.dDateTime ?? new DateTime();
                vm.ScannedBy = item.cUser;
                vm.Origin = origin;
                vm.Gateway = item.cAirline;
                vm.FlightNo = "no data";
                vm.ManifestAwb = item.cMawb;
                model.GatewayInboundViewModels.Add(vm);
            }
        }

        [HttpGet]
        public PartialViewResult Shipments(string mawb, string origin)
        {
            GatewayInboundShipmentViewModel inboundShipment = new GatewayInboundShipmentViewModel();
            inboundShipment.Shipments = new List<ShipmentViewModel>();
            UserStore userService = new UserStore();
            EmployeeBL employeeService = new EmployeeBL();
            var inbounds =
                trackingContext.inbounds.Where(x => x.cMawb.Equals(mawb)).ToList();
            if (inbounds != null)
            {
                gatewayacceptance _gatewayacceptance = null;
                string driver = "";
                int awbCount = 0;
                int itemCount = 0;
                var model = inbounds.FirstOrDefault();
                var user = userService.FindByNameAsync(model.cUser).Result;
                
                inboundShipment.ManifestAwb = mawb;
                inboundShipment.OriginCity = origin;
                inboundShipment.TransactionDate = model.dDateTime.GetValueOrDefault().ToString("MMM dd, yyyy");
                inboundShipment.ScannedBy = employeeService.GetById(user.EmployeeId).FullName;
                inboundShipment.FlightNo = "no data";
                inboundShipment.Gateway = model.cAirline;

                var cargoNos = inbounds.Select(x => x.cCargo).ToList();
                PackageNumberBL packageNumberService = new PackageNumberBL();
                var packageNumbers = packageNumberService.FilterBy(x => cargoNos.Contains(x.PackageNo)).OrderBy(x=>x.ShipmentId).ToList();
                if (packageNumbers != null)
                {
                    foreach (var item in inbounds)
                    {
                        if (packageNumbers.Any(x => x.PackageNo.Equals(item.cCargo)))
                        {
                            var _shipment = packageNumbers.FirstOrDefault(x => x.PackageNo.Equals(item.cCargo)).Shipment;
                            if (inboundShipment.Shipments.Exists(x => x.AirwayBillNo.Equals(_shipment.AirwayBillNo)))
                            {
                                itemCount = itemCount + 1;
                            }
                            else
                            {
                                ShipmentViewModel _model = new ShipmentViewModel();
                                _model.AirwayBillNo = _shipment.AirwayBillNo;
                                _model.Quantity = _shipment.Quantity.ToString();
                                _model.Remarks = _shipment.Remarks;
                                inboundShipment.Shipments.Add(_model);
                                itemCount = itemCount + 1;
                                awbCount = awbCount + 1;
                            }
                        }
                    }
                }
                inboundShipment.AwbCount = awbCount.ToString();
                inboundShipment.ItemCount = itemCount.ToString();
            }
            return PartialView("_Shipments", inboundShipment);
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
