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
    public class CompanyController : AdminBaseController
    {
        private CompanyBL service = new CompanyBL();

        public ActionResult Index()
        {
            var list = service.FilterActive();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Cities = new SelectList(service.GetCities(), "CityId", "CityName");
            ViewBag.PaymentTerms = new SelectList(service.GetPaymentTerms(), "PaymentTermId", "PaymentTermName");
            ViewBag.AccountStatus = new SelectList(service.GetAccountStatus().OrderBy(x=>x.AccountStatusName).ToList(), "AccountStatusId", "AccountStatusName");
            ViewBag.AccountTypes = new SelectList(service.GetAccountTypes(), "AccountTypeId", "AccountTypeName");
            ViewBag.BusinessTypes = new SelectList(service.GetBusinessTypes().OrderBy(x => x.BusinessTypeName).ToList(), "BusinessTypeId", "BusinessTypeName");
            ViewBag.Industries = new SelectList(service.GetIndustries(), "IndustryId", "IndustryName");
            ViewBag.OrganizationTypes = new SelectList(service.GetOrganizationTypes().OrderBy(x=>x.OrganizationTypeName).ToList(), "OrganizationTypeId", "OrganizationTypeName");
            ViewBag.BillingPeriods = new SelectList(service.GetBillingPeriods(), "BillingPeriodid", "BillingPeriodName");
            ViewBag.Companies = new SelectList(service.FilterActive().ToList(), "CompanyId", "CompanyName");
            ViewBag.Employees = new SelectList(service.GetEmployees(), "EmployeeId", "FullName");

            return View(new Company() {AccountNo = "XXX000000000" });
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Company model)
        {
            ViewBag.Cities = new SelectList(service.GetCities(), "CityId", "CityName",model.CityId);
            ViewBag.PaymentTerms = new SelectList(service.GetPaymentTerms(), "PaymentTermId", "PaymentTermName",model.PaymentTermId);
            ViewBag.AccountStatus = new SelectList(service.GetAccountStatus(), "AccountStatusId", "AccountStatusName",model.AccountStatusId);
            ViewBag.AccountTypes = new SelectList(service.GetAccountTypes(), "AccountTypeId", "AccountTypeName",model.AccountTypeId);
            ViewBag.BusinessTypes = new SelectList(service.GetBusinessTypes(), "BusinessTypeId", "BusinessTypeName",model.BusinessTypeId);
            ViewBag.Industries = new SelectList(service.GetIndustries(), "IndustryId", "IndustryName",model.IndustryId);
            ViewBag.OrganizationTypes = new SelectList(service.GetOrganizationTypes(), "OrganizationTypeId", "OrganizationTypeName",model.OrganizationTypeId);
            ViewBag.BillingPeriods = new SelectList(service.GetBillingPeriods(), "BillingPeriodid", "BillingPeriodName",model.BillingPeriodId);
            ViewBag.Companies = new SelectList(service.FilterActive().ToList(), "CompanyId", "CompanyName", model.MotherCompanyId);
            ViewBag.Employees = new SelectList(service.GetEmployees(), "EmployeeId", "FullName",model.ApprovedById);

            if (ModelState.IsValid)
            {
                model.CompanyId = Guid.NewGuid();
                model.AccountNo = "0" + service.GetNewAccountNo(model.CityId, model.ApprovedDate);
                model.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                model.ModifiedDate = DateTime.Now;
                model.RecordStatus = (int) RecordStatus.Active;
                service.Add(model);
                return RedirectToAction("Index");
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
            List<Company> companies = service.FilterActive();
            companies = companies.Where(x => x.CompanyId != model.CompanyId).Where(x => x.MotherCompanyId != model.CompanyId).ToList();

            ViewBag.Cities = new SelectList(service.GetCities(), "CityId", "CityName");
            ViewBag.PaymentTerms = new SelectList(service.GetPaymentTerms(), "PaymentTermId", "PaymentTermName");
            ViewBag.AccountStatus = new SelectList(service.GetAccountStatus(), "AccountStatusId", "AccountStatusName");
            ViewBag.AccountTypes = new SelectList(service.GetAccountTypes(), "AccountTypeId", "AccountTypeName");
            ViewBag.BusinessTypes = new SelectList(service.GetBusinessTypes(), "BusinessTypeId", "BusinessTypeName");
            ViewBag.Industries = new SelectList(service.GetIndustries(), "IndustryId", "IndustryName");
            ViewBag.OrganizationTypes = new SelectList(service.GetOrganizationTypes(), "OrganizationTypeId", "OrganizationTypeName");
            ViewBag.BillingPeriods = new SelectList(service.GetBillingPeriods(), "BillingPeriodid", "BillingPeriodName");
            ViewBag.Companies = new SelectList(companies, "CompanyId", "CompanyName", model.MotherCompanyId);
            ViewBag.Employees = new SelectList(service.GetEmployees(), "EmployeeId", "FullName");
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company model)
        {
            List<Company> companies = service.FilterActive();
            companies = companies.Where(x => x.CompanyId != model.CompanyId).Where(x => x.MotherCompanyId != model.CompanyId).ToList();

            ViewBag.Cities = new SelectList(service.GetCities(), "CityId", "CityName",model.CityId);
            ViewBag.PaymentTerms = new SelectList(service.GetPaymentTerms(), "PaymentTermId", "PaymentTermName",model.PaymentTermId);
            ViewBag.AccountStatus = new SelectList(service.GetAccountStatus(), "AccountStatusId", "AccountStatusName",model.AccountStatusId);
            ViewBag.AccountTypes = new SelectList(service.GetAccountTypes(), "AccountTypeId", "AccountTypeName",model.AccountTypeId);
            ViewBag.BusinessTypes = new SelectList(service.GetBusinessTypes(), "BusinessTypeId", "BusinessTypeName", model.BusinessTypeId);
            ViewBag.Industries = new SelectList(service.GetIndustries(), "IndustryId", "IndustryName",model.IndustryId);
            ViewBag.OrganizationTypes = new SelectList(service.GetOrganizationTypes(), "OrganizationTypeId", "OrganizationTypeName", model.OrganizationTypeId);
            ViewBag.BillingPeriods = new SelectList(service.GetBillingPeriods(), "BillingPeriodid", "BillingPeriodName",model.BillingPeriodId);
            ViewBag.Companies = new SelectList(companies, "CompanyId", "CompanyName", model.MotherCompanyId);
            ViewBag.Employees = new SelectList(service.GetEmployees(), "EmployeeId", "FullName",model.ApprovedById);
            
            if (ModelState.IsValid)
            {
                var entity = service.GetById(model.CompanyId);
                entity.CompanyName = model.CompanyName;
                entity.ContactNo = model.ContactNo;
                entity.Fax = model.Fax;
                entity.Email = model.Email;
                entity.Address1 = model.Address1;
                entity.Address2 = model.Address2;
                entity.CityId = model.CityId;
                entity.ZipCode = model.ZipCode;
                entity.Website = model.Website;
                entity.President = model.President;
                entity.TinNo = model.TinNo;
                entity.CityId = model.CityId;
                entity.MotherCompanyId = model.MotherCompanyId;
                entity.ContactPerson = model.ContactPerson;
                entity.ContactPosition = model.ContactPosition;
                entity.ContactDepartment = model.ContactDepartment;
                entity.ContactTelNo = model.ContactTelNo;
                entity.ContactMobile = model.ContactMobile;
                entity.ContactFax = model.ContactFax;
                entity.BillingAddress1 = model.BillingAddress1;
                entity.BillingAddress2 = model.BillingAddress2;
                entity.BillingCityId = model.BillingCityId;
                entity.BillingZipCode = model.BillingZipCode;
                entity.BillingContactPerson = model.BillingContactPerson;
                entity.BillingContactPosition = model.BillingContactPosition;
                entity.BillingContactDepartment = model.BillingContactDepartment;
                entity.BillingContactTelNo = model.BillingContactTelNo;
                entity.BillingContactMobile = model.BillingContactMobile;
                entity.BillingContactEmail = model.BillingContactEmail;
                entity.BillingContactFax = model.BillingContactFax;
                entity.AccountTypeId = model.AccountTypeId;
                entity.IndustryId = model.IndustryId;
                entity.BusinessTypeId = model.BusinessTypeId;
                entity.OrganizationTypeId = model.OrganizationTypeId;
                entity.AccountStatusId = model.AccountStatusId;
                entity.ApprovedDate = model.ApprovedDate;
                entity.ApprovedById = model.ApprovedById;
                entity.PaymentTermId = model.PaymentTermId;
                entity.BillingPeriodId = model.BillingPeriodId;
                entity.Discount = model.Discount;
                entity.HasValuationCharge = model.HasValuationCharge;
                entity.ApplyEvm = model.ApplyEvm;
                entity.ApplyWeight = model.ApplyWeight;
                entity.HasAwbFee = model.HasAwbFee;
                entity.IsVatable = model.IsVatable;
                entity.HasValuationCharge = model.HasValuationCharge;
                entity.HasInsurance = model.HasInsurance;
                entity.HasFreightCollectCharge = model.HasFreightCollectCharge;
                entity.HasChargeInvoice = model.HasChargeInvoice;
                entity.CreditLimit = model.CreditLimit;
                entity.HasFuelCharge = model.HasFuelCharge;
                entity.HasDeliveryFee = model.HasDeliveryFee;
                entity.HasPerishableFee = model.HasPerishableFee;
                entity.HasDangerousFee = model.HasDeliveryFee;
                entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                entity.ModifiedDate = DateTime.Now;
                service.Add(entity);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Delete(Guid id)
        {
            var model = service.GetById(id);
            model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
            model.ModifiedDate = DateTime.Now;
            model.RecordStatus = (int) RecordStatus.Deleted;
            service.Edit(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var model = service.GetById(id);
            return View(model);
        }
    }
}