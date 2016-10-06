using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class AssignTruckAreaController : AdminBaseController
    {
        private TruckAreaMappingBL service = new TruckAreaMappingBL();

        public ActionResult Index()
        {
            List<TruckAreaMapping> list = new List<TruckAreaMapping>();
            list = service.FilterActive();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Areas = new SelectList(service.GetAreas().OrderBy(x=>x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName");
            ViewBag.Trucks = new SelectList(service.GetTrucks(), "TruckId", "PlateNo");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TruckAreaMapping model)
        {
            ViewBag.Areas = new SelectList(service.GetAreas().OrderBy(x => x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName");
            ViewBag.Trucks = new SelectList(service.GetTrucks(), "TruckId", "PlateNo");

            if (ModelState.IsValid)
            {
                model.TruckAreaMappingId = Guid.NewGuid();
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;
                service.Add(model);
                return RedirectToAction("Index", "AssignTruckArea");
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
            ViewBag.Areas = new SelectList(service.GetAreas().OrderBy(x => x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName", model.RevenueUnitId);
            ViewBag.Trucks = new SelectList(service.GetTrucks(), "TruckId", "PlateNo", model.TruckId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Edit(TruckAreaMapping model)
        {
            ViewBag.Areas = new SelectList(service.GetAreas().OrderBy(x => x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName", model.RevenueUnitId);
            ViewBag.Trucks = new SelectList(service.GetTrucks(), "TruckId", "PlateNo", model.TruckId);

            if (ModelState.IsValid)
            {
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;
                service.Edit(model);
                return RedirectToAction("Index", "AssignTruckArea");
            }
            else
            {
                return View(model);
            }
        }
    }
}
