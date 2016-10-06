using System;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Admin.ViewModels;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class CityController : AdminBaseController
    {
        private CityBL service = new CityBL();

        public ActionResult Index()
        {
            var list = service.FilterActive();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.BCOs = new SelectList(service.GetBranchCorpOffices().OrderBy(x=>x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CityViewModel model)
        {
            ViewBag.BCOs = new SelectList(service.GetBranchCorpOffices().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            if (ModelState.IsValid)
            {
                City _model = new City();
                _model.CityId = Guid.NewGuid();
                _model.CityCode = model.CityCode;
                _model.CityName = model.CityName;
                _model.BranchCorpOfficeId =model.BranchCorpOfficeId;
                _model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                _model.CreatedDate = DateTime.Now;
                _model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                _model.ModifiedDate = DateTime.Now;
                _model.RecordStatus = (int)RecordStatus.Active;
                service.Add(_model);
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
            ViewBag.BCOs = new SelectList(service.GetBranchCorpOffices().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            var model = service.GetById(id);
            CityViewModel vm = new CityViewModel();
            vm.CityId = model.CityId;
            vm.CityCode = model.CityCode;
            vm.CityName = model.CityName;
            vm.BranchCorpOfficeId = model.BranchCorpOfficeId;
            
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CityViewModel model)
        {
            ViewBag.BCOs = new SelectList(service.GetBranchCorpOffices().OrderBy(x => x.BranchCorpOfficeName).ToList(), "BranchCorpOfficeId", "BranchCorpOfficeName");
            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.CityId);
                _model.CityCode = model.CityCode;
                _model.CityName = model.CityName;
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

        public ActionResult GetCities()
        {
            var result = service.FilterActive().OrderBy(x => x.CityName).Select(x => new { value = x.CityId, text = x.CityName }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}