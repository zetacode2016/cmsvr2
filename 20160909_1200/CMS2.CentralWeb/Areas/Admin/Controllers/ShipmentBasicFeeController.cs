using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class ShipmentBasicFeeController : AdminBaseController
    {
        private ShipmentBasicFeeBL service = new ShipmentBasicFeeBL();

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
        public ActionResult Add(ShipmentBasicFee model)
        {
            if (ModelState.IsValid)
            {
                model.ShipmentBasicFeeId = Guid.NewGuid();
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
        public ActionResult Edit(ShipmentBasicFee model)
        {
            if (ModelState.IsValid)
            {
                // TODO do some testing with this function
                var _model = service.GetById(model.ShipmentBasicFeeId);
                if (_model.EffectiveDate == model.EffectiveDate && _model.Amount==model.Amount && _model.IsVatable==model.IsVatable)
                {
                    _model.ShipmentFeeName = model.ShipmentFeeName;
                    _model.Description = model.Description;
                    _model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    _model.ModifiedDate = DateTime.Now;
                    _model.RecordStatus = (int) RecordStatus.Active;
                    service.Edit(_model);
                }
                else
                {
                    model.ShipmentBasicFeeId = Guid.NewGuid();
                    model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    model.ModifiedDate = DateTime.Now;
                    model.RecordStatus = (int)RecordStatus.Active;
                    service.Add(model);
                }
                
                
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