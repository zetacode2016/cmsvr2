using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Shipment.ViewModels
{
    public class PaymentViewModel
    {
        [DisplayName("Payment Date")]
        public string PaymentDate { get; set; }
        [DisplayName("OR No")]
        public string OrNo { get; set; }
        [DisplayName("PR No")]
        public string PrNo { get; set; }
        [DisplayName("AirwayBill")]
        public string AirwayBill { get; set; }
        [DisplayName("Amount Paid")]
        public string AmountPaid { get; set; }
        [DisplayName("Check Bank Name")]
        public string CheckBank { get; set; }
        [DisplayName("Check Date")]
        public string CheckDate { get; set; }
        [DisplayName("Check No")]
        public string CheckNo { get; set; }
        [DisplayName("Received By")]
        public string ReceivedBy { get; set; }
        [DisplayName("Verified By")]
        public string VerifiedBy { get; set; }
        [DisplayName("Verified Date")]
        public string VerifiedDate { get; set; }
        [DisplayName("SOA No")]
        public string SoaNo { get; set; }
    }
}