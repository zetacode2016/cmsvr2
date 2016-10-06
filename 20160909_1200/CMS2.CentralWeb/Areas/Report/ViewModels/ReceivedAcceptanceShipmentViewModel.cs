using System.Collections.Generic;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class ReceivedAcceptanceShipmentViewModel
    {
        [DisplayName("Date")]
        public string AcceptanceDate { get; set; }
        [DisplayName("Scanned By")]
        public string ScannedBy { get; set; }
        [DisplayName("Driver")]
        public string Driver { get; set; }
        [DisplayName("Checker")]
        public string Checker { get; set; }
        [DisplayName("Plate No")]
        public string Truck { get; set; }
        public List<ShipmentViewModel> Shipments { get; set; }
        [DisplayName("Total AWB Count")]
        public string AwbCount { get; set; }
        [DisplayName("Total Pieces")]
        public string ItemCount { get; set; }
    }
}