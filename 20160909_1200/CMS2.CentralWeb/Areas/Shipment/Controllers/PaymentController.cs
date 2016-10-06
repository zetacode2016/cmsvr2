using System;
using System.Web.Mvc;
using CMS2.BusinessLogic;
using CMS2.CentralWeb.Areas.Shipment.ViewModels;

namespace CMS2.CentralWeb.Areas.Shipment.Controllers
{
    public class PaymentController : ShipmentBaseController
    {
        PaymentBL service = new PaymentBL();

        public ActionResult Index()
        {
            var payments = service.FilterActive();
            return View(payments);
        }

        public ActionResult Details(Guid id)
        {
            var payment = service.GetById(id);
            PaymentViewModel vm = new PaymentViewModel();
            vm.PaymentDate = payment.PaymentDateString;
            vm.OrNo = payment.OrNo;
            vm.AirwayBill = payment.Shipment.AirwayBillNo;
            vm.AmountPaid = payment.AmountString;
            vm.CheckBank = payment.CheckBankName;
            vm.CheckDate = "";
            if (payment.CheckDate!=null)
                vm.CheckDate = payment.CheckDate.GetValueOrDefault().ToString("MMM dd, yyyy");
            vm.CheckNo = payment.CheckNo;
            vm.ReceivedBy = payment.ReceivedBy.FullName;
            vm.VerifiedBy = "";
            vm.VerifiedDate = "";
            if (payment.VerifiedById != null)
            {
                vm.VerifiedBy = payment.VerifiedBy.FullName;
                vm.VerifiedDate = payment.VerifiedDate.GetValueOrDefault().ToString("MMM dd, yyyy");
            }
            vm.SoaNo = "";
            if (payment.StatementOfAccountPaymentId != null)
                vm.SoaNo = payment.StatementOfAccountPayment.StatementOfAccount.StatementOfAccountNo;
            return View(vm);
        }
    }
}