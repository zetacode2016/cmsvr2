using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class UnitOfMeasureController : AdminBaseController
    {
        private UnitOfMeasureBL service = new UnitOfMeasureBL();

        public ActionResult Index()
        {
            List<UnitOfMeasure> list = new List<UnitOfMeasure>();
            list = service.GetAllAsync().Result;
            return View(list);
        }

        [HttpGet]
        public ActionResult InputForm(Guid? id)
        {
            ViewBag.UoMTypes = new SelectList(service.GetUnitOfMeaseureTypes(), "UnitOfMeasureTypeId", "UnitOfMeasureTypeName");

            Guid _id;
            UnitOfMeasure model = new UnitOfMeasure();
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
                RecordStatus recordStatus = (RecordStatus)model.RecordStatus;
                model.Record_Status = recordStatus;
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputForm(UnitOfMeasure model)
        {
            ViewBag.UoMTypes = new SelectList(service.GetUnitOfMeaseureTypes(), "UnitOfMeasureTypeId", "UnitOfMeasureTypeName");

            if (ModelState.IsValid)
            {
                model.RecordStatus = (int)model.Record_Status;
                var result = service.AddEdit(model);
                if (result == null)
                {
                    return RedirectToAction("Index", "UnitOfMeasure");
                }
                else
                {
                    ModelState.AddModelError("Data Error", "Invalid/duplicate Unit Of Measure");
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