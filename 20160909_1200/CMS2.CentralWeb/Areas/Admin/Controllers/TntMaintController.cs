using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using CMS2.Common.Constants;
using CMS2.DataAccess;
using CMS2.Entities.TrackingEntities;
using System.Data.Entity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    /// <summary>
    /// This is a temporary controller. Delete in production
    /// Gets data from tracking2 to and updates cms2
    /// Updates tracking2 maintenance from cms2
    /// </summary>
    public class TntMaintController : AdminBaseController
    {
        private TntMaintBL service = new TntMaintBL();
        private FlightInfoBL flightNoService = new FlightInfoBL();

        public ActionResult Index()
        {
            List<TntMaint> maintenance = service.FilterActive();
            ViewBag.BranchAcceptanceBatch = new SelectList(maintenance.Where(x => x.FieldName.Equals("Batch") && x.Module.Equals("Branch Acceptance") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");
            ViewBag.BranchAcceptanceRemarks = new SelectList(maintenance.Where(x => x.FieldName.Equals("Remarks") && x.Module.Equals("Branch Acceptance") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");

            ViewBag.GatewayTransmittalBatch = new SelectList(maintenance.Where(x => x.FieldName.Equals("Batch") && x.Module.Equals("Gateway Transmittal") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");
            ViewBag.GatewayTransmittalDestination = new SelectList(maintenance.Where(x => x.FieldName.Equals("Destination") && x.Module.Equals("Gateway Transmittal") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");
            ViewBag.GatewayTransmittalGateway = new SelectList(maintenance.Where(x => x.FieldName.Equals("Gateway") && x.Module.Equals("Gateway Transmittal") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");


            ViewBag.GatewayOutboundBatch = new SelectList(maintenance.Where(x => x.FieldName.Equals("Batch") && x.Module.Equals("Gateway Outbound") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");
            ViewBag.GatewayOutboundRemarks = new SelectList(maintenance.Where(x => x.FieldName.Equals("Remarks") && x.Module.Equals("Gateway Outbound") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");
            ViewBag.GatewayOutboundGateway = new SelectList(maintenance.Where(x => x.FieldName.Equals("Gateway") && x.Module.Equals("Gateway Outbound") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");

            ViewBag.SegregationBatch = new SelectList(maintenance.Where(x => x.FieldName.Equals("Batch") && x.Module.Equals("Segregation") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");
            ViewBag.SegregationRemarks = new SelectList(maintenance.Where(x => x.FieldName.Equals("Remarks") && x.Module.Equals("Segregation") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");

            ViewBag.DistributionBatch = new SelectList(maintenance.Where(x => x.FieldName.Equals("Batch") && x.Module.Equals("Distribution") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");

            ViewBag.CargoTransferBatch = new SelectList(maintenance.Where(x => x.FieldName.Equals("Batch") && x.Module.Equals("Cargo Transfer") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");
            ViewBag.CargoTransferReason = new SelectList(maintenance.Where(x => x.FieldName.Equals("Reason") && x.Module.Equals("Cargo Transfer") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");
            ViewBag.CargoTransferAirline = new SelectList(maintenance.Where(x => x.FieldName.Equals("Airline") && x.Module.Equals("Cargo Transfer") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");
            ViewBag.CargoTransferBranch = new SelectList(maintenance.Where(x => x.FieldName.Equals("Branch") && x.Module.Equals("Cargo Transfer") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");

            ViewBag.GatewayInboundFlightNo = new SelectList(flightNoService.FilterActive(), "FlightInfoId", "FlightNo");
            ViewBag.GatewayInboundRemarks = new SelectList(maintenance.Where(x => x.FieldName.Equals("Remarks") && x.Module.Equals("Gateway Inbound") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");
            ViewBag.GatewayInboundOrigin = new SelectList(maintenance.Where(x => x.FieldName.Equals("Origin") && x.Module.Equals("Gateway Inbound") && x.RecordStatus == 1).ToList(), "TntMaintId", "FieldValue");

            ViewBag.HoldCargoStatus = new SelectList(maintenance.Where(x => x.FieldName.Equals("Status") && x.Module.Equals("Hold Cargo")).ToList(), "TntMaintId", "FieldValue");
            ViewBag.HoldCargoReason = new SelectList(maintenance.Where(x => x.FieldName.Equals("Reason") && x.Module.Equals("Hold Cargo")).ToList(), "TntMaintId", "FieldValue");

            ViewBag.BundlingDestination = new SelectList(maintenance.Where(x => x.FieldName.Equals("Destination") && x.Module.Equals("Bundling")).ToList(), "TntMaintId", "FieldValue");


            //ViewBag.GatewayTransmittalBatch = new SelectList(service.FilterBy(x => x.FieldName.Equals("Wave") && x.Module.Equals("Gateway Transmittal")).ToList(), "TntMaintId", "FieldValue");

            return View();
        }

        [HttpGet]
        public ActionResult Add(string mod, string fld)
        {
            TntMaint model = new TntMaint()
            {
                Module = mod,
                FieldName = fld,
                FieldCode = mod.Replace(" ", "").ToLower()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TntMaint model)
        {
            if (string.IsNullOrEmpty(model.FieldCode))
                model.FieldCode = "";

            if (ModelState.IsValid)
            {
                if (service.IsExist(x => x.Module.Equals(model.Module) && x.FieldName.Equals(model.FieldName) && x.FieldCode.Equals(model.FieldCode) && x.FieldValue.Equals(model.FieldValue) && x.RecordStatus == 3))
                {
                    var _model = service.FilterBy(x => x.Module.Equals(model.Module) && x.FieldName.Equals(model.FieldName) && x.FieldCode.Equals(model.FieldCode) && x.FieldValue.Equals(model.FieldValue) && x.RecordStatus == 3).FirstOrDefault();
                    _model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    _model.ModifiedDate = DateTime.Now;
                    _model.RecordStatus = (int)RecordStatus.Active;
                    service.Edit(_model);
                    return RedirectToAction("Index", "TntMaint");
                }
                else
                {
                    model.TntMaintId = Guid.NewGuid();
                    model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    model.ModifiedDate = DateTime.Now;
                    model.RecordStatus = (int)RecordStatus.Active;
                    service.Add(model);
                    return RedirectToAction("Index", "TntMaint");
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var model = service.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TntMaint model)
        {
            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.TntMaintId);
                _model.Module = model.Module;
                _model.FieldName = model.FieldName;
                _model.FieldCode = model.FieldCode;
                _model.FieldValue = model.FieldValue;
                _model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                _model.ModifiedDate = DateTime.Now;
                _model.RecordStatus = (int)RecordStatus.Active;
                service.Edit(_model);
                return RedirectToAction("Index", "TntMaint");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Delete(Guid id)
        {
            service.Delete(id);
            return RedirectToAction("Index", "TntMaint");
        }

        public ActionResult UpdateIndex()
        {
            return View();
        }

        public ActionResult Update()
        {
            TrackNTraceContext _trackingContext = new TrackNTraceContext();
            CommodityTypeBL commodityTypeService = new CommodityTypeBL();
            UserStore userService = new UserStore();
            TntMaintBL tntMaintService = new TntMaintBL();
            AreaBL areaService = new AreaBL();
            ShipModeBL shipModeService = new ShipModeBL();
            FlightInfoBL flightInfoService = new FlightInfoBL();
            List<string> targetModules;

            #region Destinations
            List<destination> _destinations = _trackingContext.destinations.ToList();
            List<TntMaint> destinations = tntMaintService.FilterBy(x => x.FieldName.Equals("Destination")).OrderByDescending(x => x.RecordStatus).ThenBy(x => x.FieldValue).ToList();
            foreach (var item in destinations)
            {
                try
                {
                    if (_destinations.Exists(x => x.cDestinationName.Equals(item.FieldValue) && x.cDestinationCode.Equals(item.FieldCode)))
                    {
                        destination model =
                            _trackingContext.destinations.FirstOrDefault(x => x.cDestinationName.Equals(item.FieldValue));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.destinations.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cDestinationCode = item.FieldCode;
                            model.cDestinationName = item.FieldValue;
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        destination _model = new destination();
                        _model.cDestinationCode = item.FieldCode;
                        _model.cDestinationName = item.FieldValue;
                        _trackingContext.destinations.Add(_model);
                    }
                    _trackingContext.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            #endregion

            #region Origins
            List<origin> _origins = _trackingContext.origins.ToList();
            List<TntMaint> origins = tntMaintService.FilterBy(x => x.FieldName.Equals("Origin")).OrderByDescending(x => x.RecordStatus).ThenBy(x => x.FieldValue).ToList();
            foreach (var item in origins)
            {
                try
                {
                    if (_origins.Exists(x => x.cOriginName.Equals(item.FieldValue) && x.cOriginCode.Equals(item.FieldCode)))
                    {
                        origin model =
                            _trackingContext.origins.FirstOrDefault(x => x.cOriginName.Equals(item.FieldValue));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.origins.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cOriginCode = item.FieldCode;
                            model.cOriginName = item.FieldValue;
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        origin _model = new origin();
                        _model.cOriginCode = item.FieldCode;
                        _model.cOriginName = item.FieldValue;
                        _trackingContext.origins.Add(_model);
                    }
                    _trackingContext.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            #endregion

            #region Gateways
            List<gateway> _gateways = _trackingContext.gateways.ToList();
            List<TntMaint> gateways = tntMaintService.FilterBy(x => x.FieldName.Equals("Gateway")).OrderByDescending(x => x.RecordStatus).ThenBy(x => x.FieldValue).ToList();
            foreach (var item in gateways)
            {
                try
                {
                    if (_gateways.Exists(x => x.cGatewayName.Equals(item.FieldValue) && x.cGatewayCode.Equals(item.FieldCode)))
                    {
                        gateway model =
                            _trackingContext.gateways.FirstOrDefault(x => x.cGatewayName.Equals(item.FieldValue));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.gateways.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cGatewayCode = item.FieldCode;
                            model.cGatewayName = item.FieldValue;
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        gateway _model = new gateway();
                        _model.cGatewayCode = item.FieldCode;
                        _model.cGatewayName = item.FieldValue;
                        _trackingContext.gateways.Add(_model);
                    }
                    _trackingContext.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            #endregion

            #region Airlines
            List<airline> _airlines = _trackingContext.airlines.ToList();
            List<TntMaint> airlines = tntMaintService.FilterBy(x => x.FieldName.Equals("Gateway")).OrderByDescending(x => x.RecordStatus).ThenBy(x => x.FieldValue).ToList();
            foreach (var item in gateways)
            {
                try
                {
                    if (_airlines.Exists(x => x.cAirlineName.Equals(item.FieldValue) && x.cAirlineCode.Equals(item.FieldCode)))
                    {
                        airline model =
                            _trackingContext.airlines.FirstOrDefault(x => x.cAirlineName.Equals(item.FieldValue));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.airlines.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cAirlineCode = item.FieldCode;
                            model.cAirlineName = item.FieldValue;
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        airline _model = new airline();
                        _model.cAirlineCode = item.FieldCode;
                        _model.cAirlineName = item.FieldValue;
                        _trackingContext.airlines.Add(_model);
                    }
                    _trackingContext.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            #endregion

            #region Users
            List<users> tntUsers = _trackingContext.users.ToList();
            List<User> cms2Users =
                userService.GetAllUsers().OrderByDescending(x => x.RecordStatus).ThenBy(x => x.UserName).ToList();

            // remove user from tnt not in cms2
            var existing = cms2Users.Select(x => x.UserName).ToList();
            var nonExisting = tntUsers.Where(x => !existing.Contains(x.cUsername)).OrderBy(x => x.cUsername).ToList();
            foreach (var item in nonExisting)
            {
                var model = _trackingContext.users.FirstOrDefault(x => x.cUsername.Equals(item.cUsername));
                if (model != null)
                {
                    _trackingContext.users.Remove(model);
                    _trackingContext.Entry(model).State = EntityState.Deleted;
                    _trackingContext.SaveChanges();
                }
            }

            foreach (var item in cms2Users)
            {
                try
                {
                    if (tntUsers.Exists(x => x.cUsername.Equals(item.UserName)))
                    {
                        users model = _trackingContext.users.FirstOrDefault(x => x.cUsername.Equals(item.UserName));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.users.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cUsername = item.UserName;
                            model.cCity = "";
                            model.cBranch = "";
                            model.cBco = "";
                            if (item.Employee.EmployeePositionMappings.Count > 0)
                            {
                                var mapping =
                                    item.Employee.EmployeePositionMappings.Where(
                                        x => x.RecordStatus == (int)RecordStatus.Active).FirstOrDefault();
                                if (mapping != null)
                                {
                                    model.cCity = mapping.AssignedLocation.City.CityName;
                                    model.cBranch = mapping.AssignedLocation.RevenueUnitName;
                                    model.cBco = mapping.AssignedLocation.City.BranchCorpOffice.BranchCorpOfficeName;
                                }

                            }
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        users _model = new users();
                        _model.cUsername = item.UserName;
                        _model.lActive = true;
                        _model.lAdmin = true;
                        _model.cBranch = "";
                        if (item.Employee.EmployeePositionMappings.Count > 0)
                        {
                            var mapping =
                                item.Employee.EmployeePositionMappings.Where(
                                    x => x.RecordStatus == (int)RecordStatus.Active).FirstOrDefault();
                            if (mapping != null)
                            {
                                _model.cCity = mapping.AssignedLocation.City.CityName;
                                _model.cBranch = mapping.AssignedLocation.RevenueUnitName;
                                _model.cBco = mapping.AssignedLocation.City.BranchCorpOffice.BranchCorpOfficeName;
                            }
                        }
                        _trackingContext.users.Add(_model);
                    }
                    _trackingContext.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            #endregion

            #region CommodityTypes
            List<commodity> _commodities = _trackingContext.commodities.ToList();
            List<CommodityType> commodities = commodityTypeService.GetAll().OrderByDescending(x => x.RecordStatus).ThenBy(x => x.CommodityTypeName).ToList();
            targetModules = new List<string>() { "gatewayinbound", "gatewaytransmittal" };

            foreach (var module in targetModules)
            {
                foreach (var item in commodities)
                {
                    try
                    {
                        string commodityTypeName = item.CommodityTypeName;
                        if (commodityTypeName.Length > 20)
                        {
                            commodityTypeName = commodityTypeName.Substring(0, 20);
                        }
                        if (_commodities.Exists(x => x.cCommodityName.Equals(commodityTypeName) && x.cCommodityCode.Equals(module)))
                        {
                            commodity model =
                                _trackingContext.commodities.FirstOrDefault(
                                    x => x.cCommodityName.Equals(commodityTypeName));
                            if (item.RecordStatus == (int)RecordStatus.Deleted)
                            {
                                _trackingContext.commodities.Remove(model);
                                _trackingContext.Entry(model).State = EntityState.Deleted;
                            }
                            else
                            {
                                model.cCommodityCode = module;
                                model.cCommodityName = commodityTypeName;
                                _trackingContext.Entry(model).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            commodity _model = new commodity();
                            _model.cCommodityCode = module;
                            _model.cCommodityName = commodityTypeName;
                            _trackingContext.commodities.Add(_model);
                        }
                        _trackingContext.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            #endregion

            #region Remarks
            List<remarks> _remarks = _trackingContext.remarks.ToList();
            List<TntMaint> remarks = tntMaintService.FilterBy(x => x.FieldName.Equals("Remarks")).OrderByDescending(x => x.RecordStatus).ThenBy(x => x.FieldValue).ToList();
            foreach (var item in remarks)
            {
                try
                {
                    if (_remarks.Exists(x => x.cRemarkName.Equals(item.FieldValue) && x.cRemarkCode.Equals(item.FieldCode)))
                    {
                        remarks model =
                            _trackingContext.remarks.FirstOrDefault(x => x.cRemarkName.Equals(item.FieldValue));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.remarks.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cRemarkCode = item.FieldCode;
                            model.cRemarkName = item.FieldValue;
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        remarks _model = new remarks();
                        _model.cRemarkCode = item.FieldCode;
                        _model.cRemarkName = item.FieldValue;
                        _trackingContext.remarks.Add(_model);
                    }
                    _trackingContext.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            #endregion

            #region Status
            List<status> _status = _trackingContext.status.ToList();
            List<TntMaint> status = tntMaintService.FilterBy(x => x.FieldName.Equals("Status")).OrderByDescending(x => x.RecordStatus).ThenBy(x => x.FieldValue).ToList();

            foreach (var item in status)
            {
                try
                {
                    if (_status.Exists(x => x.cStatusName.Equals(item.FieldValue)))
                    {
                        status model =
                            _trackingContext.status.FirstOrDefault(x => x.cStatusName.Equals(item.FieldValue));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.status.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cStatusCode = "holdcargo";
                            model.cStatusName = item.FieldValue;
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        status _model = new status();
                        _model.cStatusCode = "holdcargo";
                        _model.cStatusName = item.FieldValue;
                        _trackingContext.status.Add(_model);
                    }
                    _trackingContext.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            #endregion

            #region Batch
            List<batch> _batches = _trackingContext.batches.ToList();
            List<TntMaint> batches = tntMaintService.FilterBy(x => x.FieldName.Equals("Batch")).OrderByDescending(x => x.RecordStatus).ThenBy(x => x.FieldValue).ToList();
            foreach (var item in batches)
                {
                    try
                    {
                        if (_batches.Exists(x => x.cBatchName.Equals(item.FieldValue) && x.cBatchCode.Equals(item.FieldCode)))
                        {
                            batch model = _trackingContext.batches.FirstOrDefault(x => x.cBatchName.Equals(item.FieldValue));
                            if (item.RecordStatus == (int)RecordStatus.Deleted)
                            {
                                _trackingContext.batches.Remove(model);
                                _trackingContext.Entry(model).State = EntityState.Deleted;
                            }
                            else
                            {
                                model.cBatchCode = item.FieldCode;
                                model.cBatchName = item.FieldValue;
                                _trackingContext.Entry(model).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            batch _model = new batch();
                            _model.cBatchCode = item.FieldCode;
                            _model.cBatchName = item.FieldValue;
                            _trackingContext.batches.Add(_model);
                        }
                        _trackingContext.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
                }

            #endregion

            #region Area
            List<area> _areas = _trackingContext.areas.ToList();
            List<RevenueUnit> areas = areaService.FilterBy(x => x.RevenueUnitType.RevenueUnitTypeName.Equals(RevenueUnitTypes.Area)).OrderByDescending(x => x.RecordStatus).ThenBy(x => x.RevenueUnitName).ToList();

            foreach (var item in areas)
            {
                try
                {
                    if (_areas.Exists(x => x.cAreaName.Equals(item.RevenueUnitName)))
                    {
                        area model = _trackingContext.areas.FirstOrDefault(x => x.cAreaName.Equals(item.RevenueUnitName));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.areas.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cAreaCode = "distribution";
                            model.cAreaName = item.RevenueUnitName;
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        area _model = new area();
                        _model.cAreaCode = "distribution";
                        _model.cAreaName = item.RevenueUnitName;
                        _trackingContext.areas.Add(_model);
                    }
                    _trackingContext.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
            #endregion

            #region Reason
            List<reason> _reasons = _trackingContext.reasons.ToList();
            List<TntMaint> reasons = tntMaintService.FilterBy(x => x.FieldName.Equals("Reason")).OrderByDescending(x => x.RecordStatus).ThenBy(x => x.FieldValue).ToList();
            foreach (var item in reasons)
                {
                    if (_reasons.Exists(x => x.cReasonName.Equals(item.FieldValue) && x.cReasonCode.Equals(item.FieldCode)))
                    {
                        reason model =
                            _trackingContext.reasons.FirstOrDefault(x => x.cReasonName.Equals(item.FieldValue));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.reasons.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cReasonCode = item.FieldCode;
                            model.cReasonName = item.FieldValue;
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        reason _model = new reason();
                        _model.cReasonCode = item.FieldCode;
                        _model.cReasonName = item.FieldValue;
                        _trackingContext.reasons.Add(_model);
                    }
                    try
                    {
                        _trackingContext.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
                }

            #endregion

            #region BSO->Branch and BCO->Branch
            List<branch> _branches = _trackingContext.branches.ToList();
            List<RevenueUnit> branches = areaService.FilterBy(x => x.RevenueUnitType.RevenueUnitTypeName.Equals(RevenueUnitTypes.BSO)).OrderByDescending(x => x.RecordStatus).ToList();
            foreach (var item in branches)
            {
                try
                {
                    if (_branches.Exists(x => x.cBranchName.Equals(item.RevenueUnitName)))
                    {
                        branch model = _trackingContext.branches.FirstOrDefault(x => x.cBranchName.Equals(item.RevenueUnitName));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.branches.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cBranchCode = "cargotransfer";
                            model.cBranchName = item.RevenueUnitName;
                            model.cCity = item.City.CityName;
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        branch _model = new branch();
                        _model.cBranchCode = "cargotransfer";
                        _model.cBranchName = item.RevenueUnitName;
                        _model.cCity = item.City.CityName;
                        _trackingContext.branches.Add(_model);
                    }
                    _trackingContext.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            BranchCorpOfficeBL branchCorpOfficeService = new BranchCorpOfficeBL();
            List<BranchCorpOffice> bcos = branchCorpOfficeService.GetAll();
            foreach (var item in bcos)
            {
                try
                {
                    if (_branches.Exists(x => x.cBranchName.Equals(item.BranchCorpOfficeName)))
                    {
                        branch model = _trackingContext.branches.FirstOrDefault(x => x.cBranchName.Equals(item.BranchCorpOfficeName));
                        if (item.RecordStatus == (int)RecordStatus.Deleted)
                        {
                            _trackingContext.branches.Remove(model);
                            _trackingContext.Entry(model).State = EntityState.Deleted;
                        }
                        else
                        {
                            model.cBranchCode = "cargotransfer";
                            model.cBranchName = item.BranchCorpOfficeName;
                            model.cCity = "";
                            _trackingContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        branch _model = new branch();
                        _model.cBranchCode = "cargotransfer";
                        _model.cBranchName = item.BranchCorpOfficeName;
                        _model.cCity = "";
                        _trackingContext.branches.Add(_model);
                    }
                    _trackingContext.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
            #endregion

            #region ShipMode

            List<shipmode> _shipmodes = _trackingContext.shipmodes.ToList();
            List<ShipMode> shipmodes = shipModeService.GetAll().OrderByDescending(x => x.RecordStatus).ThenBy(x => x.ShipModeName).ToList();
            targetModules = new List<string>() { "gatewayoutbound", "gatewayinbound" };

            foreach (var module in targetModules)
            {
                foreach (var item in shipmodes)
                {
                    try
                    {
                        if (_shipmodes.Exists(x => x.cShipmodeName.Equals(item.ShipModeName) && x.cShipmodeCode.Equals(module)))
                        {
                            shipmode model = _trackingContext.shipmodes.FirstOrDefault(x => x.cShipmodeName.Equals(item.ShipModeName));
                            if (item.RecordStatus == (int)RecordStatus.Deleted)
                            {
                                _trackingContext.shipmodes.Remove(model);
                                _trackingContext.Entry(model).State = EntityState.Deleted;
                            }
                            else
                            {
                                model.cShipmodeCode = module;
                                model.cShipmodeName = item.ShipModeName;
                                _trackingContext.Entry(model).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            shipmode _model = new shipmode();
                            _model.cShipmodeCode = module;
                            _model.cShipmodeName = item.ShipModeName;
                            _trackingContext.shipmodes.Add(_model);
                        }
                        _trackingContext.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            #endregion

            #region FlightNo

            List<flight> _flights = _trackingContext.flights.ToList();
            List<FlightInfo> flights = flightInfoService.GetAll().OrderByDescending(x => x.RecordStatus).ThenBy(x => x.FlightNo).ToList();
            targetModules = new List<string>() { "gatewayinbound", "gatewaytransmittal" };

            foreach (var module in targetModules)
            {
                foreach (var item in flights)
                {
                    try
                    {
                        if (_flights.Exists(x => x.cFlightNo.Equals(item.FlightNo) && x.cFlightCode.Equals(module)))
                        {
                            flight model = _trackingContext.flights.FirstOrDefault(x => x.cFlightNo.Equals(item.FlightNo));
                            if (item.RecordStatus == (int)RecordStatus.Deleted)
                            {
                                _trackingContext.flights.Remove(model);
                                _trackingContext.Entry(model).State = EntityState.Deleted;
                            }
                            else
                            {
                                model.cFlightCode = module;
                                model.cFlightNo = item.FlightNo;
                                _trackingContext.Entry(model).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            flight _model = new flight();
                            _model.cFlightCode = module;
                            _model.cFlightNo = item.FlightNo;
                            _trackingContext.flights.Add(_model);
                        }
                        _trackingContext.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            #endregion

            ViewData["Messsage"] = "Update finished.";
            return View();
        }

    }
}
