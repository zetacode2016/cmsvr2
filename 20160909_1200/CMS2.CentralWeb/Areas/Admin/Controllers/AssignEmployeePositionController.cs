using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Admin.ViewModels;
using CMS2.Common.Constants;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class AssignEmployeePositionController : AdminBaseController
    {
        private EmployeePositionMappingBL service;
        private PositionBL positionService;
        private DepartmentBL departmentService;
        private AreaBL areaService;
        private BranchSatOfficeBL bsoService;
        private GatewaySatOfficeBL gatewaySatService;
        private BranchCorpOfficeBL bcoService;
        private EmployeeBL employeeService;

        public AssignEmployeePositionController()
        {
            service = new EmployeePositionMappingBL();
            positionService = new PositionBL(service.GetUnitOfWork());
            departmentService = new DepartmentBL(service.GetUnitOfWork());
            areaService = new AreaBL(service.GetUnitOfWork());
            bsoService = new BranchSatOfficeBL(service.GetUnitOfWork());
            gatewaySatService = new GatewaySatOfficeBL(service.GetUnitOfWork());
            bcoService = new BranchCorpOfficeBL(service.GetUnitOfWork());
            employeeService = new EmployeeBL(service.GetUnitOfWork());
        }

        public ActionResult Index()
        {
            List<EmployeePositionMapping> list = new List<EmployeePositionMapping>();
            list = service.FilterActive();
            return View(ModelsToViewModels(list));
        }

        public ActionResult Add()
        {
            var employees = employeeService.GetEmployeeNames();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            foreach (var item in employees)
            {
                employeeList.Add(new SelectListItem { Text = item.Key, Value = item.Value.ToString() });
            }
            ViewBag.Employees = new SelectList(employeeList, "Value", "Text");
            ViewBag.Positions = new SelectList(positionService.FilterActive(), "PositionId", "PositionName");
            ViewBag.Departments = new SelectList(departmentService.FilterActive(), "DepartmentId", "DepartmentName");
            ViewBag.AssignLocations = new SelectList(Locations(), "Value", "Text");
            ViewBag.LocationNames = new SelectList(LocationNames(), "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(EmployeePositionMapping model)
        {
            var employees = employeeService.GetEmployeeNames();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            foreach (var item in employees)
            {
                employeeList.Add(new SelectListItem { Text = item.Key, Value = item.Value.ToString() });
            }
            ViewBag.Employees = new SelectList(employeeList, "Value", "Text");
            ViewBag.Positions = new SelectList(positionService.FilterActive(), "PositionId", "PositionName");
            ViewBag.Departments = new SelectList(departmentService.FilterActive(), "DepartmentId", "DepartmentName");
            ViewBag.AssignLocations = new SelectList(Locations(), "Value", "Text", model.LocationAssignment);
            switch (model.LocationAssignment)
            {
                case AssignLocationConstant.Area:
                    ViewBag.LocationNames = new SelectList(areaService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName }), "Value", "Text", model.AssignedLocationId);
                    break;
                case AssignLocationConstant.BSO:
                    ViewBag.LocationNames = new SelectList(bsoService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName }), "Value", "Text", model.AssignedLocationId);
                    break;
                case AssignLocationConstant.GatewaySat:
                    ViewBag.LocationNames = new SelectList(gatewaySatService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName }), "Value", "Text", model.AssignedLocationId);
                    break;
                case AssignLocationConstant.BCO:
                    ViewBag.LocationNames = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).Select(x => new { value = x.BranchCorpOfficeId, text = x.BranchCorpOfficeName }), "Value", "Text", model.AssignedLocationId);
                    break;
            }
            if (ModelState.IsValid)
            {
                model.EmployeePositionMappingId = Guid.NewGuid();
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;
                service.Add(model);
                return RedirectToAction("Index", "AssignEmployeePosition");
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

            model.Employee = employeeService.GetById(model.EmployeeId);
            ViewBag.Positions = new SelectList(positionService.FilterActive(), "PositionId", "PositionName", model.PositionId);
            ViewBag.Departments = new SelectList(departmentService.FilterActive(), "DepartmentId", "DepartmentName", model.DepartmentId);
            ViewBag.AssignLocations = new SelectList(Locations(), "Value", "Text",model.LocationAssignment);
            switch (model.LocationAssignment)
            {
                case AssignLocationConstant.Area:
                    ViewBag.LocationNames = new SelectList(areaService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName }), "Value", "Text", model.AssignedLocationId);
                    break;
                case AssignLocationConstant.BSO:
                    ViewBag.LocationNames = new SelectList(bsoService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName }), "Value", "Text", model.AssignedLocationId);
                    break;
                case AssignLocationConstant.GatewaySat:
                    ViewBag.LocationNames = new SelectList(gatewaySatService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName }), "Value", "Text", model.AssignedLocationId);
                    break;
                case AssignLocationConstant.BCO:
                    ViewBag.LocationNames = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).Select(x => new { value = x.BranchCorpOfficeId, text = x.BranchCorpOfficeName }), "Value", "Text", model.AssignedLocationId);
                    break;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeePositionMapping model)
        {
            ViewBag.Positions = new SelectList(positionService.FilterActive(), "PositionId", "PositionName", model.PositionId);
            ViewBag.Departments = new SelectList(departmentService.FilterActive(), "DepartmentId", "DepartmentName", model.DepartmentId);
            ViewBag.AssignLocations = new SelectList(Locations(), "Value", "Text", model.LocationAssignment);
            switch (model.LocationAssignment)
            {
                case AssignLocationConstant.Area:
                    ViewBag.LocationNames = new SelectList(areaService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName }), "Value", "Text", model.AssignedLocationId);
                    break;
                case AssignLocationConstant.BSO:
                    ViewBag.LocationNames = new SelectList(bsoService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName }), "Value", "Text", model.AssignedLocationId);
                    break;
                case AssignLocationConstant.GatewaySat:
                    ViewBag.LocationNames = new SelectList(gatewaySatService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName }), "Value", "Text", model.AssignedLocationId);
                    break;
                case AssignLocationConstant.BCO:
                    ViewBag.LocationNames = new SelectList(bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).Select(x => new { value = x.BranchCorpOfficeId, text = x.BranchCorpOfficeName }), "Value", "Text", model.AssignedLocationId);
                    break;
            }
            if (ModelState.IsValid)
            {
                EmployeePositionMapping _model = new EmployeePositionMapping();
                _model.EmployeePositionMappingId = Guid.NewGuid();
                _model.EmployeeId = model.EmployeeId;
                _model.DepartmentId = model.DepartmentId;
                _model.PositionId = model.PositionId;
                _model.AssignedLocationId = model.AssignedLocationId;
                _model.AssignedLocation = model.AssignedLocation;
                _model.LocationAssignment = model.LocationAssignment;
                _model.DateAssigned = model.DateAssigned.Date;
                _model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                _model.CreatedDate = DateTime.Now;
                _model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                _model.ModifiedDate = DateTime.Now;
                _model.RecordStatus = (int)RecordStatus.Active;
                service.Add(_model);

                _model = service.GetById(model.EmployeePositionMappingId);
                _model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                _model.ModifiedDate = DateTime.Now;
                _model.RecordStatus = (int)RecordStatus.Deleted;
                service.Edit(_model);


                return RedirectToAction("Index", "AssignEmployeePosition");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Details(Guid id)
        {
            var model = service.GetById(id);
            return View(model);
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

        public ActionResult GetLocations(string location)
        {
            switch (location)
            {
                case AssignLocationConstant.Area:
                    var result = areaService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName });
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;
                case AssignLocationConstant.BSO:
                    result = bsoService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName });
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;
                case AssignLocationConstant.GatewaySat:
                    result = gatewaySatService.FilterActive().OrderBy(x => x.RevenueUnitName).Select(x => new { value = x.RevenueUnitId, text = x.RevenueUnitName });
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;
                case AssignLocationConstant.BCO:
                    result = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).Select(x => new { value = x.BranchCorpOfficeId, text = x.BranchCorpOfficeName });
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;
                default:
                    return Json(null, JsonRequestBehavior.AllowGet);
                    break;
            }
        }

        private List<SelectListItem> Locations()
        {
            List<SelectListItem> locations = new List<SelectListItem>();
            locations.Add(new SelectListItem { Text = "", Value = "" });
            var fields = typeof (AssignLocationConstant).GetFields();
            foreach (var item in fields)
            {
                var value = item.GetValue(null).ToString();
                locations.Add(new SelectListItem { Text = value, Value = value });
            }
            return locations;
        }

        private List<SelectListItem> LocationNames()
        {
            List<SelectListItem> locations = new List<SelectListItem>();
            locations.Add(new SelectListItem { Text = "select location", Value = "" });
            return locations;
        }

        private List<EmployeePositionMappingViewModel> ModelsToViewModels(List<EmployeePositionMapping> models)
        {
            List<EmployeePositionMappingViewModel> vms = new List<EmployeePositionMappingViewModel>();
            foreach (var item in models)
            {
                EmployeePositionMappingViewModel vm = new EmployeePositionMappingViewModel();
                vm.EmployeePositionMappingId = item.EmployeePositionMappingId;
                vm.EmployeeId = item.EmployeeId;
                vm.Employee = item.Employee;
                vm.PositionId = item.PositionId;
                vm.Position = item.Position;
                vm.DepartmentId = item.DepartmentId;
                vm.Department = item.Department;
                vm.DateAssigned = item.DateAssigned;
                vm.AssignedLocationId = item.AssignedLocationId;
                switch (item.LocationAssignment)
                {
                    case AssignLocationConstant.Area:
                        vm.AssignedLocation = areaService.GetById(item.AssignedLocationId);
                        vm.AssignedLocationName = ((RevenueUnit)vm.AssignedLocation).RevenueUnitName;
                        break;
                    case AssignLocationConstant.BSO:
                        vm.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
                        vm.AssignedLocationName = ((RevenueUnit)vm.AssignedLocation).RevenueUnitName;
                        break;
                    case AssignLocationConstant.GatewaySat:
                        vm.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
                        vm.AssignedLocationName = ((RevenueUnit)vm.AssignedLocation).RevenueUnitName;
                        break;
                    case AssignLocationConstant.BCO:
                        vm.AssignedLocation = bcoService.GetById(item.AssignedLocationId);
                        vm.AssignedLocationName = ((BranchCorpOffice)vm.AssignedLocation).BranchCorpOfficeName;
                        break;
                }
                vm.LocationAssignment = item.LocationAssignment;
                vms.Add(vm);
            }
            return vms;
        }
    }
}