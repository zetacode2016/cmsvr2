using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class ClusterController : AdminBaseController
    {
        private ClusterBL service = new ClusterBL();
        public ActionResult Index()
        {
            var list = service.FilterActive();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.BranchCorpOffices = new SelectList(service.GetBranchCorpOffices(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Cluster model)
        {
            ViewBag.BranchCorpOffices = new SelectList(service.GetBranchCorpOffices(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            if (ModelState.IsValid)
            {
                model.ClusterId = Guid.NewGuid();
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
            ViewBag.BranchCorpOffices = new SelectList(service.GetBranchCorpOffices(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            var cluster = service.GetById(id);
            return View(cluster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cluster model)
        {
            ViewBag.BranchCorpOffices = new SelectList(service.GetBranchCorpOffices(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.ClusterId);
                _model.ClusterId = model.ClusterId;
                _model.BranchCorpOfficeId = model.BranchCorpOfficeId;
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