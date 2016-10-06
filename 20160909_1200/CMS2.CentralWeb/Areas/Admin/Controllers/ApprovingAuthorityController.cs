using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class ApprovingAuthorityController : Controller
    {
        private ApprovingAuthorityBL service = new ApprovingAuthorityBL();

        public ActionResult Index(Guid? id) // id is CompanyId
        {
            List<ApprovingAuthority> models;
            if (id == null)
            {
                models = service.FilterActive();
            }
            else
            {
                models = service.FilterActiveBy(x => x.CompanyId == id);
            }
            return View(models);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Companies = new SelectList(service.GetCompanies(), "CompanyId", "CompanyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ApprovingAuthority model)
        {
            ViewBag.Companies = new SelectList(service.GetCompanies(), "CompanyId", "CompanyName");
            if (ModelState.IsValid)
            {
                model.ApprovingAuthorityId = Guid.NewGuid();
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.CreatedBy = Guid.Parse((User.Identity.GetUserId()));
                model.CreatedDate = DateTime.Now;
                model.RecordStatus = (int) RecordStatus.Active;
                service.Add(model);
                return RedirectToAction("Index");
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
            ViewBag.Companies = new SelectList(service.GetCompanies(), "CompanyId", "CompanyName",model.CompanyId);
            return View(model);
        }

        public ActionResult Edit(ApprovingAuthority model)
        {
            ViewBag.Companies = new SelectList(service.GetCompanies(), "CompanyId", "CompanyName", model.CompanyId);
            if (ModelState.IsValid)
            {
                var entity = service.GetById(model.ApprovingAuthorityId);
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Title = model.Title;
                entity.Position = model.Position;
                entity.Department = model.Department;
                entity.ContactNo = model.ContactNo;
                entity.Mobile = model.Mobile;
                entity.Fax = model.Fax;
                entity.Email = model.Email;
                entity.CompanyId = model.CompanyId;
                entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                entity.ModifiedDate = DateTime.Now;
                entity.RecordStatus = (int)RecordStatus.Active;
                service.Edit(entity);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Delete(Guid id)
        {
            var model = service.GetById(id);
            model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
            model.ModifiedDate = DateTime.Now;
            model.RecordStatus = (int)RecordStatus.Deleted;
            service.Edit(model);
            return RedirectToAction("Index"); 
        }

        public ActionResult Details(Guid id)
        {
            var model = service.GetById(id);
            return View(model);
        }
    }
}