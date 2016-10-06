using System;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Admin.ViewModels
{
    public class PaymentViewModel
    {
        [DisplayName("OR No")]
        public string OrNo { get; set; }
        [DisplayName("PR No")]
        public string PrNo { get; set; }
        [DisplayName("Date")]
        public DateTime PaymentDate { get; set; }
        [DisplayName("Amount")]
        public decimal PaymentAmount { get; set; }
        [DisplayName("Payment Type")]
        public Guid PaymentTypeId { get; set; }
        [DisplayName("Bank Name")]
        public string CheckBankName { get; set; }
        [DisplayName("Check No")]
        public string CheckNo { get; set; }
        [DisplayName("Check Date")]
        public DateTime? CheckDate { get; set; }
        public string Remarks { get; set; }
        public bool ApplyToAll { get; set; }
    }
}