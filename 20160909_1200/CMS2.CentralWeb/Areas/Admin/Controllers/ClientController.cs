using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class ClientController : AdminBaseController
    {
        private ClientBL service = new ClientBL();

        public ActionResult Index(string type, Guid? id)
        {
            string header = "Customers (non-corp)";
            if (!string.IsNullOrEmpty(type) && type.Equals("corp"))
            {
                header = "Representatives";
            }

            List<Client> list = new List<Client>();
            if (id == null)
            {
                list = service.FilterActive();
                if (string.IsNullOrEmpty(type))
                {
                    list = service.FilterActiveBy(x => x.CompanyId == null);
                }
                else if (type.Equals("corp"))
                {
                    list = service.FilterActiveBy(x => x.CompanyId != null);
                }
            }
            else
            {
                Guid _id = Guid.Parse(id.ToString());
                list = service.FilterBy(x => x.CompanyId == _id);
                if (list.Count > 0)
                { header = list[0].Company.CompanyName + " Representatives"; }
                else
                {
                    header = "No Representatives found.";
                }
            }

            ViewBag.Header = header;
            return View(list);
        }

        [HttpGet]
        public ActionResult Add(string type, Guid? id) // id is CompanyId
        {
            ViewBag.Cities = new SelectList(service.GetCities().OrderBy(x => x.CityName).ToList(), "CityId", "CityName");
            ViewBag.Companies = new SelectList(service.GetCompanies().OrderBy(x => x.CompanyName).ToList(), "CompanyId", "CompanyName");

            if (id == null)
            { return View(new Client() { AccountNo = "2XXX000000000" }); }
            else
            {
                Client model = new Client();
                model.CompanyId = id;
                model.AccountNo = "2XXX000000000";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Client model)
        {
            var cities = service.GetCities().OrderBy(x => x.CityName).ToList();
            var companies = service.GetCompanies().OrderBy(x => x.CompanyName).ToList();
            ViewBag.Cities = new SelectList(cities, "CityId", "CityName", model.CityId);
            ViewBag.Companies = new SelectList(companies, "CompanyId", "CompanyName", model.CompanyId);

            model.CompanyName = "";
            if (ModelState.IsValid)
            {
                if (service.IsExist(x => x.FirstName.Equals(model.FirstName) && x.LastName.Equals(model.LastName)))
                {
                    ModelState.AddModelError("Data Error", "Invalid/duplicate Client");
                    return View(model);
                }
                else
                {
                    // corp rep client account #
                    model.AccountNo = "2" + service.GetNewAccountNo(cities.Find(x => x.CityId == model.CityId).CityCode, false);
                    if (model.CompanyId != null)
                    {
                        model.CompanyName = companies.Find(x => x.CompanyId == model.CompanyId).CompanyName;
                    }
                    model.ClientId = Guid.NewGuid();
                    model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                    model.ModifiedDate = DateTime.Now;
                    model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                    model.CreatedDate = DateTime.Now;
                    model.RecordStatus = (int)RecordStatus.Active;
                    service.Add(model);
                    return RedirectToAction("Index", "Client");
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
            var cities = service.GetCities().OrderBy(x => x.CityName).ToList();
            var companies = service.GetCompanies().OrderBy(x => x.CompanyName).ToList();
            ViewBag.Cities = new SelectList(cities, "CityId", "CityName");
            ViewBag.Companies = new SelectList(companies, "CompanyId", "CompanyName");
            var model = service.GetById(id);
            if (model == null)
                model = new Client();

            RecordStatus recordStatus = (RecordStatus)model.RecordStatus;
            model.Record_Status = recordStatus;

            string type = "Customer (non-corp)";
            if (model.CompanyId != null)
                type = "Representative";
            ViewBag.ClientTtpe = type;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client model)
        {
            ViewBag.Cities = new SelectList(service.GetCities().OrderBy(x => x.CityName).ToList(), "CityId", "CityName");
            ViewBag.Companies = new SelectList(service.GetCompanies().OrderBy(x => x.CompanyName).ToList(), "CompanyId", "CompanyName");

            model.CompanyName = "";
            if (string.IsNullOrEmpty(model.ContactNo) && string.IsNullOrEmpty(model.Mobile))
            {
                ModelState.AddModelError("Data Error", "Both Contact No and mobile cannot be empty.");
            }
            else if (!string.IsNullOrEmpty(model.ContactNo) && !IsNumericOnly(7, 7, model.ContactNo))
            {
                ModelState.AddModelError("Data Error", "Invalid Contact No.");
            }
            else if (!string.IsNullOrEmpty(model.Mobile) && !IsNumericOnly(11, 11, model.Mobile))
            {
                ModelState.AddModelError("Data Error", "Invalid Mobile.");
            }

            if (ModelState.IsValid)
            {
                
                Client entity = service.GetById(model.ClientId);
                entity.Address1 = model.Address1;
                entity.Address2 = model.Address2;
                entity.ContactNo = model.ContactNo;
                entity.Mobile = model.Mobile;
                entity.Email = model.Email;
                entity.CompanyId = model.CompanyId;
                entity.CompanyName = model.CompanyName;
                entity.CityId = model.CityId;
                entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                entity.ModifiedDate = DateTime.Now;
                entity.RecordStatus = (int)model.RecordStatus;
                service.Edit(entity);
                return RedirectToAction("Index", "Client");
            }
            else
            {
                ViewBag.IsNew = false;
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
            Edit(model);
            return RedirectToAction("Index", "Client");
        }

        private Boolean IsNumericOnly(int intMin, int intMax, string strNumericOnly)
        {
            Boolean blnResult = false;
            Regex regexPattern = new Regex("^[0-9\\s]{" + intMin.ToString() + "," + intMax.ToString() + "}$");
            if ((strNumericOnly.Length >= intMin & strNumericOnly.Length <= intMax))
            {
                // check here if there are other none alphanumeric characters
                strNumericOnly = strNumericOnly.Trim();
                blnResult = regexPattern.IsMatch(strNumericOnly);
            }
            else
            {
                blnResult = false;
            }
            return blnResult;
        }
    }
}