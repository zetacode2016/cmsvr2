using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class BranchCorpOfficeController : AdminBaseController
    {
        private BranchCorpOfficeBL service = new BranchCorpOfficeBL();

        public ActionResult Index()
        {
            var list = service.FilterActive();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Regions = new SelectList(service.GetRegions(), "RegionId", "RegionName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BranchCorpOffice model)
        {
            ViewBag.Regions = new SelectList(service.GetRegions(), "RegionId", "RegionName");
            if (ModelState.IsValid)
            {
                model.BranchCorpOfficeId = Guid.NewGuid();
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
            ViewBag.Regions = new SelectList(service.GetRegions(), "RegionId", "RegionName");
            var model = service.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BranchCorpOffice model)
        {
            ViewBag.Regions = new SelectList(service.GetRegions(), "RegionId", "RegionName");
            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.BranchCorpOfficeId);
                _model.BranchCorpOfficeName = model.BranchCorpOfficeName;
                _model.StreetAddress = model.StreetAddress;
                _model.ContactNo1 = model.ContactNo1;
                _model.ContactNo2 = model.ContactNo2;
                _model.Fax = model.Fax;
                _model.RegionId = model.RegionId;
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
