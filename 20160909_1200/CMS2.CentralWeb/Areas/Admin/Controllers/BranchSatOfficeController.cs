using System;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Constants;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class BranchSatOfficeController : AdminBaseController
    {
        private BranchSatOfficeBL service = new BranchSatOfficeBL();

        public ActionResult Index()
        {
            var list = service.FilterActive();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Cities = new SelectList(service.GetCities().OrderBy(x => x.CityName).ToList(), "CityId", "CityName");
            ViewBag.Clusters = new SelectList(service.GetClusters().OrderBy(x => x.ClusterName).ToList(), "ClusterId", "ClusterName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(RevenueUnit model)
        {
            ViewBag.Cities = new SelectList(service.GetCities().OrderBy(x => x.CityName).ToList(), "CityId", "CityName");
            ViewBag.Clusters = new SelectList(service.GetClusters().OrderBy(x => x.ClusterName).ToList(), "ClusterId", "ClusterName");
            if (ModelState.IsValid)
            {
                model.RevenueUnitId = Guid.NewGuid();
                model.RevenueUnitTypeId = Guid.Parse(RevenueUnitTypeId.BSO);
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
            ViewBag.Cities = new SelectList(service.GetCities().OrderBy(x => x.CityName).ToList(), "CityId", "CityName");
            ViewBag.Clusters = new SelectList(service.GetClusters().OrderBy(x => x.ClusterName).ToList(), "ClusterId", "ClusterName");
            var model = service.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RevenueUnit model)
        {
            ViewBag.Cities = new SelectList(service.GetCities().OrderBy(x => x.CityName).ToList(), "CityId", "CityName");
            ViewBag.Clusters = new SelectList(service.GetClusters().OrderBy(x => x.ClusterName).ToList(), "ClusterId", "ClusterName");
            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.RevenueUnitId);
                _model.RevenueUnitName = model.RevenueUnitName;
                _model.ClusterId = model.ClusterId;
                _model.CityId = model.CityId;
                _model.StreetAddress = model.StreetAddress;
                _model.ContactNo1 = model.ContactNo1;
                _model.ContactNo2 = model.ContactNo2;
                _model.Fax = model.Fax;
                _model.CityId = model.CityId;
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