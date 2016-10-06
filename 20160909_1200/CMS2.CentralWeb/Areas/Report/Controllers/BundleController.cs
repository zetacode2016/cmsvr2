using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Report.ViewModels;
using CMS2.DataAccess;
using CMS2.Entities;

namespace CMS2.CentralWeb.Areas.Report.Controllers
{
    public class BundleController : ReportBaseController
    {
        private TrackNTraceContext trackingContext;
        private BranchCorpOfficeBL bcoService;
        private UserStore userService;
        private EmployeePositionMappingBL employeePositionService;
        private PackageNumberBL packageNumberService;
        private CityBL cityService;
        
        public BundleController()
        {
            trackingContext = new TrackNTraceContext();
            bcoService = new BranchCorpOfficeBL();
            userService = new UserStore();
            employeePositionService = new EmployeePositionMappingBL();
            packageNumberService = new PackageNumberBL();
            cityService = new CityBL();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Types = new SelectList(TransactionType(), "Value", "Text");
            ViewBag.Bcos = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            return View();
        }

        [HttpPost]
        public ActionResult Index(BundleSummaryViewModel viewModel)
        {
            ViewBag.Types = new SelectList(TransactionType(), "Value", "Text");
            ViewBag.Bcos = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName", viewModel.BranchCorpOfficeId);

            var cities = cityService.GetAll();
            List<Guid> employeeIds = new List<Guid>();
            List<string> usernames = new List<string>();
            var users=userService.GetAllUsers();
            int awbCount = 0;
            string previousSackNo = "";
            string currentSackNo = "";
            Guid previousShipmentId = new Guid();
            Guid currentShipmentId = new Guid();
            string cityName = "";

            employeeIds =
                        employeePositionService.GetByDateBco(viewModel.TransactionDate, viewModel.BranchCorpOfficeId)
                            .Select(x => x.EmployeeId)
                            .ToList();

            if (users!=null)
            {
                foreach (var item in employeeIds)
                {
                    var username = users.FirstOrDefault(x => x.EmployeeId == item);
                    if (username != null)
                    {
                        usernames.Add(username.UserName);
                    }
                }
            }

            var bundles=trackingContext.bundles.Where(x =>
                                (x.dDateTime.Value.Year == viewModel.TransactionDate.Year && x.dDateTime.Value.Month == viewModel.TransactionDate.Month && x.dDateTime.Value.Day == viewModel.TransactionDate.Day) && usernames.Contains(x.cUser)).OrderBy(x => x.cSackNo).ToList();

            if (bundles!=null)
            {
                foreach (var item in bundles)
                {
                    var city = cities.FirstOrDefault(x => x.CityCode.Equals(item.cDestination));
                    cityName = city != null ? city.CityName : item.cDestination;
                    var packageNumber =
                        packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo)).FirstOrDefault();
                    if (packageNumber != null)
                    {
                        currentShipmentId = packageNumber.ShipmentId;
                        currentSackNo = item.cSackNo;
                        if (previousSackNo.Equals(currentSackNo))
                        {
                            if (previousShipmentId != currentShipmentId)
                            {
                                previousShipmentId = currentShipmentId;
                                var sackNo =
                                    viewModel.BundleViewModels.FirstOrDefault((x => x.BundleNo.Equals(currentSackNo)));
                                sackNo.AwbCount = (Convert.ToInt32(sackNo.AwbCount) + 1).ToString();
                            }
                        }
                        else
                        {
                            previousSackNo = currentSackNo;
                            previousShipmentId = currentShipmentId;
                            awbCount = 1;
                            BundleViewModel vm = new BundleViewModel();
                            vm.AwbCount = awbCount.ToString();
                            vm.BundleNo = item.cSackNo;
                            vm.Username = item.cUser;
                            vm.OriginDestination = cityName;
                            vm.TransactionType = viewModel.TransactionType;
                            vm.TransactionDate = viewModel.TransactionDate;
                            viewModel.BundleViewModels.Add(vm);
                        }
                    }
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public PartialViewResult Shipments(DateTime bundleDate, string sackNo, string username, string destination, string bundletype)
        {
            BundleShipmentViewModel bundleShipments = new BundleShipmentViewModel();
            List<PackageNumber> packagNumbers = new List<PackageNumber>();
            Guid previousShipmentId = new Guid();
            Guid currentShipmentId = new Guid();
            int awbCount = 0;
            var user = userService.FindByName(username);
            var bundles =
                trackingContext.bundles.Where(
                    x =>
                        (x.dDateTime.Value.Year == bundleDate.Year && x.dDateTime.Value.Month == bundleDate.Month && x.dDateTime.Value.Day == bundleDate.Day) && x.cUser.Equals(username) && x.cSackNo.Equals(sackNo)).OrderBy(x=>x.dDateTime).ToList();
            
            if (bundles != null)
            {
                string location = employeePositionService.GetByEmployeeDate(user.EmployeeId, bundleDate).AssignedLocation.RevenueUnitName;
                int itemCount = 0;

                bundleShipments.TransactionDate = bundleDate.ToString("MMM dd, yyyy");
                bundleShipments.ScannedBy = user.Employee.FullName;
                switch (bundletype)
                {
                    case "Bundle":
                        bundleShipments.Origin = location;
                        bundleShipments.Destination = destination;
                        break;
                    case "UnBundle":
                        bundleShipments.Origin = destination;
                        bundleShipments.Destination = location;
                        break;
                }
                bundleShipments.BundleNo = sackNo;
                bundleShipments.TransactionType = bundletype;
                bundleShipments.Discrepancy = "0";

                List<string> cargoNos = bundles.Select(x => x.cCargo).ToList();
                packagNumbers =
                    packageNumberService.FilterBy(x => cargoNos.Contains(x.PackageNo))
                        .OrderBy(x => x.ShipmentId)
                        .ToList();
                if (packagNumbers.Count > 0)
                {
                    foreach (var item in packagNumbers)
                    {
                        currentShipmentId = item.ShipmentId;
                        if (previousShipmentId == currentShipmentId)
                        {
                            var temp =
                                bundleShipments.Shipments.FirstOrDefault(
                                    x => x.AirwayBillNo.Equals(item.Shipment.AirwayBillNo));
                            temp.Quantity = (Convert.ToInt32(temp.Quantity) + 1).ToString();
                            itemCount = itemCount + 1;
                        }
                        else
                        {
                            previousShipmentId = currentShipmentId;
                            ShipmentViewModel _model = new ShipmentViewModel();
                            _model.AirwayBillNo = item.Shipment.AirwayBillNo;
                            _model.Quantity = "1";
                            _model.Remarks = item.Shipment.Remarks;
                            bundleShipments.Shipments.Add(_model);
                            itemCount = itemCount + 1;
                            awbCount = awbCount + 1;
                        }
                    }
                    bundleShipments.AwbCount = awbCount.ToString();
                    bundleShipments.ItemCount = itemCount.ToString();
                }
            }
            return PartialView("_Shipments", bundleShipments);
        }

        private List<SelectListItem> TransactionType()
        {
            List<SelectListItem> types = new List<SelectListItem>();
            types.Add(new SelectListItem { Text = "", Value = "" });
            types.Add(new SelectListItem { Text = "Bundle", Value = "Bundle" });
            types.Add(new SelectListItem { Text = "UnBundle", Value = "UnBundle" });
            return types;
        }
    }
}