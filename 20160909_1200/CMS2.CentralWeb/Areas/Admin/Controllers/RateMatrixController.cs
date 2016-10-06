using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Admin.ViewModels;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class RateMatrixController : AdminBaseController
    {
        private RateMatrixBL service;
        private ApplicableRateBL applicableRateService;
        private CommodityTypeBL commodityTypeService;
        private ServiceTypeBL serviceTypeService;
        private ServiceModeBL serviceModeService;
        private CityBL cityService;
        private List<Guid> existingApplicableRates;
        private ExpressRateBL expressRateService;

        public RateMatrixController()
        {
            service = new RateMatrixBL();
            applicableRateService = new ApplicableRateBL(service.GetUnitOfWork());
            commodityTypeService = new CommodityTypeBL(service.GetUnitOfWork());
            serviceTypeService = new ServiceTypeBL(service.GetUnitOfWork());
            serviceModeService = new ServiceModeBL(service.GetUnitOfWork());
            cityService = new CityBL(service.GetUnitOfWork());
            existingApplicableRates = service.GetApplicableRates();
            expressRateService = new ExpressRateBL(service.GetUnitOfWork());
        }

        public ActionResult Index()
        {

            ViewBag.ApplicableRates =
                new SelectList(applicableRateService.FilterActiveBy(x => existingApplicableRates.Contains(x.ApplicableRateId)).OrderBy(x => x.ApplicableRateName).ToList(),
                    "ApplicableRateId", "ApplicableRateName");
            //ViewBag.CommodityTypes = new SelectList(InitList(), "Value", "Text");
            //ViewBag.ServiceTypes = new SelectList(InitList(), "Value", "Text");
            //ViewBag.ServiceModes = new SelectList(InitList(), "Value", "Text");
            ViewBag.OriginCities = new SelectList(InitList(), "Value", "Text");
            ViewBag.DestinationCities = new SelectList(InitList(), "Value", "Text");

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {

            ViewBag.ApplicableRates =
               new SelectList(applicableRateService.FilterActive().OrderBy(x => x.ApplicableRateName).ToList(),
                   "ApplicableRateId", "ApplicableRateName");
            ViewBag.CommodityTypes =
                new SelectList(commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList(),
                    "CommodityTypeId", "CommodityTypeName");
            ViewBag.ServiceTypes =
                new SelectList(serviceTypeService.FilterActive().OrderBy(x => x.ServiceTypeName).ToList(),
                    "ServiceTypeId", "ServiceTypeName");
            ViewBag.ServiceModes =
                new SelectList(serviceModeService.FilterActive().OrderBy(x => x.ServiceModeName).ToList(),
                    "ServiceModeId", "ServiceModeName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RateMatrix model)
        {
            ViewBag.ApplicableRates =
               new SelectList(applicableRateService.FilterActive().OrderBy(x => x.ApplicableRateName).ToList(),
                   "ApplicableRateId", "ApplicableRateName", model.ApplicableRateId);
            ViewBag.CommodityTypes =
                new SelectList(commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList(),
                    "CommodityTypeId", "CommodityTypeName", model.CommodityTypeId);
            ViewBag.ServiceTypes =
                new SelectList(serviceTypeService.FilterActive().OrderBy(x => x.ServiceTypeName).ToList(),
                    "ServiceTypeId", "ServiceTypeName", model.ServiceTypeId);
            ViewBag.ServiceModes =
                new SelectList(serviceModeService.FilterActive().OrderBy(x => x.ServiceModeName).ToList(),
                    "ServiceModeId", "ServiceModeName", model.ServiceModeId);

            if (ModelState.IsValid)
            {
                if (
                    service.IsExist(
                        x =>
                            x.ApplicableRateId == model.ApplicableRateId && x.CommodityTypeId == model.CommodityTypeId &&
                            x.ServiceTypeId == model.ServiceTypeId && x.ServiceModeId == model.ServiceModeId))
                {
                    var entity = service.FilterBy(x => x.ApplicableRateId == model.ApplicableRateId && x.CommodityTypeId == model.CommodityTypeId && x.ServiceTypeId == model.ServiceTypeId && x.ServiceModeId == model.ServiceModeId).FirstOrDefault();
                    entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    entity.ModifiedDate = DateTime.Now;
                    entity.RecordStatus = (int)RecordStatus.Active;
                    service.Edit(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    model.RateMatrixId = Guid.NewGuid();
                    model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    model.ModifiedDate = DateTime.Now;
                    model.RecordStatus = (int)RecordStatus.Active;
                    service.Add(model);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult CopyCreate()
        {
            ViewBag.FromApplicableRates =
               new SelectList(applicableRateService.FilterActiveBy(x => existingApplicableRates.Contains(x.ApplicableRateId)).OrderBy(x => x.ApplicableRateName).ToList(),
                   "ApplicableRateId", "ApplicableRateName");
            ViewBag.ToApplicableRates =
              new SelectList(applicableRateService.FilterActiveBy(x => !existingApplicableRates.Contains(x.ApplicableRateId)).OrderBy(x => x.ApplicableRateName).ToList(),
                  "ApplicableRateId", "ApplicableRateName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CopyCreate(RateMatrixCopyCreateViewModel model)
        {
            ViewBag.FromApplicableRates =
               new SelectList(applicableRateService.FilterActiveBy(x => existingApplicableRates.Contains(x.ApplicableRateId)).OrderBy(x => x.ApplicableRateName).ToList(),
                   "ApplicableRateId", "ApplicableRateName", model.FromApplicableRateId);
            ViewBag.ToApplicableRates =
              new SelectList(applicableRateService.FilterActiveBy(x => !existingApplicableRates.Contains(x.ApplicableRateId)).OrderBy(x => x.ApplicableRateName).ToList(),
                  "ApplicableRateId", "ApplicableRateName", model.ToApplicableRateId);

            if (ModelState.IsValid)
            {
                service.CopyCreate(model.FromApplicableRateId, model.ToApplicableRateId, Guid.Parse(User.Identity.GetUserId()));
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        private List<SelectListItem> InitList()
        {
            List<SelectListItem> init = new List<SelectListItem>();
            init.Add(new SelectListItem { Text = "", Value = "" });
            return init;
        }

        public ActionResult GetApplicableRate(Guid rid)
        {
            var result = service.FilterActiveBy(x => x.ApplicableRateId == rid).FirstOrDefault();
            RateMatrixViewModel vm = new RateMatrixViewModel();
            if (result != null)
            {
                
                vm.RateMatrixId = result.RateMatrixId;
                vm.ApplicableRateId = result.ApplicableRateId;
                vm.CommodityTypeName = result.CommodityType.CommodityTypeName;
                vm.ServiceTypeName = result.ServiceType.ServiceTypeName;
                vm.ServiceModeName = result.ServiceMode.ServiceModeName;
                vm.ApplyEvm = result.ApplyEvm;
                vm.ApplyWeight = result.ApplyWeight;
                vm.HasAwbFee = result.HasAwbFee;
                vm.HasInsurance = result.HasInsurance;
                vm.HasFuelCharge = result.HasFuelCharge;
                vm.IsVatable = result.IsVatable;
                vm.HasValuationCharge = result.HasValuationCharge;
                vm.HasDeliveryFee = result.HasDeliveryFee;
                vm.HasPerishableFee = result.HasPerishableFee;
                vm.HasDangerousFee = result.HasDangerousFee;
            }
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCommodityTypes(Guid rid)
        {
            var result = service.FilterActiveBy(x => x.ApplicableRateId == rid).Select(x => new { value = x.CommodityTypeId, text = x.CommodityType.CommodityTypeName }).Distinct().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServiceTypes(Guid rid, Guid cid)
        {
            var result = service.FilterActiveBy(x => x.ApplicableRateId == rid && x.CommodityTypeId == cid).Select(x => new { value = x.ServiceTypeId, text = x.ServiceType.ServiceTypeName }).Distinct().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServiceModes(Guid rid, Guid cid, Guid stid)
        {
            var result = service.FilterActiveBy(x => x.ApplicableRateId == rid && x.CommodityTypeId == cid && x.ServiceTypeId == stid).Select(x => new { value = x.ServiceModeId, text = x.ServiceMode.ServiceModeName }).Distinct().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetExpressRate(Guid rid)
        {
            var matrix =
                service.FilterActiveBy(
                    x =>
                        x.ApplicableRateId == rid).FirstOrDefault();
            List<ExpressRateViewModel> expressRates = new List<ExpressRateViewModel>();
            if (matrix != null)
            {
                expressRates = GetExpressRates(matrix.RateMatrixId);
            }
            return PartialView("_ExpressRate", expressRates);
        }

        private List<ExpressRateViewModel> GetExpressRates(Guid matrixId)
        {
            List<ExpressRateViewModel> expressRates = new List<ExpressRateViewModel>();
            var rates = expressRateService.FilterActiveBy(x => x.RateMatrixId == matrixId).OrderBy(x => x.OriginCity.CityName).ThenBy(x => x.DestinationCity.CityName).ThenBy(x => x.MinimumWeight).ToList();
            if (rates != null)
            {
                string previousOrigin = "";
                string previousDestination = "";
                ExpressRateViewModel vm = new ExpressRateViewModel();
                foreach (var rate in rates)
                {
                    if (rate.OriginCity.CityName.Equals(previousOrigin))
                    {
                        if (rate.DestinationCity.CityName.Equals(previousDestination))
                        {
                            var _vm =
                                expressRates.FirstOrDefault(
                                    x =>
                                        x.RateOriginCity.Equals(previousOrigin) &&
                                        x.RateDestinationCity.Equals(previousDestination));
                            _vm.Costs.Add(rate.Cost);
                        }
                        else
                        {
                            previousDestination = rate.DestinationCity.CityName;
                            vm = new ExpressRateViewModel();
                            vm.Costs = new List<decimal>();
                            vm.RateMatrixId = rate.RateMatrixId;
                            vm.RateOriginCityId = rate.OriginCityId;
                            vm.RateOriginCity = rate.OriginCity.CityName;
                            vm.RateDestinationCityId = rate.DestinationCityId;
                            vm.RateDestinationCity = rate.DestinationCity.CityName;
                            vm.Costs.Add(rate.Cost);
                            expressRates.Add(vm);
                        }
                    }
                    else
                    {
                        previousOrigin = rate.OriginCity.CityName;
                        previousDestination = rate.DestinationCity.CityName;
                        vm = new ExpressRateViewModel();
                        vm.Costs = new List<decimal>();
                        vm.RateMatrixId = rate.RateMatrixId;
                        vm.RateOriginCityId = rate.OriginCityId;
                        vm.RateOriginCity = rate.OriginCity.CityName;
                        vm.RateDestinationCityId = rate.DestinationCityId;
                        vm.RateDestinationCity = rate.DestinationCity.CityName;
                        vm.Costs.Add(rate.Cost);
                        expressRates.Add(vm);
                    }
                }
            }
            return expressRates;
        }

        public bool DeleteRow(Guid matrixId, Guid originId, Guid destinationId)
        {
            var rates =
                expressRateService.FilterActiveBy(
                    x =>
                        x.RateMatrixId == matrixId && x.OriginCityId == originId && x.DestinationCityId == destinationId);
            if (rates == null)
            {
                return false;
            }
            else
            {
                foreach (var item in rates)
                {
                    item.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    item.ModifiedDate = DateTime.Now;
                    item.RecordStatus = (int)RecordStatus.Deleted;
                    expressRateService.Edit(item);
                }
                return true;
            }
        }

        [HttpGet]
        public ActionResult AddRow(Guid matrixId, Guid originId, Guid destinationId)
        {
            var entities = expressRateService.FilterActiveBy(x => x.RateMatrixId == matrixId && x.OriginCityId == originId && x.DestinationCityId == destinationId).OrderBy(x => x.MinimumWeight).ToList();
            List<string> result = new List<string>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.CostString);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult NewExpressRate(Guid rid)
        {
            var matrix = service.FilterActiveBy(x => x.ApplicableRateId == rid).FirstOrDefault();
            ExpressRateViewModel viewModel = new ExpressRateViewModel();
            viewModel.RateMatrixId = matrix.RateMatrixId;
            viewModel.Costs = new List<decimal>();
            return PartialView("_NewExpressRate", viewModel);
        }

        [HttpGet]
        public ActionResult GetMatrix(Guid rid, Guid cid, Guid stid, Guid smid)
        {
            var result = service.FilterActiveBy(x => x.ApplicableRateId == rid && x.CommodityTypeId == cid && x.ServiceTypeId == stid && x.ServiceModeId == smid).FirstOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveNewRow(ExpressRateViewModel model)
        {
            List<decimal> minwt = new List<decimal>() { (decimal)0.01, (decimal)5.50, (decimal)49.50, (decimal)249.49, (decimal)999.50 };
            List<decimal> maxwt = new List<decimal>() { (decimal)5.49, (decimal)49.49, (decimal)249.50, (decimal)949.50, (decimal)10000.00 };

            for (int index = 0; index < model.Costs.Count; index++)
            {
                ExpressRate entity = new ExpressRate();
                entity.ExpressRateId = Guid.NewGuid();
                entity.RateMatrixId = model.RateMatrixId;
                entity.OriginCityId = model.RateOriginCityId;
                entity.DestinationCityId = model.RateDestinationCityId;
                entity.Cost = Convert.ToDecimal(model.Costs[index].ToString());
                entity.EffectiveDate = DateTime.Now;
                entity.MinimumWeight = minwt[index];
                entity.MaximumWeight = maxwt[index];
                entity.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                entity.ModifiedDate = DateTime.Now;
                entity.RecordStatus = (int)RecordStatus.Active;
                expressRateService.Add(entity);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetOriginCities(Guid matrixId)
        {
            var result = expressRateService.GetOriginCitiesByMatrixId(matrixId).OrderBy(x => x.CityName).Select(x => new { value = x.CityId, text = x.CityName }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDestinationCities(Guid matrixId)
        {
            var result = expressRateService.GetDestinationCitiesByMatrixId(matrixId).OrderBy(x => x.CityName).Select(x => new { value = x.CityId, text = x.CityName }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(Guid matrixId)
        {
            var model = service.GetById(matrixId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RateMatrix model)
        {
            if (ModelState.IsValid)
            {
                var entity = service.GetById(model.RateMatrixId);
                entity.ApplyEvm = model.ApplyEvm;
                entity.ApplyWeight = model.ApplyWeight;
                entity.HasAwbFee = model.HasAwbFee;
                entity.HasInsurance = model.HasInsurance;
                entity.IsVatable = model.IsVatable;
                entity.HasFuelCharge = model.HasFuelCharge;
                entity.HasValuationCharge = model.HasValuationCharge;
                entity.HasDeliveryFee = model.HasDeliveryFee;
                entity.HasPerishableFee = model.HasPerishableFee;
                entity.HasDangerousFee = model.HasDangerousFee;
                entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                entity.ModifiedDate = DateTime.Now;
                entity.RecordStatus = (int) RecordStatus.Active;
                service.Edit(entity);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}