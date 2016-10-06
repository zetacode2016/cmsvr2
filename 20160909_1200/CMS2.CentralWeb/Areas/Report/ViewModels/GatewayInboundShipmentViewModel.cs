using System.Collections.Generic;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class GatewayInboundShipmentViewModel
    {
        [DisplayName("Origin")]
        public string OriginCity { get; set; }
        [DisplayName("Date")]
        public string TransactionDate { get; set; }
        [DisplayName("Scanned By")]
        public string ScannedBy { get; set; }
        [DisplayName("Gateway")]
        public string Gateway { get; set; }
        [DisplayName("Flight No")]
        public string FlightNo { get; set; }
        [DisplayName("MAWB")]
        public string ManifestAwb { get; set; }
        public List<ShipmentViewModel> Shipments { get; set; }
        [DisplayName("Total AWB Count")]
        public string AwbCount { get; set; }
        [DisplayName("Total Pieces")]
        public string ItemCount { get; set; }
    }
}