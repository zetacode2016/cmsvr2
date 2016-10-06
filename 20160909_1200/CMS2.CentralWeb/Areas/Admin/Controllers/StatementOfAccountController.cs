using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.BusinessLogic.ReportModels;
using CMS2.CentralWeb.Areas.Admin.ViewModels;
using CMS2.CentralWeb.Helper;
using CMS2.Common.Enums;
using CMS2.Common.Identity;
using CMS2.Entities;
using CMS2.Entities.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.AspNet.Identity;

namespace CMS2.CentralWeb.Areas.Admin.Controllers
{
    public class StatementOfAccountController : AdminBaseController
    {
        private StatementOfAccountBL service = new StatementOfAccountBL();

        public ActionResult Index(string message = "", string func = "")
        {
            ViewBag.Message = message;
            ViewBag.Function = func;
            string functionTitle = "";
            switch (func)
            {
                case "Create": // Create/Edit
                    ViewBag.Periods = new SelectList(service.GetBillingPeriods(null), "SoaPeriod");
                    functionTitle = "Create/Edit";
                    break;
                case "Display":
                    functionTitle = "Display";
                    break;
                case "Details":
                    functionTitle = "Details";
                    break;
                case "Adjustment":
                    functionTitle = "Adjustment";
                    break;
                case "History":
                    functionTitle = "History";
                    break;
                case "Payment":
                    functionTitle = "Make Payment";
                    break;
                case "PaymentHistory":
                    functionTitle = "Payment History";
                    break;
            }
            ViewBag.FunctionTitle = functionTitle;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection form)
        {
            string func = form["func"].ToString();
            ViewBag.Function = func;
            ViewBag.FunctionTitle = form["title"].ToString();

            string _accountNo = "";
            string _soaNo = "";
            if (form["AccountNo"] != null)
            {
                _accountNo = form["AccountNo"].ToString();
            }
            if (form["SOANo"] != null)
            {
                _soaNo = form["SOANo"].ToString();
                if (_soaNo.Length != 5)
                {
                    ViewBag.Message = "Invalid SOA No";
                    return View();
                }
            }
            Guid soaId;
            switch (func)
            {
                case "Create": // Create/Edit
                    ViewBag.Periods = new SelectList(service.GetBillingPeriods(null), "SoaPeriod");
                    string periodSelected = form["SoaPeriod"].ToString();
                    DateTime _periodStart = Convert.ToDateTime(periodSelected.Substring(0, periodSelected.IndexOf("-") - 1));
                    DateTime _periodEnd = Convert.ToDateTime(periodSelected.Substring(periodSelected.IndexOf("-") + 2, (periodSelected.Length - (periodSelected.IndexOf("-") + 2))));
                    if (
                        service.IsExist(x => x.Company.AccountNo.Equals(_accountNo) && x.StatementOfAccountPeriodFrom == _periodStart && x.StatementOfAccountPeriodUntil == _periodEnd))
                    {
                        StatementOfAccount entity = service.FilterActiveBy(x => x.Company.AccountNo.Equals(_accountNo) && x.StatementOfAccountPeriodFrom == _periodStart && x.StatementOfAccountPeriodUntil == _periodEnd).OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault(); ;
                        return RedirectToAction("Details", new { id = entity.StatementOfAccountId });
                    }
                    else
                    {
                        return RedirectToAction("Create", new { accountNo = _accountNo, periodStart = _periodStart, periodEnd = _periodEnd });
                    }
                case "Display": // display Read-Only SOA info
                    soaId = GetIdByStatementOfAccountNo(_soaNo);
                    if (soaId == null || soaId == Guid.Empty)
                    {
                        return RedirectToAction("Display", new { id = soaId, message = "SOA not found." });
                    }
                    else
                    {
                        return RedirectToAction("Display", new { id = soaId });
                    }
                case "Details": // display Read-Only SOA Details
                    soaId = GetIdByStatementOfAccountNo(_soaNo);
                    if (soaId == null || soaId == Guid.Empty)
                    {
                        return RedirectToAction("Details", new { id = soaId, message = "SOA not found." });
                    }
                    else
                    {
                        return RedirectToAction("Details", new { id = soaId });
                    }
                case "Adjustment":
                    soaId = GetIdByStatementOfAccountNo(_soaNo);
                    return RedirectToAction("Adjustment", new { id = soaId });
                case "History":
                    return RedirectToAction("History", new { accountNo = _accountNo });
                case "Payment":
                    soaId = GetIdByStatementOfAccountNo(_soaNo);
                    return RedirectToAction("Payment", new { id = soaId });
                case "PaymentHistory":
                    return RedirectToAction("PaymentHistory", new { soaNo = _soaNo });
            }
            return View();
        }

        public ActionResult Create(string accountNo, DateTime periodStart, DateTime periodEnd)
        {
            logs.AppLogs(LogPath, "SOA Controller - Create");
            StatementOfAccount entity = service.CreateNewSoa(accountNo, periodStart, periodEnd, Guid.Parse(User.Identity.GetUserId()));
            if (entity != null)
            {
                StatementOfAccountModel model = service.EntityToModel(entity);
                model = service.Finalize(model);
                service.CreateSavePdfFile(model);
                return RedirectToAction("Details", new { id = entity.StatementOfAccountId });
            }
            else
            {
                return RedirectToAction("Index", new { message = "Failed to create SOA", func = "Create" });
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            logs.AppLogs(LogPath, "SOA Controller - Edit (Get)");
            Guid _id = id ?? new Guid();
            StatementOfAccountModel model = service.GetModelById(_id);
            model = service.ComputeSoa(model);

            var previousSoaNos = service.GetPreviousSoaNumbersByCompanyId(model.CompanyId);
            List<SelectListItem> soaNosList = new List<SelectListItem>();
            foreach (var item in previousSoaNos)
            {
                soaNosList.Add(new SelectListItem { Text = item.Key, Value = item.Value.ToString() });
            }
            ViewBag.PreviousSoaNos = new SelectList(soaNosList, "Value", "Text");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StatementOfAccountModel model)
        {
            logs.AppLogs(LogPath, "SOA Controller  - Edit (Post)");
            var previousSoaNos = service.GetPreviousSoaNumbersByCompanyId(model.CompanyId);
            List<SelectListItem> soaNosList = new List<SelectListItem>();
            foreach (var item in previousSoaNos)
            {
                soaNosList.Add(new SelectListItem { Text = item.Key, Value = item.Value.ToString() });
            }
            ViewBag.PreviousSoaNos = new SelectList(soaNosList, "Value", "Text");

            if (ModelState.IsValid)
            {
                StatementOfAccount entity = service.ModelToEntity(model);
                entity.CiNo = model.CiNo;
                entity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                entity.ModifiedDate = DateTime.Now;
                entity.RecordStatus = (int)RecordStatus.Active;
                service.Edit(entity);
                return RedirectToAction("Index", new { message = "SOA successfully updated." });
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Display(Guid? id, string message = "")
        {
            logs.AppLogs(LogPath, "SOA Controller - Display (Get)");
            ViewBag.Message = message;
            Guid _id = id ?? new Guid();
            StatementOfAccountModel model = service.GetModelById(_id);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("Index", new { func = "Display", message = "SOA not found." });
        }

        [MultiButton]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(StatementOfAccountModel model)
        {
            return RedirectToActionPermanent("Edit", new { id = model.StatementOfAccountId });
        }

        [MultiButton]
        [HttpPost]
        public ActionResult Finalize(StatementOfAccountModel model)
        {
            logs.AppLogs(LogPath, "SOA Controller - Finalize");
            StatementOfAccountModel _model = service.Finalize(model);
            return RedirectToAction("Details", new { message = "SOA successfully finalized.", id = _model.StatementOfAccountId });
        }

        public ActionResult Finalize(Guid id)
        {
            logs.AppLogs(LogPath, "SOA Controller - Finalize");
            StatementOfAccountModel model = service.GetModelById(id);
            model = service.Finalize(model);
            return RedirectToAction("Details", new { message = "SOA successfully finalized.", id = model.StatementOfAccountId });
        }

        /// <summary>
        /// Read-only of the SOA, Current Charges and Previous Balance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(Guid? id, string message = "")
        {
            logs.AppLogs(LogPath, "SOA Controller - Details (Get)");
            ViewBag.Message = message;
            Guid _id = id ?? new Guid();
            StatementOfAccountModel model = service.GetModelById(_id);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("Index", new { func = "Details", message = "SOA not found." });
        }

        [HttpGet]
        public ActionResult Adjustment(Guid? id)
        {
            logs.AppLogs(LogPath, "SOA Controller - Adjustment (Get)");
            Guid _id = id ?? new Guid();

            StatementOfAccountModel model = service.GetModelById(_id);
            if (model != null)
            {
                return View(model);
            }
            return View(new StatementOfAccountModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adjustment(StatementOfAccountModel model, FormCollection form)
        {
            logs.AppLogs(LogPath, "SOA Controller - Adjustment (Post)");
            List<ShipmentAdjustment> shipmentAdjustments = new List<ShipmentAdjustment>();
            foreach (var item in form.AllKeys)
            {
                if (item.StartsWith("Adjustment"))
                {
                    int startIndex = item.IndexOf('_') + 1;
                    var shipmentId = item.Substring(startIndex, item.Length - startIndex);
                    decimal amountPaid = Convert.ToDecimal(form[item]);
                    if (amountPaid != 0)
                    {
                        ShipmentAdjustment adjustmentModel = new ShipmentAdjustment()
                        {
                            ShipmentId = Guid.Parse(shipmentId),
                            StatementOfAccountId = model.StatementOfAccountId,
                            DateAdjusted = model.SoaDueDate.AddDays(-1).Date, // ++++ added for testing
                            AdjustmentAmount = amountPaid,
                            AdjustedById = Guid.Parse(User.Identity.GetUserId()),
                            ModifiedBy = Guid.Parse(User.Identity.GetUserId()),
                            ModifiedDate = DateTime.Now,
                            RecordStatus = (int)RecordStatus.Active
                        };
                        shipmentAdjustments.Add(adjustmentModel);
                    }
                }
            }
            service.UpdateAdjustments(shipmentAdjustments);
            return RedirectToAction("Adjustment", new { id = model.StatementOfAccountId });
        }

        public ActionResult History(string accountNo)
        {
            logs.AppLogs(LogPath, "SOA Controller - History");
            List<StatementOfAccountModel> models = service.GetHistory(accountNo);
            return View(models);
        }

        [HttpGet]
        public ActionResult Payment(Guid? id, string message = "")
        {
            Guid _id = id ?? new Guid();
            StatementOfAccountModel model = service.GetModelById(_id);
            if (model != null)
            {
                // TODO commented validation for testing
                //if (DateTime.Now <= model.DateDue)
                //{
                StatementOfAccountPaymentViewModel viewModel = new StatementOfAccountPaymentViewModel()
                {
                    StatementOfAccountModel = model,
                    PaymentViewModel = new PaymentViewModel()
                };

                ViewBag.PaymentTypes = new SelectList(service.GetPaymentTypes(), "PaymentTypeId", "PaymentTypeName");
                ViewBag.Message = message;
                return View(viewModel);
                //}
                //else
                //{
                //    ViewBag.Message = "Sorry, cannot make payment for this Statement of Account.";
                //    ViewBag.PaymentTypes = new SelectList(service.GetPaymentTypes(), "PaymentTypeId", "PaymentTypeName");
                //    return View();
                //}
            }
            return View(new StatementOfAccountPaymentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(StatementOfAccountPaymentViewModel viewModel, FormCollection form)
        {
            UserStore userStore = new UserStore(service.GetUnitOfWork());
            IdentityUser userInfo = userStore.FindByIdAsync(Guid.Parse(User.Identity.GetUserId())).Result;
            
            StatementOfAccountPayment _soaPayment = new StatementOfAccountPayment()
            {
                StatementOfAccountId = viewModel.StatementOfAccountModel.StatementOfAccountId,
                PaymentDate = viewModel.PaymentViewModel.PaymentDate,
                OrNo = viewModel.PaymentViewModel.OrNo,
                Amount = viewModel.PaymentViewModel.PaymentAmount,
                PaymentTypeId = viewModel.PaymentViewModel.PaymentTypeId,
                CheckBankName = viewModel.PaymentViewModel.CheckBankName,
                CheckNo = viewModel.PaymentViewModel.CheckNo,
                CheckDate = viewModel.PaymentViewModel.CheckDate,
                ReceivedById = userInfo.EmployeeId,
                Remarks = viewModel.PaymentViewModel.Remarks,
                CreatedBy = Guid.Parse(User.Identity.GetUserId()),
                ModifiedBy = Guid.Parse(User.Identity.GetUserId())
            };

            bool applytoAll = viewModel.PaymentViewModel.ApplyToAll;
            if (applytoAll)
            {
                service.MakePayment(_soaPayment, viewModel.StatementOfAccountModel);
                return RedirectToAction("Index", new { func = "Payment" });
            }
            else
            {
                decimal totalPaymentsToShipment = 0;
                List<Payment> shipmentPayments = new List<Payment>();
                foreach (var item in form.AllKeys)
                {
                    if (item.StartsWith("PaymentAmount"))
                    {
                        int startIndex = item.IndexOf('_') + 1;
                        var shipmentId = item.Substring(startIndex, item.Length - startIndex);
                        decimal amountPaid = Convert.ToDecimal(form[item]);
                        if (amountPaid > 0)
                        {
                            Payment model = new Payment()
                            {
                                ShipmentId = Guid.Parse(shipmentId),
                                PaymentDate = viewModel.PaymentViewModel.PaymentDate,
                                OrNo = viewModel.PaymentViewModel.OrNo,
                                Amount = amountPaid,
                                PaymentTypeId = viewModel.PaymentViewModel.PaymentTypeId,
                                CheckBankName = viewModel.PaymentViewModel.CheckBankName,
                                CheckNo = viewModel.PaymentViewModel.CheckNo,
                                CheckDate = viewModel.PaymentViewModel.CheckDate,
                                ReceivedById = userInfo.EmployeeId,
                                Remarks = viewModel.PaymentViewModel.Remarks,
                                CreatedBy = Guid.Parse(User.Identity.GetUserId()),
                                ModifiedBy = Guid.Parse(User.Identity.GetUserId())
                            };
                            shipmentPayments.Add(model);
                            totalPaymentsToShipment = totalPaymentsToShipment + amountPaid;
                        }
                    }
                }

                if (totalPaymentsToShipment == viewModel.PaymentViewModel.PaymentAmount)
                {
                    service.MakePayment(_soaPayment, shipmentPayments);
                    return RedirectToAction("Payment", new { id = viewModel.StatementOfAccountModel.StatementOfAccountId });
                }
                else
                {
                    return RedirectToAction("Payment", new { id = viewModel.StatementOfAccountModel.StatementOfAccountId, message = "Invalid payment amount." });
                }
            }
        }

        [HttpGet]
        public ActionResult PaymentHistory(string soaNo)
        {
            List<StatementOfAccountPayment> models =
                service.GetStatementOfAccountPaymentsBySoaNo(soaNo).OrderBy(x => x.StatementOfAccount.StatementOfAccountNo).ThenByDescending(x => x.PaymentDate).ToList();
            if (models != null && models.Count > 0)
            {
                return View(models);
            }
            else
            {
                return RedirectToAction("Index", new { func = "Payment History", message = "No payment record for SOA No " + soaNo });
            }

        }

        public JsonResult BillingPeriod(string accountNo)
        {
            var billingperiod = service.GetBillingPeriods((accountNo));
            return Json(billingperiod, JsonRequestBehavior.AllowGet);
        }

        private Guid GetIdByStatementOfAccountNo(string soaNo)
        {
            var id = service.GetIdByStatementOfAccountNo(soaNo);
            if (id != null && id != Guid.Empty)
            {
                return id;
            }
            else
            {
                return Guid.Empty;
            }
        }

        public ActionResult ViewPdf(string id, string branch) // id value is the SOA No
        {
            logs.AppLogs(LogPath, "SOA Controller - ViewPrintable");
            string soaReportPath = "";
            string soaReportFile = "";
            if (!string.IsNullOrEmpty(id))
            {
                string soaDate = id.Substring(0, 8);
                var appSetting = service.ApplicationSetting;
                soaReportPath = appSetting.SingleOrDefault(x => x.SettingName.Equals("SOAReportPath")).SettingValue + soaDate + "\\" + branch + "\\";
                soaReportFile = id + ".pdf";

                try
                {
                    return File(soaReportPath + soaReportFile, "application/pdf");
                }
                catch (Exception ex)
                {
                    logs.ErrorLogs(LogPath, "SOA Controller - ViewPrintable", ex.Message);
                    //return RedirectToAction("Display",new { id = id.Substring(0, 8), message = "Error in generating printable report." });
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Data in this SOA is dynamic.
        /// True SOA is stored in PDF and retrieved using the Action ViewPdf.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ViewPrintable(Guid id)
        {
            logs.AppLogs(LogPath, "SOA Controller - ViewPrintable");
            StatementOfAccountModel model = service.GetModelById(id);
            string soaReportPath = "";
            if (model != null)
            {
                StatementOfAccountDataSet soaDs = new StatementOfAccountDataSet();
                // fill SOA Details
                // TODO check the assignment of data. Must not add.
                DataRow dr = soaDs.Tables["StatementOfAccount"].NewRow();
                dr["SoaNo"] = model.StatementOfAccountNo;
                dr["SoaDateString"] = model.StatementOfAccountDateString;
                dr["SoaDueDate"] = model.SoaDueDateString;
                dr["SoaBillingPeriod"] = model.StatementOfAccountPeriod;
                dr["AccountNo"] = model.Company.AccountNo;
                dr["AccountNoBarcode"] = model.StatementOfAccountNoBarcode;
                dr["CompanyName"] = model.Company.CompanyName;
                dr["CompanyAddress"] = model.Company.BillingAddress1;
                dr["SoaAmountDue"] = model.TotalSoaAmountString;
                dr["TotalCurrentBalance"] = model.TotalCurrentChargesString;
                dr["TotalPreviousBalance"] = model.TotalPreviousBalanceString;
                dr["TotalPreviousSurcharges"] = model.TotalPreviousSurchargeString;
                dr["TotalFreightCharges"] = model.TotalCurrentSubTotalString;
                dr["TotalVatAmount"] = model.TotalCurrentVatAmountString;
                dr["TotalCurrentAmountDue"] = model.TotalCurrentTotalString;
                dr["TotalPreviousAmountDue"] = model.TotalPreviousAmountDueString;
                dr["TotalPreviousAdjustments"] = model.TotalPreviousAdjustmentsString;
                dr["TotalPreviousPayments"] = model.TotalPreviousPaymentsString;
                soaDs.Tables["StatementOfAccount"].Rows.Add(dr);

                // fill current Shipments
                foreach (var item in model.CurrentShipments)
                {
                    dr = soaDs.Tables["CurrentShipments"].NewRow();
                    dr["DateAccepted"] = item.DateAcceptedString;
                    dr["AirwayBillNo"] = item.AirwayBillNo;
                    dr["Origin"] = item.OriginCity.CityName;
                    dr["Destination"] = item.DestinationCity.CityName;
                    dr["FreightCharges"] = item.ShipmentSubTotalString;
                    dr["VatAmount"] = item.ShipmentVatAmountString;
                    dr["AmountDue"] = item.ShipmentTotalString;
                    soaDs.Tables["CurrentShipments"].Rows.Add(dr);
                }

                // fill previous Shipments
                foreach (var item in model.PreviousShipments)
                {
                    dr = soaDs.Tables["PreviousShipments"].NewRow();
                    dr["SoaDate"] = item.StatementOfAccount.StatementOfAccountDateString;
                    dr["SoaNo"] = item.StatementOfAccount.StatementOfAccountNo;
                    dr["AwbNo"] = item.AirwayBillNo;
                    dr["PreviousAmountDue"] = item.PreviousAmountDueString;
                    dr["PreviousPayment"] = item.PreviousPaymentsString;
                    dr["PreviousAdjustment"] = item.PreviousAdjustmentsString;
                    dr["PreviousAmountDue"] = item.PreviousBalanceString;
                    dr["Surcharge"] = item.SurchargeString;
                    soaDs.Tables["PreviousShipments"].Rows.Add(dr);
                }

                // fill SOA Payments
                foreach (var item in model.PreviousSoaPayments)
                {
                    dr = soaDs.Tables["Payments"].NewRow();
                    dr["PaymentDate"] = item.PaymentDateString;
                    dr["OrPrNo"] = item.OrNo;
                    dr["Form"] = item.PaymentType.PaymentTypeName;
                    dr["Remarks"] = item.CheckBankName + " " + item.CheckNo + " " + item.Remarks;
                    dr["AmountPaid"] = item.AmountString;
                    soaDs.Tables["Payments"].Rows.Add(dr);
                }

                ReportDocument reportDocument = new ReportDocument();
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportFile = Server.MapPath("~/Reports/StatementOfAccount.rpt");
                reportDocument.Load(reportFile);
                reportDocument.SetDataSource(soaDs);
                try
                {
                    // this will export Report to PDFFile
                    Stream stream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf");
                }
                catch (Exception ex)
                {
                    logs.ErrorLogs(LogPath, "SOA Controller - ViewPrintable", ex.Message);
                    return RedirectToAction("Details", new { id = id, message = "Error in generating printable report." });
                }
            }
            return View(); // TODO redirect properly if SOA is not found
        }

        [MultiButton]
        [HttpPost]
        public ActionResult Print(StatementOfAccount model)
        {
            service.Print(model);
            return View();
        }
    }
}