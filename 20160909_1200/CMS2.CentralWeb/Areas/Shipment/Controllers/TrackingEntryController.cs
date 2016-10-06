using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Shipment.ViewModels;
using CMS2.DataAccess;
using CMS2.Entities;

namespace CMS2.CentralWeb.Areas.Shipment.Controllers
{
    public class TrackingEntryController : ShipmentBaseController
    {
        private PackageNumberBL service = new PackageNumberBL(new CmsUoW());

        public ActionResult Index()
        {
            var employees = service.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();

            foreach (var item in employees)
            {
                employeeList.Add(new SelectListItem { Text = item.FullName, Value = item.EmployeeId.ToString() });
            }
            ViewBag.Employees = new SelectList(employeeList.OrderBy(x => x.Text), "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Index(TrackingEntryViewModel vm)
        {
            var employees = service.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();

            foreach (var item in employees)
            {
                employeeList.Add(new SelectListItem { Text = item.FullName, Value = item.EmployeeId.ToString() });
            }
            ViewBag.Employees = new SelectList(employeeList, "Value", "Text", vm.EmployeeId);

            vm.DateFrom = new DateTime(vm.DateFrom.Year, vm.DateFrom.Month, vm.DateFrom.Day, 0, 0, 0);
            vm.DateUntil = new DateTime(vm.DateUntil.Year, vm.DateUntil.Month, vm.DateUntil.Day, 23, 59, 59);
            List<PackageNumber> packageNumbers = service.FilterActiveBy(x => x.ScannedById == vm.EmployeeId && x.Shipment.DateAccepted >= vm.DateFrom && x.Shipment.DateAccepted <= vm.DateUntil).OrderBy(x => x.Shipment.AirwayBillNo).ToList();
            if (packageNumbers != null)
            {
                vm.TrackingEntryDetailsViewModels = new List<TrackingEntryDetailsViewModel>();
                foreach (var item in packageNumbers)
                {
                    TrackingEntryDetailsViewModel model;
                    if (vm.TrackingEntryDetailsViewModels.Count>0 && item.Shipment.AirwayBillNo.Equals(vm.TrackingEntryDetailsViewModels.Last().AirwayBilllNo))
                    {
                        model = new TrackingEntryDetailsViewModel();
                        model.EmployeeName = "";
                        model.Date = "";
                        model.AirwayBilllNo = "";
                        model.CargoNo = item.PackageNo;
                        model.Quantity = "";
                    }
                    else
                    {
                        model = new TrackingEntryDetailsViewModel();
                        model.EmployeeName = item.ScannedBy.FullName;
                        model.Date = item.Shipment.DateAccepted.ToString("MMM dd, yyyy");
                        model.AirwayBilllNo = item.Shipment.AirwayBillNo;
                        model.CargoNo = item.PackageNo;
                        model.Quantity = item.Shipment.Quantity.ToString();
                    }

                    vm.TrackingEntryDetailsViewModels.Add(model);
                }
            }
            return View(vm);
        }
    }
}
