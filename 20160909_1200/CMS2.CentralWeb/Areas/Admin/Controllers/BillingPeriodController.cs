using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class BillingPeriodController : AdminBaseController
    {
        private BillingPeriodBL service = new BillingPeriodBL();

        public ActionResult Index()
        {
            List<BillingPeriod> list = new List<BillingPeriod>();
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
        public ActionResult Add(BillingPeriod model)
        {
            if (ModelState.IsValid)
            {
                if (service.IsExist(x => x.BillingPeriodName.Equals(model.BillingPeriodName)))
                {
                    ModelState.AddModelError("Data Error","Billing period already exist.");
                    return View(model);
                }
                else if (model.NumberOfDays % 7 != 0)
                {
                    ModelState.AddModelError("Data Error", "Billing period is invalid.");
                    return View(model);
                }
                else
                {
                    model.BillingPeriodId = Guid.NewGuid();
                    model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    model.ModifiedDate = DateTime.Now;
                    model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                    model.CreatedDate = DateTime.Now;
                    model.RecordStatus=(int)RecordStatus.Active;
                    Add(model);
                    return RedirectToAction("Index", "Maintenance");    
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
            var model = service.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BillingPeriod model)
        {
            if (ModelState.IsValid)
            {
                if (model.NumberOfDays % 7 != 0)
                {
                    ModelState.AddModelError("Data Error", "Billing period is invalid.");
                    return View(model);
                }
                else
                {
                    model.BillingPeriodId = Guid.NewGuid();
                    model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    model.ModifiedDate = DateTime.Now;
                    model.RecordStatus = (int)RecordStatus.Active;
                    Add(model);
                    return RedirectToAction("Index", "Maintenance");
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var model = service.GetById(id);
            model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
            model.ModifiedDate = DateTime.Now;
            model.RecordStatus = (int) RecordStatus.Deleted;
            Edit(model);
            return RedirectToAction("Index", "Maintenance");

        }
    }
}