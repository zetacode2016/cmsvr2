using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class GroupController : AdminBaseController
    {
        // TODO: consider the approach below
        // use IoC implemented by DependencyInjection
        //private IAPCargoBL<Group> service;
        //public AreaController(IAPCargoBL<Group> groupService)
        //{
        //    this.service = groupService;
        //}

        private GroupBL service = new GroupBL();

        public ActionResult Index()
        {
            List<Group> list = new List<Group>();
            list = service.FilterActive();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Group model)
        {
            if (ModelState.IsValid)
            {
                model.GroupId = Guid.NewGuid();
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
            var group = service.GetById(id);
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Group model)
        {
            if (ModelState.IsValid)
            {
                var _model = service.GetById(model.GroupId);
                _model.GroupName = model.GroupName;
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