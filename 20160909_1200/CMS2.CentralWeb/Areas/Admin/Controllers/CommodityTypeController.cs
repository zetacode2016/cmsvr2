using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class CommodityTypeController : AdminBaseController
    {
        private CommodityTypeBL service = new CommodityTypeBL();

        public ActionResult Index()
        {
            var list = service.FilterActive();
            return View(list);
        }
        
        [HttpGet]
        public ActionResult Add()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CommodityType model)
        {
            
            if (ModelState.IsValid)
            {
                model.CommodityTypeId = Guid.NewGuid();
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;

                service.Add(model);
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
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommodityType model)
        {
            if (ModelState.IsValid)
            {
                var entity = service.GetById(model.CommodityTypeId);
                if (entity != null)
                {
                    entity.CommodityTypeCode = model.CommodityTypeCode;
                    entity.CommodityTypeName = model.CommodityTypeName;
                    entity.CommodityTypeDesc = model.CommodityTypeDesc;
                    entity.EvmDivisor = model.EvmDivisor;
                    entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    entity.ModifiedDate = DateTime.Now;
                    entity.RecordStatus = (int)RecordStatus.Active;
                    service.Edit(entity);

                    return RedirectToAction("Index", "Maintenance");
                }
                else
                {
                    ModelState.AddModelError("Data Error","Cannot edit Commodity Type");
                    return View(model);
                }
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