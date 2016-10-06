using CMS2.CentralWeb.Areas.Shipment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.DataAccess;
using CMS2.BusinessLogic;
using CMS2.Common.Constants;
using CMS2.Entities;
using CMS2.Entities.TrackingEntities;

namespace CMS2.CentralWeb.Areas.Shipment.Controllers
{
    public class TrackingController : ShipmentBaseController
    {
        private AreaBL areaService;
        private BranchSatOfficeBL bsoService;
        private GatewaySatOfficeBL gatewayService;
        private BranchCorpOfficeBL bcoService;
        private TrackNTraceContext tntContext;
        private EmployeePositionMappingBL employeeMappingService;
        private ShipmentBL shipmentService;
        private EmployeeBL employeeService;
        private DeliveryBL deliveryService;
        private UserStore userService;

        public TrackingController()
        {
            areaService = new AreaBL();
            bsoService = new BranchSatOfficeBL();
            gatewayService = new GatewaySatOfficeBL();
            bcoService = new BranchCorpOfficeBL();
            tntContext = new TrackNTraceContext();
            employeeMappingService = new EmployeePositionMappingBL();
            shipmentService = new ShipmentBL();
            employeeService = new EmployeeBL();
            deliveryService = new DeliveryBL();
            userService = new UserStore();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TrackingQueryViewModel model)
        {
            model.TrackingViewModels = new List<TrackingViewModel>();

            List<string> cargoNos = new List<string>();
            Guid shipmentId = new Guid();
            TrackingViewModel trackingVM = null;
            DateTime trackDate = new DateTime();
            UserStore userService = new UserStore();
            string city = "";
            User user = new User();
            List<branchacceptance> acceptancePackages = new List<branchacceptance>();
            branchacceptance acceptance = new branchacceptance();
            EmployeePositionMapping mapping = new EmployeePositionMapping();

            var shipments = shipmentService.FilterBy(x => x.AirwayBillNo.Equals(model.AirwayBillNo));
            if (shipments != null && shipments.Count > 0)
            {
                #region Received
                Entities.Shipment shipment = shipments.FirstOrDefault();
                shipmentId = shipment.ShipmentId;
                cargoNos = shipment.PackageNumbers.Select(x => x.PackageNo).ToList();
                //cargoIds = shipment.PackageNumbers.Select(x => x.PackageNumberId).ToList();
                trackDate = shipment.DateAccepted;
                user = userService.GetAllUsers().FirstOrDefault(x => x.EmployeeId == shipment.AcceptedById);
                mapping = employeeMappingService.GetByEmployeeDate(user.EmployeeId, trackDate);
                if (mapping != null)
                    city = mapping.AssignedLocation.City.CityName;
                trackingVM = new TrackingViewModel()
                {
                    Weekday = trackDate.DayOfWeek.ToString(),
                    Date = trackDate.Date.ToString("dd/MM/yyyy"),
                    Time = trackDate.ToString("hh:mm tt"),
                    Status = "Shipment Picked-up/Received",
                    Location = city
                };
                model.TrackingViewModels.Add(trackingVM);
                #endregion

                //#region BranchAcceptanceFromArea
                //acceptancePackages = tntContext.branchacceptances.Where(x => cargoNos.Contains(x.cCargo)).ToList();
                //if (acceptancePackages != null && acceptancePackages.Count > 0)
                //{
                //    city = "";
                //    acceptance = acceptancePackages.OrderBy(x => x.dDateTime).FirstOrDefault();
                //    trackDate = acceptance.dDateTime ?? DateTime.Now;
                //    user = userService.GetAllUsers().FirstOrDefault(x => x.UserName.Equals(acceptance.cUser));
                //    if (user != null)
                //    {
                //        mapping = employeeMappingService.GetByEmployeeDate(user.EmployeeId, trackDate);
                //        if (mapping != null)
                //            city = mapping.AssignedLocation.City.CityName;
                //    }
                //    trackingVM = new TrackingViewModel()
                //    {
                //        Weekday = trackDate.DayOfWeek.ToString(),
                //        Date = trackDate.Date.ToString("dd/MM/yyyy"),
                //        Time = trackDate.ToString("hh:mm tt"),
                //        Status = "Arrived at APCargo Hub",
                //        Location = city
                //    };
                //    model.TrackingViewModels.Add(trackingVM);
                //}
                //#endregion

                //#region Transmittals
                //var transmittals =
                //    tntContext.gatewaytransmittals.Where(x => cargoNos.Contains(x.cCargo)).ToList();
                //if (transmittals != null && transmittals.Count > 0)
                //{
                //    city = "";
                //    gatewaytransmittal transmittal = transmittals.FirstOrDefault();
                //    trackDate = transmittal.dDateTime ?? new DateTime();
                //    user = userService.GetAllUsers().FirstOrDefault(x => x.UserName.Equals(transmittal.cUser));
                //    if (user != null)
                //    {
                //        mapping = employeeMappingService.GetByEmployeeDate(user.EmployeeId, trackDate);
                //        if (mapping != null)
                //            city = mapping.AssignedLocation.City.CityName;
                //    }
                //    trackingVM = new TrackingViewModel()
                //    {
                //        Weekday = trackDate.DayOfWeek.ToString(),
                //        Date = trackDate.Date.ToString("dd/MM/yyyy"),
                //        Time = trackDate.ToString("hh:mm tt"),
                //        Status = "Departed APCargo Facility",
                //        Location = city
                //    };
                //    model.TrackingViewModels.Add(trackingVM);
                //}
                //#endregion

                //#region Inbounds
                //var inbounds = tntContext.inbounds.Where(x => cargoNos.Contains(x.cCargo)).ToList();
                //if (inbounds != null && inbounds.Count > 0)
                //{
                //    city = "";
                //    inbound inbound = inbounds.FirstOrDefault();
                //    trackDate = inbound.dDateTime ?? new DateTime();
                //    user = userService.GetAllUsers().FirstOrDefault(x => x.UserName.Equals(inbound.cUser));
                //    if (user != null)
                //    {
                //        mapping = employeeMappingService.GetByEmployeeDate(user.EmployeeId,
                //         trackDate);
                //        if (mapping != null)
                //            city = mapping.AssignedLocation.City.CityName;
                //    }
                //    trackingVM = new TrackingViewModel()
                //    {
                //        Weekday = trackDate.DayOfWeek.ToString(),
                //        Date = trackDate.Date.ToString("dd/MM/yyyy"),
                //        Time = trackDate.ToString("hh:mm tt"),
                //        Status = "Arrived At Destination Gateway",
                //        Location = city
                //    };
                //    model.TrackingViewModels.Add(trackingVM);
                //}
                //#endregion

                //#region BranchAcceptanceFromGateway
                //if (acceptancePackages != null && acceptancePackages.Count > 0)
                //{
                //    city = "";
                //    acceptance = acceptancePackages.OrderByDescending(x => x.dDateTime).FirstOrDefault();
                //    if (acceptance != null)
                //    {
                //        trackDate = acceptance.dDateTime ?? DateTime.Now;
                //        user = userService.GetAllUsers().FirstOrDefault(x => x.UserName.Equals(acceptance.cUser));
                //        if (user != null)
                //        {
                //            mapping = employeeMappingService.GetByEmployeeDate(
                //             user.EmployeeId, trackDate);
                //            if (mapping != null)
                //                city = mapping.AssignedLocation.City.CityName;
                //        }
                //        trackingVM = new TrackingViewModel()
                //        {
                //            Weekday = trackDate.DayOfWeek.ToString(),
                //            Date = trackDate.Date.ToString("dd/MM/yyyy"),
                //            Time = trackDate.ToString("hh:mm tt"),
                //            Status = "Arrived at APCargo Facility",
                //            Location = city
                //        };
                //        model.TrackingViewModels.Add(trackingVM);
                //    }
                //}
                //#endregion

                //#region Distribution
                //var distributions = tntContext.distribution2s.Where(x => cargoNos.Contains(x.cCargo)).ToList();
                //if (distributions != null && distributions.Count > 0)
                //{
                //    city = "";
                //    distribution2 distribution = distributions.FirstOrDefault();
                //    trackDate = distribution.dDateTime ?? new DateTime();
                //    user =
                //        userService.GetAllUsers()
                //            .FirstOrDefault(x => x.UserName.Equals(distribution.cUser));
                //    if (user != null)
                //    {
                //        mapping = employeeMappingService.GetByEmployeeDate(
                //          user.EmployeeId, trackDate);
                //        if (mapping != null)
                //            city = mapping.AssignedLocation.City.CityName;
                //    }
                //    trackingVM = new TrackingViewModel()
                //    {
                //        Weekday = trackDate.DayOfWeek.ToString(),
                //        Date = trackDate.Date.ToString("dd/MM/yyyy"),
                //        Time = trackDate.ToString("hh:mm tt"),
                //        Status = "Out For Delivery",
                //        Location = city
                //    };
                //    model.TrackingViewModels.Add(trackingVM);
                //}
                //#endregion

                #region Delivery
                var deliveries = deliveryService.FilterBy(x => x.ShipmentId == shipmentId && x.DeliveryStatus.DeliveryStatusName.Equals("Delivered"));
                if (deliveries != null && deliveries.Count > 0)
                {
                    city = "";
                    var delivery = deliveries.FirstOrDefault();
                    trackDate = delivery.DateDelivered;
                    user =
                        userService.GetAllUsers()
                            .FirstOrDefault(x => x.EmployeeId == delivery.DeliveredById);
                    if (user != null)
                    {
                        mapping =
                           employeeMappingService.GetByEmployeeDateCityName(
                               user.EmployeeId, trackDate,
                               delivery.Shipment.DestinationCity.CityName);
                        if (mapping != null)
                            city = mapping.AssignedLocation.City.CityName;
                    }
                    trackingVM = new TrackingViewModel()
                    {
                        Weekday = trackDate.DayOfWeek.ToString(),
                        Date = trackDate.Date.ToString("dd/MM/yyyy"),
                        Time = trackDate.ToString("hh:mm tt"),
                        Status = "Delivered",
                        Location = city
                    };
                    model.TrackingViewModels.Add(trackingVM);
                }
                #endregion
            }

            return View(model);
        }

        public ActionResult Detailed()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detailed(DetailedTrackingQueryViewModel model)
        {
            model.DetailedTrackingViewModels = new List<DetailedTrackingViewModel>();

            List<string> cargoNos = new List<string>();
            Guid shipmentId = new Guid();
            DetailedTrackingViewModel trackingVM = null;
            DateTime trackDate = new DateTime();
            DateTime distributionDate = new DateTime();
            DateTime deliveryDate = new DateTime();

            string location = "";
            User user = new User();
            List<branchacceptance> branchacceptances = new List<branchacceptance>();
            branchacceptance acceptance = new branchacceptance();
            EmployeePositionMapping mapping = new EmployeePositionMapping();
            List<transfer> transfers = new List<transfer>();
            List<holdcargo> holdcargoes = new List<holdcargo>();
            List<bundle> bundles = new List<bundle>();
            string previousAwb = "";

            var shipments = shipmentService.FilterBy(x => x.AirwayBillNo.Equals(model.AirwayBillNo));
            if (shipments != null && shipments.Count > 0)
            {
                Entities.Shipment shipment = shipments.FirstOrDefault();
                shipmentId = shipment.ShipmentId;
                model.Shipper = shipment.Shipper.FullName;
                model.Consignee = shipment.Consignee.FullName;
                model.PaymentMode = shipment.PaymentMode.PaymentModeName;
                model.Commodity = shipment.CommodityType.CommodityTypeName;
                model.Origin = shipment.OriginCity.CityName;
                model.Destination = shipment.DestinationCity.CityName;
                cargoNos = shipment.PackageNumbers.Select(x => x.PackageNo).ToList();
                model.ItemCount = cargoNos.Count.ToString();
                cargoNos = shipment.PackageNumbers.Select(x => x.PackageNo).ToList();
                
                #region Received
                location = "";
                trackDate = shipment.DateAccepted;
                trackingVM = new DetailedTrackingViewModel();
                trackingVM.TransactionDate = trackDate;
                trackingVM.Status = "Shipment Picked-up/Received";
                trackingVM.Column1 = cargoNos.Count.ToString();
                user = userService.GetAllUsers().FirstOrDefault(x => x.EmployeeId == shipment.AcceptedById);
                if (user != null)
                {
                    trackingVM.ScannedBy = user.Employee.FullName;
                    mapping = employeeMappingService.GetByEmployeeDate(user.EmployeeId, trackDate);
                    if (mapping != null)
                    {
                        GetLocation(ref mapping, ref location);
                        trackingVM.Location = location;
                    }
                }
                trackingVM.Column2 = "";
                trackingVM.Column3 = "";
                trackingVM.Column4 = "";
                model.DetailedTrackingViewModels.Add(trackingVM);
                #endregion

                #region Delivery
                var deliveries = deliveryService.FilterBy(x => x.ShipmentId == shipmentId).OrderBy(x => x.DateDelivered).ToList();
                if (deliveries != null && deliveries.Count > 0)
                {
                    foreach(var delivery in deliveries)
                    {
                        if (delivery.DeliveryStatus.DeliveryStatusName.Equals("Delivered"))
                        {
                            #region Delivered
                            location = "";
                            deliveryDate = delivery.DateDelivered;
                            trackingVM = new DetailedTrackingViewModel();
                            trackingVM.TransactionDate = deliveryDate;
                            trackingVM.Status = "Delivered";
                            user = userService.GetAllUsers().FirstOrDefault(x => x.EmployeeId == delivery.DeliveredById);
                            if (user != null)
                            {
                                trackingVM.ScannedBy = user.Employee.FullName;
                                mapping = employeeMappingService.GetByEmployeeDateCityName(user.EmployeeId, deliveryDate, delivery.Shipment.DestinationCity.CityName);
                                if (mapping != null)
                                {
                                    GetLocation(ref mapping, ref location);
                                    trackingVM.Location = location;
                                }
                            }
                            trackingVM.Column1 = "";
                            trackingVM.Column2 = "";
                            if (delivery.DeliveryReceipts != null)
                            {
                                var receipt =
                                    delivery.DeliveryReceipts.FirstOrDefault(x => x.DeliveryId == delivery.DeliveryId);
                                if (receipt!=null)
                                    trackingVM.Column1 = receipt.ReceivedBy;
                            }
                                
                            if (delivery.DeliveryRemark != null)
                                trackingVM.Column2 = delivery.DeliveryRemark.DeliveryRemarkName;
                            trackingVM.Column3 = delivery.DeliveredBy.FullName;
                            trackingVM.Column4 = "";
                            model.DetailedTrackingViewModels.Add(trackingVM);
                            break;

                            #endregion
                        }
                    }
                }
                #endregion
                
            }
            model.DetailedTrackingViewModels.OrderBy(x => x.TransactionDate).ToList();
            return View(model);
        }

        public void GetLocation(ref EmployeePositionMapping mapping, ref string location)
        {
            switch (mapping.LocationAssignment)
            {
                case AssignLocationConstant.Area:
                    var area = areaService.GetById(mapping.AssignedLocationId);
                    location = area.RevenueUnitName;
                    mapping.AssignedLocation = area;
                    break;
                case AssignLocationConstant.BSO:
                    var bso = bsoService.GetById(mapping.AssignedLocationId);
                    location = bso.RevenueUnitName;
                    mapping.AssignedLocation = bso;
                    break;
                case AssignLocationConstant.GatewaySat:
                    var gw = gatewayService.GetById(mapping.AssignedLocationId);
                    location = gw.RevenueUnitName;
                    mapping.AssignedLocation = gw;
                    break;
                case AssignLocationConstant.BCO:
                    var bco = bcoService.GetById(mapping.AssignedLocationId);
                    location = bco.BranchCorpOfficeName;
                    mapping.AssignedLocation = bco;
                    break;
            }
        }

    }
}
