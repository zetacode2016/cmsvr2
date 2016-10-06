using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class ExpressRateController : AdminBaseController
    {
        private ExpressRateBL service = new ExpressRateBL();

        public ActionResult Index()
        {
            var list = service.FilterActive();
            return View(list);
        }
        
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.OriginCities = new SelectList(service.GetCities(), "CityId", "CityName");
            ViewBag.DestinationCities = new SelectList(service.GetCities(), "CityId", "CityName");
            ViewBag.Commodities = new SelectList(service.GetCommodities(), "CommodityTypeId", "CommodityTypeName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ExpressRate model)
        {
            ViewBag.OriginCities = new SelectList(service.GetCities(), "CityId", "CityName");
            ViewBag.DestinationCities = new SelectList(service.GetCities(), "CityId", "CityName");
            ViewBag.Commodities = new SelectList(service.GetCommodities(), "CommodityTypeId", "CommodityTypeName");

            if (ModelState.IsValid)
            {
                model.ExpressRateId = Guid.NewGuid();
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;
                service.Add(model);
                return RedirectToAction("Index", "ExpressRate");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            ViewBag.OriginCities = new SelectList(service.GetCities(), "CityId", "CityName");
            ViewBag.DestinationCities = new SelectList(service.GetCities(), "CityId", "CityName");
            ViewBag.Commodities = new SelectList(service.GetCommodities(), "CommodityTypeId", "CommodityTypeName");

            var model = service.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExpressRate model)
        {
            ViewBag.OriginCities = new SelectList(service.GetCities(), "CityId", "CityName");
            ViewBag.DestinationCities = new SelectList(service.GetCities(), "CityId", "CityName");
            ViewBag.Commodities = new SelectList(service.GetCommodities(), "CommodityTypeId", "CommodityTypeName");

            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.ExpressRateId);
                _model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                _model.ModifiedDate = DateTime.Now;
                _model.RecordStatus = (int)RecordStatus.Deleted;
                service.Edit(_model);

                model.ExpressRateId = Guid.NewGuid();
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;
                service.Add(model);

                return RedirectToAction("Index", "ExpressRate");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Delete(Guid id)
        {
            service.Delete(id);
            return RedirectToAction("Index", "ExpressRate");
        }
    }
}