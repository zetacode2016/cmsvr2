using System.Collections.Generic;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class ShipmentTransferShipmentViewModel
    {
        [DisplayName("Date")]
        public string TransactionDate { get; set; }
        [DisplayName("From")]
        public string TransferFrom { get; set; }
        [DisplayName("To")]
        public string TransferTo { get; set; }
        [DisplayName("Scanned By")]
        public string ScannedBy { get; set; }
        [DisplayName("Driver")]
        public string Driver { get; set; }
        [DisplayName("Plate No")]
        public string Truck { get; set; }
        public List<ShipmentViewModel> Shipments { get; set; }
        [DisplayName("Total AWB Count")]
        public string AwbCount { get; set; }
        [DisplayName("Total Pieces")]
        public string ItemCount { get; set; }
    }
}