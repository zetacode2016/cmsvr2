using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class EmployeeController : AdminBaseController
    {
        private EmployeeBL service;

        public EmployeeController()
        {
            service = new EmployeeBL();
        }

        public ActionResult Index()
        {
            var list = service.FilterActive();
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Employee model)
        {
            if (service.IsExist(x => x.FirstName.Equals(model.FirstName) && x.LastName.Equals(model.LastName)))
            {
                ModelState.AddModelError("Data Error", "Employee already exist with the same name.");
            }
            if (ModelState.IsValid)
            {
                model.EmployeeId = Guid.NewGuid();
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;
                var result = service.Add(model);
                if (result != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Data Error", "Failed to add new employee information");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            Employee model = new Employee();
            model = service.GetById(id);
                return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;
                service.Edit(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Details(Guid id)
        {
            Employee model = new Employee();
            model = service.GetById(id);
            RecordStatus recordStatus = (RecordStatus)model.RecordStatus;
            model.Record_Status = recordStatus;
            return View(model);
        }
    }
}