using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class RegionController : AdminBaseController
    {
        private RegionBL service = new RegionBL();

        public ActionResult Index()
        {
            var list = service.FilterActive();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Groups = new SelectList(service.GetGroups(), "GroupId", "GroupName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Region model)
        {
            ViewBag.Groups = new SelectList(service.GetGroups(), "GroupId", "GroupName");
            if (ModelState.IsValid)
            {
                model.RegionId = Guid.NewGuid();
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
            ViewBag.Groups = new SelectList(service.GetGroups(), "GroupId", "GroupName");
            var model = service.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Region model)
        {
            ViewBag.Groups = new SelectList(service.GetGroups(), "GroupId", "GroupName");
            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.RegionId);
                _model.RegionName = model.RegionName;
                _model.GroupId = model.GroupId;
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