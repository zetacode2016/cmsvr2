using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Report.ViewModels;
using CMS2.DataAccess;
using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    public class DistributionController : ReportBaseController
    {
        TrackNTraceContext context = new TrackNTraceContext();
        BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");

            return View();

        }

        [HttpPost]
        public ActionResult Index(DistributionSummaryViewModel model)
        {
            ViewBag.BCOs = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName", model.BranchCorpOfficeId);
            model.DistributionViewModels = new List<DistributionViewModel>();

            UserStore userService = new UserStore();
            EmployeePositionMappingBL employeePositionService = new EmployeePositionMappingBL();
            AreaBL areaService = new AreaBL();
            EmployeeBL employeeService = new EmployeeBL();

            var employees = employeeService.GetAll();
            var areas = areaService.FilterBy(x => x.Cluster.BranchCorpOffice.BranchCorpOfficeId == model.BranchCorpOfficeId).ToList();
            if (areas != null)
            {
                var areaNames = areas.Select(y => y.RevenueUnitName).ToList();
                var distributions =
                   context.distribution2s.Where(
                       x =>
                           (x.dDateTime.Value.Year == model.TransactionDate.Year &&
                            x.dDateTime.Value.Month == model.TransactionDate.Month &&
                            x.dDateTime.Value.Day == model.TransactionDate.Day) &&
                           areaNames.Contains(x.cArea)).OrderBy(x=>x.cArea).ThenBy(x=>x.cDriver).ThenBy(x=>x.cPlateNo).ToList();
                if (distributions != null)
                {
                    string checker = "";
                    string driverName = "";
                    foreach (var item in distributions)
                    {
                        checker = item.cChecker;
                        driverName = item.cDriver;
                        foreach (var _item in employees)
                        {
                            var temp = _item.FirstName.Substring(0, 1).ToUpper() + "." + _item.LastName.ToUpper();
                            if (temp.Equals(item.cChecker))
                            {
                                checker = _item.FullName;
                            }
                            if (temp.Equals(item.cDriver))
                            {
                                driverName = _item.FullName;
                            }
                        }

                        var user = userService.FindByNameAsync(item.cUser).Result;
                        //EmployeePositionMapping userAssignment = employeePositionService.GetByEmployeeDate(user.EmployeeId, item.dDateTime ?? DateTime.Now);
                        
                        if (!model.DistributionViewModels.Exists(x=>x.Area.Equals(item.cArea) && x.Truck.Equals(item.cPlateNo)))
                        {
                            DistributionViewModel vm = new DistributionViewModel();
                            vm.TransactionDate = item.dDateTime ?? new DateTime();
                            vm.Area = item.cArea;
                            vm.Truck = item.cPlateNo;
                            vm.Driver = driverName;
                            vm.Wave = item.cWave;
                            vm.Checker = checker;
                            vm.Username = user.UserName;
                            model.DistributionViewModels.Add(vm);
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public PartialViewResult Shipments(DateTime distDate, string driver, string plateNo, string username, string checker, string marea)
        {
            DistributionShipmentViewModel distributedShipments = new DistributionShipmentViewModel();
            distributedShipments.Shipments = new List<ShipmentViewModel>();

            PackageNumberBL packageNumberService = new PackageNumberBL();
            ShipmentBL shipmentService = new ShipmentBL();
            AreaBL areaService = new AreaBL();
            
            string driverName = ConvertName(driver);
            var distributions =
                context.distribution2s.Where(
                    x =>
                        (x.dDateTime.Value.Year == distDate.Year &&
                         x.dDateTime.Value.Month == distDate.Month && x.dDateTime.Value.Day == distDate.Day) && x.cUser.Equals(username) && x.cPlateNo.Equals(plateNo) && x.cDriver.Equals(driverName)).ToList();

            if (distributions != null)
            {
                var _area = areaService.FilterBy(x => x.RevenueUnitName.Equals(marea)).FirstOrDefault();
                int awbCount = 0;
                int itemCount = 0;
                decimal totalWeight = 0;
                decimal totalAmount = 0;

                distributedShipments.DistributionDate = distDate.ToString("MMM dd, yyyy");
                distributedShipments.ScannedBy = checker;
                distributedShipments.Driver = driver;
                distributedShipments.Truck = plateNo;
                distributedShipments.Area = marea;
                distributedShipments.BranchCorpOffice = "";
                if (_area != null)
                    distributedShipments.BranchCorpOffice = _area.Cluster.BranchCorpOffice.BranchCorpOfficeName;

                List<string> cargoNos = distributions.Select(x => x.cCargo).ToList();
                var packageNumbers = packageNumberService.FilterBy(x => cargoNos.Contains(x.PackageNo)).OrderBy(x => x.ShipmentId).ToList();
                if (packageNumbers != null)
                {
                    foreach (var item in distributions)
                    {
                        if (packageNumbers.Any(x => x.PackageNo.Equals(item.cCargo)))
                        {
                            var _shipment = packageNumbers.FirstOrDefault(x => x.PackageNo.Equals(item.cCargo)).Shipment;
                            var shipment = shipmentService.ComputeCharges(shipmentService.EntityToModel(_shipment));
                            if (distributedShipments.Shipments.Exists(x => x.AirwayBillNo.Equals(_shipment.AirwayBillNo)))
                            {
                                itemCount = itemCount + 1;
                            }
                            else
                            {
                                ShipmentViewModel _model = new ShipmentViewModel();
                                _model.AirwayBillNo = shipment.AirwayBillNo;
                                _model.Weight = shipment.Weight.ToString("N");
                                _model.Quantity = shipment.Quantity.ToString();
                                _model.Amount = shipment.ShipmentTotal.ToString("N");
                                _model.PaymentMode = shipment.PaymentMode.PaymentModeCode;
                                _model.ServiceMode = shipment.ServiceMode.ServiceModeCode;
                                _model.Origin = shipment.OriginCity.CityCode;
                                _model.Destination = shipment.DestinationCity.CityCode;
                                _model.Consignee = shipment.Consignee.FullName;
                                _model.ConsigneeAddress = shipment.Consignee.Address1;
                                _model.Remarks = shipment.Remarks;
                                distributedShipments.Shipments.Add(_model);
                                totalWeight = totalWeight + shipment.Weight;
                                totalAmount = totalAmount + shipment.ShipmentTotal;
                                itemCount = itemCount + 1;
                                awbCount = awbCount + 1;
                            }
                        }
                    }
                }
                
                distributedShipments.AwbCount = awbCount.ToString();
                distributedShipments.TotalWeight = totalWeight.ToString("N");
                distributedShipments.TotalAmount = totalAmount.ToString("N");
                distributedShipments.ItemCount = itemCount.ToString();
            }
            return PartialView("_Shipments", distributedShipments);
        }

        private string ConvertName(string name)
        {
            if (name.IndexOf(',')>0)
            {
                string[] temp = name.Split(',');
                return temp[1].Trim().ToUpper().Substring(0, 1) + "." + temp[0].Trim().ToUpper();
            }
            else
            {
                return name;
            }
        }
    }
}
