using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class TruckController : AdminBaseController
    {
        private TruckBL service = new TruckBL();

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
        public ActionResult Add(Truck model)
        {
            if (ModelState.IsValid)
            {
                model.TruckId = Guid.NewGuid();
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;
                service.Add(model);
                return RedirectToAction("Index", "Truck");
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
        public ActionResult Edit(Truck model)
        {
            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.TruckId);
                _model.TruckModel = model.TruckModel;
                _model.PlateNo = model.PlateNo;
                _model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                _model.ModifiedDate = DateTime.Now;
                _model.RecordStatus = (int)RecordStatus.Active;
                service.Edit(_model);
                return RedirectToAction("Index", "Truck");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Delete(Guid id)
        {
            service.Delete(id);
            return RedirectToAction("Index", "Truck");
        }
    }
}