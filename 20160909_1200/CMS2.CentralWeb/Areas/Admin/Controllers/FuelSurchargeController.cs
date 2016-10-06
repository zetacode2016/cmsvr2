using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class FuelSurchargeController : AdminBaseController
    {
        private FuelSurchargeBL service = new FuelSurchargeBL();

        public ActionResult Index()
        {
            List<FuelSurcharge> list = new List<FuelSurcharge>();
            list = service.GetAllAsync().Result;
            return View(list);
        }

        [HttpGet]
        public ActionResult InputForm(Guid? id)
        {
            ViewBag.Groups = new SelectList(service.GetGroups(), "GroupId", "GroupName");

            Guid _id;
            FuelSurcharge model = new FuelSurcharge();
            string pageTitle = "";
            if (id == null)
            {
                ViewBag.IsNew = true;
                return View(model);
            }
            else
            {
                _id = Guid.Parse(id.ToString());
                model = service.GetByIdAsync(_id).Result;
                ViewBag.IsNew = false;
                RecordStatus recordStatus = (RecordStatus)model.RecordStatus;
                model.Record_Status = recordStatus;
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputForm(FuelSurcharge model)
        {
            ViewBag.Groups = new SelectList(service.GetGroups(), "GroupId", "GroupName");

            if (ModelState.IsValid)
            {
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)model.Record_Status;
                var result = service.AddEdit(model);
                if (result == null)
                {
                    return RedirectToAction("Index", "FuelSurcharge");
                }
                else
                {
                    ModelState.AddModelError("Data Error", "Invalid/duplicate Fuel Surcharge");
                    ViewBag.IsNew = false;
                    return View(model);
                }
            }
            else
            {
                ViewBag.IsNew = false;
                return View(model);
            }
        }

        public ActionResult Delete(Guid id)
        {
            service.Delete(id);
            return RedirectToAction("Index", "FuelSurcharge");
        }
    }
}