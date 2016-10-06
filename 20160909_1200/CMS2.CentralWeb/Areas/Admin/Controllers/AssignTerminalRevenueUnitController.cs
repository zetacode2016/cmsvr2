using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class AssignTerminalRevenueUnitController : AdminBaseController
    {
        private TerminalRevenueUnitMappingBL service;
        private RevenueUnitBL revenueUnitService;
        private UserRoleBL userRoleService;
        private TerminalBL terminalService;

        public AssignTerminalRevenueUnitController()
        {
            service = new TerminalRevenueUnitMappingBL();
            revenueUnitService = new RevenueUnitBL(service.GetUnitOfWork());
            userRoleService = new UserRoleBL(service.GetUnitOfWork());
            terminalService=  new TerminalBL(service.GetUnitOfWork());
        }
        public ActionResult Index()
        {
            List<TerminalRevenueUnitMapping> list = new List<TerminalRevenueUnitMapping>();
            list = service.FilterActive();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.RevenueUnits = new SelectList(revenueUnitService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName");
            ViewBag.Terminals = new SelectList(terminalService.FilterActive().OrderBy(x => x.TerminalCode).ToList(), "TerminalId", "TerminalCode");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TerminalRevenueUnitMapping model)
        {
            ViewBag.RevenueUnits = new SelectList(revenueUnitService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName",model.RevenueUnitId);
            ViewBag.Terminals = new SelectList(terminalService.FilterActive().OrderBy(x => x.TerminalCode).ToList(), "TerminalId", "TerminalCode", model.TerminalId);

            if (ModelState.IsValid)
            {
                model.TerminalRevenueUnitMappingId = Guid.NewGuid();
                model.AssignedById = userRoleService.GetUserById(Guid.Parse(User.Identity.GetUserId())).EmployeeId;
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;
                service.Add(model);
                return RedirectToAction("Index", "AssignTerminalRevenueUnit");
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
            ViewBag.RevenueUnits = new SelectList(revenueUnitService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName", model.RevenueUnitId);
            ViewBag.Terminals = new SelectList(terminalService.FilterActive().OrderBy(x => x.TerminalCode).ToList(), "TerminalId", "TerminalCode", model.TerminalId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Edit(TerminalRevenueUnitMapping model)
        {
            ViewBag.RevenueUnits = new SelectList(revenueUnitService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList(), "RevenueUnitId", "RevenueUnitName", model.RevenueUnitId);
            ViewBag.Terminals = new SelectList(terminalService.FilterActive().OrderBy(x => x.TerminalCode).ToList(), "TerminalId", "TerminalCode", model.TerminalId);

            if (ModelState.IsValid)
            {
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int)RecordStatus.Active;
                service.Edit(model);
                return RedirectToAction("Index", "AssignTerminalRevenueUnit");
            }
            else
            {
                return View(model);
            }
        }
    }
}