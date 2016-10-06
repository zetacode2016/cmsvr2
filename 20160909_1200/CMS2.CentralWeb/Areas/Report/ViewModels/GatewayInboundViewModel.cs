using System;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class GatewayInboundViewModel
    {
        public DateTime TransactionDate { get; set; }
        public string ScannedBy { get; set; }
        public string Origin { get; set; }
        public string Gateway { get; set; }
        public string FlightNo { get; set; }
        public string ManifestAwb { get; set; }
    }
}