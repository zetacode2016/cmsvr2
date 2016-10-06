using System.Collections.Generic;

namespace CMS2.CentralWeb.ViewModels
{
    /// <summary>
    /// Shipment Payments used for CFS WebAPI
    /// </summary>
    public class Payments
    {
        public string PaymentDate { get; set; }
        public string ORNo { get; set; }
        public string PaymentType { get; set; }
        public string CheckBankName { get; set; }
        public string CheckNo { get; set; }
        public string CheckDate { get; set; }
       public List<AwbPayment> AwbPayments { get; set; }
    }
}