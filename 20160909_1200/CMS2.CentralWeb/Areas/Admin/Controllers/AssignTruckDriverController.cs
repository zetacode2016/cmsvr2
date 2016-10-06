using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class AssignTruckDriverController : AdminBaseController
    {
        private TruckDriverMappingBL service = new TruckDriverMappingBL();

        public ActionResult Index()
        {
            List<TruckDriverMapping> list = new List<TruckDriverMapping>();
            list = service.FilterActive();
            return View(list);
        }

        public ActionResult Add()
        {
            var drivers = service.GetDrivers();
            List<SelectListItem> driverList = new List<SelectListItem>();
            foreach (var item in drivers)
            {
                driverList.Add(new SelectListItem { Text = item.Key, Value = item.Value.ToString() });
            }
            ViewBag.Drivers = new SelectList(driverList, "Value", "Text");
            ViewBag.Trucks = new SelectList(service.GetTrucks().OrderBy(x=>x.PlateNo).ToList(), "TruckId", "PlateNo");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TruckDriverMapping model)
        {
            var drivers = service.GetDrivers();
            List<SelectListItem> driverList = new List<SelectListItem>();
            foreach (var item in drivers)
            {
                driverList.Add(new SelectListItem {Text = item.Key, Value = item.Value.ToString()});
            }
            ViewBag.Drivers = new SelectList(driverList, "Value", "Text", model.EmployeeId);
            ViewBag.Trucks = new SelectList(service.GetTrucks().OrderBy(x => x.PlateNo).ToList(), "TruckId", "PlateNo", model.TruckId);

            if (ModelState.IsValid)
            {
                model.TruckDriverMappingId = Guid.NewGuid();
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int) RecordStatus.Active;
                service.Add(model);
                return RedirectToAction("Index", "AssignTruckDriver");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGetAttribute]
        public ActionResult Edit(Guid id)
        {
            var model = service.GetById(id);
           var drivers = service.GetDrivers();
            List<SelectListItem> driverList = new List<SelectListItem>();
            foreach (var item in drivers)
            {
                driverList.Add(new SelectListItem {Text = item.Key, Value = item.Value.ToString()});
            }
            ViewBag.Drivers = new SelectList(driverList, "Value", "Text", model.EmployeeId);
            ViewBag.Trucks = new SelectList(service.GetTrucks().OrderBy(x => x.PlateNo).ToList(), "TruckId", "PlateNo", model.TruckId);
            return View(model);
        }

        [HttpPostAttribute]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Edit(TruckDriverMapping model)
        {
            var drivers = service.GetDrivers();
            List<SelectListItem> driverList = new List<SelectListItem>();
            foreach (var item in drivers)
            {
                driverList.Add(new SelectListItem {Text = item.Key, Value = item.Value.ToString()});
            }
            ViewBag.Drivers = new SelectList(driverList, "Value", "Text", model.EmployeeId);
            ViewBag.Trucks = new SelectList(service.GetTrucks().OrderBy(x => x.PlateNo).ToList(), "TruckId", "PlateNo", model.TruckId);

            if (ModelState.IsValid)
            {
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                service.Edit(model);
                return RedirectToAction("Index", "AssignTruckDriver");
            }
            else
            {
                return View(model);
            }
        }
    }
}