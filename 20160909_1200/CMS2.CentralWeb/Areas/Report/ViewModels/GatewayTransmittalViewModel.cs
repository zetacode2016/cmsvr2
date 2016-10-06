using System;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class GatewayTransmittalViewModel
    {
        public DateTime TransactionDate { get; set; }
        public string AirwayBillNo { get; set; }
        public string ScannedBy { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Gateway { get; set; }
        public string Commodity { get; set; }
        public string Driver { get; set; }
        public string Truck { get; set; }
    }
}