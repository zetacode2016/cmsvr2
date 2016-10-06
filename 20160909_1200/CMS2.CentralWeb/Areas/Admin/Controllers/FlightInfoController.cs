using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Admin.ViewModels;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class FlightInfoController : AdminBaseController
    {
        private FlightInfoBL service;
        private BranchCorpOfficeBL bcoService;
        private CityBL cityService;
        //private GatewayBL gatewayService;

        public FlightInfoController()
        {
            service = new FlightInfoBL();
            bcoService = new BranchCorpOfficeBL(service.GetUnitOfWork());
            cityService = new CityBL(service.GetUnitOfWork());
            //gatewayService = new GatewayBL(service.GetUnitOfWork());
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "TntMaint");
        }

        [HttpGet]
        public ActionResult Add()
        {
            //ViewBag.Gateways = new SelectList(gatewayService.FilterActiveBy(x=>x.GatewayType.GatewayTypeName.Equals("Airline")),"GatewayId","GatewayName");
            ViewBag.OriginBcos = new SelectList(bcoService.FilterActive().OrderBy(x=>x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            ViewBag.DestinationBcos = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            ViewBag.OriginCities = new SelectList(Cities(), "Value", "Text");
            ViewBag.DestinationCities = new SelectList(Cities(), "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FlightInfoViewModel model)
        {
            //ViewBag.Gateways = new SelectList(gatewayService.FilterActiveBy(x => x.GatewayType.GatewayTypeName.Equals("Airline")), "GatewayId", "GatewayName", model.GatewayId);
            ViewBag.OriginBcos = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName", model.OriginBcoId);
            ViewBag.DestinationBcos = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName", model.DestinationBcoId);
            ViewBag.OriginCities = new SelectList(cityService.FilterBy(x => x.BranchCorpOfficeId == model.OriginBcoId).OrderBy(x => x.CityName), "CityId", "CityName", model.OriginCityId);
            ViewBag.DestinationCities = new SelectList(cityService.FilterBy(x => x.BranchCorpOfficeId == model.DestinationBcoId).OrderBy(x => x.CityName), "CityId", "CityName", model.DestinationCityId);

            if (ModelState.IsValid)
            {
                if (service.IsExist(x => x.OriginCityId == model.OriginCityId && x.DestinationCityId == model.DestinationCityId && x.FlightNo.Equals(model.FlightNo) && x.RecordStatus==3))
                {
                    var entity = service.FilterBy(x =>x.OriginCityId == model.OriginCityId && x.DestinationCityId == model.DestinationCityId && x.FlightNo.Equals(model.FlightNo) && x.RecordStatus == 3).FirstOrDefault();
                    entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    entity.ModifiedDate = DateTime.Now;
                    entity.RecordStatus = (int) RecordStatus.Active;
                    service.Edit(entity);
                }
                else
                {
                    FlightInfo entity = new FlightInfo();
                    entity.FlightInfoId = Guid.NewGuid();
                    //entity.GatewayId = model.GatewayId;
                    entity.FlightNo = model.FlightNo;
                    entity.OriginCityId = model.OriginCityId;
                    entity.DestinationCityId = model.DestinationCityId;
                    entity.ETD = model.ETD;
                    entity.ETA = model.ETA;
                    entity.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                    entity.CreatedDate = DateTime.Now;
                    entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    entity.ModifiedDate = DateTime.Now;
                    entity.RecordStatus = (int)RecordStatus.Active;
                    service.Add(entity);
                }
                return RedirectToAction("Index", "TntMaint");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            FlightInfoViewModel model = new FlightInfoViewModel();
            var entity = service.GetById(id);
            if (entity != null)
            {
                model.FlightInfoId = entity.FlightInfoId;
                //model.GatewayId = entity.GatewayId;
                model.FlightNo = entity.FlightNo;
                model.OriginCityId = entity.OriginCityId;
                model.OriginBcoId = entity.OriginCity.BranchCorpOffice.BranchCorpOfficeId;
                model.DestinationCityId = entity.DestinationCityId;
                model.DestinationBcoId = entity.DestinationCity.BranchCorpOffice.BranchCorpOfficeId;
                model.ETD = entity.ETD;
                model.ETA = entity.ETA;
            }
            //ViewBag.Gateways = new SelectList(gatewayService.FilterActiveBy(x => x.GatewayType.GatewayTypeName.Equals("Airline")), "GatewayId", "GatewayName",model.GatewayId);
            ViewBag.OriginBcos = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName",model.OriginBcoId);
            ViewBag.DestinationBcos = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName",model.DestinationBcoId);
            ViewBag.OriginCities = new SelectList(cityService.FilterBy(x=>x.BranchCorpOfficeId==model.OriginBcoId).OrderBy(x=>x.CityName),"CityId", "CityName", model.OriginCityId);
            ViewBag.DestinationCities = new SelectList(cityService.FilterBy(x => x.BranchCorpOfficeId == model.DestinationBcoId).OrderBy(x => x.CityName), "CityId", "CityName", model.DestinationCityId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FlightInfoViewModel model)
        {
            //ViewBag.Gateways = new SelectList(gatewayService.FilterActiveBy(x => x.GatewayType.GatewayTypeName.Equals("Airline")), "GatewayId", "GatewayName", model.GatewayId);
            ViewBag.OriginBcos = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName", model.OriginBcoId);
            ViewBag.DestinationBcos = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName", model.DestinationBcoId);
            ViewBag.OriginCities = new SelectList(cityService.FilterBy(x => x.BranchCorpOfficeId == model.OriginBcoId).OrderBy(x => x.CityName), "CityId", "CityName", model.OriginCityId);
            ViewBag.DestinationCities = new SelectList(cityService.FilterBy(x => x.BranchCorpOfficeId == model.DestinationBcoId).OrderBy(x => x.CityName), "CityId", "CityName", model.DestinationCityId);

            if (ModelState.IsValid)
            {
                if (service.IsExist(x => x.FlightInfoId == model.FlightInfoId))
                {
                    var entity = service.GetById(model.FlightInfoId);
                    //entity.GatewayId = model.GatewayId;
                    entity.FlightNo = model.FlightNo;
                    entity.OriginCityId = model.OriginCityId;
                    entity.DestinationCityId = model.DestinationCityId;
                    entity.ETD = model.ETD;
                    entity.ETA = model.ETA;
                    entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    entity.ModifiedDate = DateTime.Now;
                    entity.RecordStatus = (int)RecordStatus.Active;
                    service.Edit(entity);
                }
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

        // initial dropdownlist for Cities
        private List<SelectListItem> Cities()
        {
            List<SelectListItem> locations = new List<SelectListItem>();
            locations.Add(new SelectListItem { Text = "select BCO", Value = "" });
            return locations;
        }

        public ActionResult GetCities(Guid bcoId)
        {
            var result = cityService.FilterActiveBy(x=>x.BranchCorpOfficeId== bcoId).OrderBy(x => x.CityName).Select(x => new { value = x.CityId, text = x.CityName });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}