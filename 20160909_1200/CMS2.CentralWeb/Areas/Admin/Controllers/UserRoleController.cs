using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Admin.ViewModels;
using CMS2.Common.Enums;
using CMS2.Common.Identity;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class UserRoleController : AdminBaseController
    {
        private UserRoleBL service = new UserRoleBL();
        private readonly UserManager<IdentityUser, Guid> _userManager;

        public UserRoleController(UserManager<IdentityUser, Guid> userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region RoleFunctions
        [HttpGet]
        public ActionResult ViewAllRoles()
        {
            return View(service.GetActiveRoles().OrderBy(x => x.RoleName).ToList());
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRole(Role model)
        {
            model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
            model.CreatedDate = DateTime.Now;
            model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
            model.ModifiedDate = DateTime.Now;
            model.RecordStatus = (int)RecordStatus.Active;
            service.AddRole(model);
            return RedirectToAction("ViewAllRoles");
        }

        [HttpGet]
        public ActionResult EditRole(Guid id)
        {
            var model = service.GetRoleById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(Role model)
        {
            model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
            model.ModifiedDate = DateTime.Now;
            service.EditRole(model);
            return RedirectToAction("ViewAllRoles");
        }

        public ActionResult DeleteRole(Guid id)
        {
            var model = service.GetRoleById(id);
            model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
            model.ModifiedDate = DateTime.Now;
            model.RecordStatus = (int)RecordStatus.Deleted;
            service.DeleteRole(model);
            return RedirectToAction("ViewAllRoles");
        }
        #endregion

        public ActionResult ViewAllUsers()
        {
            var users = service.GetAllActiveUsers();
            return View(users);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            var employees = service.GetEmployeeNames();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            foreach (var item in employees)
            {
                employeeList.Add(new SelectListItem { Text = item.Key, Value = item.Value.ToString() });
            }
            ViewBag.Employees = new SelectList(employeeList, "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(User model)
        {
            var employees = service.GetEmployeeNames();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            foreach (var item in employees)
            {
                employeeList.Add(new SelectListItem { Text = item.Key, Value = item.Value.ToString() });
            }
            ViewBag.Employees = new SelectList(employeeList, "Value", "Text");
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    Id = Guid.NewGuid(),
                    UserName = model.UserName,
                    EmployeeId = model.EmployeeId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Guid.Parse(User.Identity.GetUserId()),
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = Guid.Parse(User.Identity.GetUserId()),
                    RecordStatus = (int)RecordStatus.Active,
                    LastLogInDate = DateTime.Now,
                    LastPasswordChange = DateTime.Now,
                    OldPassword = model.PasswordHash
                };
                var result = _userManager.Create(user, model.PasswordHash);
                if (result.Succeeded)
                {
                    return RedirectToAction("ViewAllUsers");
                }
                else
                {
                    ModelState.AddModelError("Data Error", "Failed to add new user.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult EditUser(Guid id)
        {
            var user = service.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User model)
        {
            if (ModelState.IsValid)
            {

                var user = service.GetUserById(model.UserId);

                user.UserName = model.UserName;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                user.RecordStatus = (int)RecordStatus.Active;
                service.EditUser(user);
            }

            return View(model);
        }

        public ActionResult UserDetails(Guid id)
        {
            return View();
        }

        public ActionResult DeleteUser(Guid id)
        {
            return View();
        }

        public ActionResult DeactivateUser(Guid id)
        {
            return View();
        }

        public ActionResult ActivateUser(Guid id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserRole()
        {
            ViewBag.Users = new SelectList(service.GetAllActiveUsers(), "UserId", "UserName");
            ViewBag.Roles = new SelectList(service.GetActiveRoles(), "RoleId", "RoleName");

            return View();
        }

        [HttpPost]
        public ActionResult AddToRole(UserRoleViewModel vm)
        {
            Role role = service.GetRoleById(vm.RoleId);
            IdentityUser user = service.GetById(vm.UserId);
            service.AddToRole(user, role.RoleName);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult RemoveFromRole(UserRoleViewModel vm)
        {
            Role role = service.GetRoleById(vm.RoleId);
            IdentityUser user = service.GetById(vm.UserId);
            service.RemoveFromRole(user, role.RoleName);
            return RedirectToAction("Index");
        }
    }
}