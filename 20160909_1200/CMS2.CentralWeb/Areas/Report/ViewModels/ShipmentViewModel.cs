

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class ShipmentViewModel
    {
        public string AirwayBillNo { get; set; }
        public string Shipper { get; set; }
        public string ShipperAddress { get; set; }
        public string Consignee { get; set; }
        public string ConsigneeAddress { get; set; }
        public string Origin { get; set; }
        public string OriginCity { get; set; }
        public string Destination { get; set; }
        public string DestinationCity { get; set; }
        public string Weight { get; set; }
        public string Quantity { get; set; }
        public string PaymentMode { get; set; }
        public string ServiceMode { get; set; }
        public string Remarks { get; set; }
        public string Amount { get; set; }
        public string PackageCount { get; set; }
        public string PickedUpBy { get; set; }
    }
}