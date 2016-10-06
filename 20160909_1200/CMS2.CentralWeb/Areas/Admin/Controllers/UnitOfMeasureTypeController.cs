using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class UnitOfMeasureTypeController : AdminBaseController
    {
        private UnitOfMeasureTypeBL service = new UnitOfMeasureTypeBL();

        public ActionResult Index()
        {
            List<UnitOfMeasureType> list = new List<UnitOfMeasureType>();
            list = service.GetAllAsync().Result;
            return View(list);
        }

        [HttpGet]
        public ActionResult InputForm(Guid? id)
        {
            Guid _id;
            UnitOfMeasureType model = new UnitOfMeasureType();
            string pageTitle = "";
            if (id == null)
            {
                ViewBag.IsNew = true;
                model.Record_Status = RecordStatus.Active;
                return View(model);
            }
            else
            {
                _id = Guid.Parse(id.ToString());
                model = service.GetByIdAsync(_id).Result;
                ViewBag.IsNew = false;
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputForm(UnitOfMeasureType model)
        {
            if (ModelState.IsValid)
            {
                model.RecordStatus = (int)model.Record_Status;
                var result = service.AddEdit(model);
                if (result == null)
                {
                    return RedirectToAction("Index", "UnitOfMeasureType");
                }
                else
                {
                    ModelState.AddModelError("Data Error", "Invalid/duplicate Unit Of Measure Type");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}