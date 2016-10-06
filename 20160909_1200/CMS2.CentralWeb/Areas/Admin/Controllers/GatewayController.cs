using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class GatewayController : AdminBaseController
    {
        private GatewayBL service = new GatewayBL();

        public ActionResult Index()
        {
            var list = service.FilterActive();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.GatewayTypes = new SelectList(service.GetGatewayTypes(), "GatewayTypeId", "GatewayTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Gateway model)
        {
            ViewBag.GatewayTypes = new SelectList(service.GetGatewayTypes(), "GatewayTypeId", "GatewayTypeName",model.GatewayTypeId);
            if (ModelState.IsValid)
            {
                model.GatewayId = Guid.NewGuid();
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
            ViewBag.GatewayTypes = new SelectList(service.GetGatewayTypes(), "GatewayTypeId", "GatewayTypeName", model.GatewayTypeId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gateway model)
        {
            ViewBag.GatewayTypes = new SelectList(service.GetGatewayTypes(), "GatewayTypeId", "GatewayTypeName", model.GatewayTypeId);
            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.GatewayId);
                _model.GatewayCode = model.GatewayCode;
                _model.GatewayName = model.GatewayName;
                _model.GatewayTypeId = model.GatewayTypeId;
                _model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                _model.ModifiedDate = DateTime.Now;
                _model.RecordStatus = (int)RecordStatus.Active;
                service.Edit(_model);
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
