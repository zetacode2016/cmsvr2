using System.Collections.Generic;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Shipment.ViewModels
{
    public class DetailedTrackingQueryViewModel
    {
        [DisplayName("AirwayBill No")]
        public string AirwayBillNo { get; set; }
        [DisplayName("Shipper")]
        public string Shipper { get; set; }
        [DisplayName("Consignee")]
        public string Consignee { get; set; }
        [DisplayName("Pay Mode")]
        public string PaymentMode { get; set; }
        [DisplayName("Commodity")]
        public string Commodity { get; set; }
        [DisplayName("Origin")]
        public string Origin { get; set; }
        [DisplayName("Destination")]
        public string Destination { get; set; }
        [DisplayName("No. of Pcs.")]
        public string ItemCount { get; set; }
        public List<DetailedTrackingViewModel> DetailedTrackingViewModels { get; set; }
    }
}