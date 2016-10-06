using System;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class CratingController : AdminBaseController
    {
        private CratingBL service = new CratingBL();

        //public ActionResult Index()
        //{
        //    var list = service.FilterActive();
        //    return View(list);
        //}

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Crating model)
        {
            if (ModelState.IsValid)
            {
                model.CratingId = Guid.NewGuid();
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
        public ActionResult Edit(Crating model)
        {
            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.CratingId);
                _model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                _model.ModifiedDate = DateTime.Now;
                _model.RecordStatus = (int)RecordStatus.Deleted;
                service.Edit(_model);

                Crating entity = new Crating();
                entity.CratingId = Guid.NewGuid();
                entity.CratingName = model.CratingName;
                entity.BaseMinimum = model.BaseMinimum;
                entity.BaseMaximum = model.BaseMaximum;
                entity.BaseFee = model.BaseFee;
                entity.ExcessCost = model.ExcessCost;
                entity.Multiplier = model.Multiplier;
                entity.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                entity.ModifiedDate = DateTime.Now;
                entity.RecordStatus = (int)RecordStatus.Active;
                service.Add(model);

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