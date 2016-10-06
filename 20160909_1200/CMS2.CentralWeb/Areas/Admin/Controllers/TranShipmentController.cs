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
    public class TranShipmentController : AdminBaseController
    {
        private TransShipmentRouteBL service = new TransShipmentRouteBL();
        CityBL cityService = new CityBL();
        TransShipmentLegBL tranShipmentLegsService = new TransShipmentLegBL();
        //public ActionResult Index()
        //{
        //    var list = service.FilterActive();
        //    return View(list);
        //}

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.OriginCities = new SelectList(cityService.FilterActive().OrderBy(x=>x.CityName).ToList(), "CityId", "CityName");
            ViewBag.DestinationCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName");
            ViewBag.LegCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TranShipmentRouteViewModel model)
        {
            ViewBag.OriginCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName", model.OriginCityId);
            ViewBag.DestinationCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName", model.DestinationCityId);
            ViewBag.LegCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName",model.LegId);

            if (ModelState.IsValid)
            {
                TransShipmentRoute entity = new TransShipmentRoute();
                entity.TransShipmentRouteId = Guid.NewGuid();
                entity.TransShipmentRouteName = model.TransShipmentRouteName;
                entity.OriginCityId = model.OriginCityId;
                entity.DestinationCityId = model.DestinationCityId;
                entity.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                entity.ModifiedDate = DateTime.Now;
                entity.RecordStatus = (int)RecordStatus.Active;
                entity.Legs = new List<TransShipmentLeg>();
                entity.Legs.Add(new TransShipmentLeg()
                {
                    TransShipmentLegId = Guid.NewGuid(),
                    LegId = model.LegId,
                    LegOrder = 1,
                    CreatedBy = Guid.Parse(User.Identity.GetUserId()),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = Guid.Parse(User.Identity.GetUserId()),
                    ModifiedDate = DateTime.Now,
                    RecordStatus = (int)RecordStatus.Active
                });
                service.Add(entity);
                return RedirectToAction("Index", "Maintenance");
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
            if (model.Legs == null)
            {
                model.Legs =
                    tranShipmentLegsService.FilterActiveBy(x => x.TransShipmentRouteId == model.TransShipmentRouteId);
            }
            TranShipmentRouteViewModel vm = new TranShipmentRouteViewModel();
            vm.TransShipmentRouteId = model.TransShipmentRouteId;
            vm.TransShipmentRouteName = model.TransShipmentRouteName;
            vm.OriginCityId = model.OriginCityId;
            vm.LegId = model.Legs[0].LegId;
            vm.DestinationCityId = model.DestinationCityId;

            ViewBag.OriginCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName", vm.OriginCityId);
            ViewBag.DestinationCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName", vm.DestinationCityId);
            ViewBag.LegCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName", vm.LegId);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TranShipmentRouteViewModel model)
        {
            ViewBag.OriginCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName", model.OriginCityId);
            ViewBag.DestinationCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName", model.DestinationCityId);
            ViewBag.LegCities = new SelectList(cityService.FilterActive().OrderBy(x => x.CityName).ToList(), "CityId", "CityName", model.LegId);

            if (ModelState.IsValid)
            {
                service.Delete(model.TransShipmentRouteId);

                TransShipmentRoute entity = new TransShipmentRoute();
                entity.TransShipmentRouteId = Guid.NewGuid();
                entity.TransShipmentRouteName = model.TransShipmentRouteName;
                entity.OriginCityId = model.OriginCityId;
                entity.DestinationCityId = model.DestinationCityId;
                entity.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                entity.ModifiedDate = DateTime.Now;
                entity.RecordStatus = (int)RecordStatus.Active;
                entity.Legs = new List<TransShipmentLeg>();
                entity.Legs.Add(new TransShipmentLeg()
                {
                    TransShipmentLegId = Guid.NewGuid(),
                    LegId = model.LegId,
                    LegOrder = 1,
                    CreatedBy = Guid.Parse(User.Identity.GetUserId()),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = Guid.Parse(User.Identity.GetUserId()),
                    ModifiedDate = DateTime.Now,
                    RecordStatus = (int)RecordStatus.Active
                });
                service.Add(entity);
                return RedirectToAction("Index", "Maintenance");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Delete(Guid id)
        {
            service.Delete(id);
            return RedirectToAction("Index", "Maintenance");
        }
    }
}