using System;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class CommodityController : AdminBaseController
    {
        private CommodityBL service = new CommodityBL();
        private CommodityTypeBL commodityTypeService = new CommodityTypeBL();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.CommodityTypes = new SelectList(commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList(), "CommodityTypeId", "CommodityTypeName", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Commodity entity)
        {
            ViewBag.CommodityTypes = new SelectList(commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList(), "CommodityTypeId", "CommodityTypeName", entity.CommodityTypeId);

            if (ModelState.IsValid)
            {
                entity.CommodityId = Guid.NewGuid();
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                entity.ModifiedDate = DateTime.Now;
                entity.RecordStatus = (int)RecordStatus.Active;

                service.Add(entity);
                return RedirectToAction("Index", "Maintenance");
            }
            else
            {
                return View(entity);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {

            var model = service.GetById(id);
            ViewBag.CommodityTypes = new SelectList(commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList(), "CommodityTypeId", "CommodityTypeName", model.CommodityTypeId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Commodity model)
        {
            ViewBag.CommodityTypes = new SelectList(commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList(), "CommodityTypeId", "CommodityTypeName", model.CommodityTypeId);
            if (ModelState.IsValid)
            {
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                service.Edit(model);

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