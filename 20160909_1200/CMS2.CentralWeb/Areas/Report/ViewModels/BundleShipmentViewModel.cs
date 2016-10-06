
using System.Collections.Generic;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class BundleShipmentViewModel
    {
        public BundleShipmentViewModel()
        {
            Shipments = new List<ShipmentViewModel>();
            Discrepancies=new List<BundleShipmentDiscrepancyViewModel>();
        }
        [DisplayName("Date")]
        public string TransactionDate { get; set; }
        [DisplayName("Origin")]
        public string Origin { get; set; }
        [DisplayName("Destination")]
        public string Destination { get; set; }
        [DisplayName("Sack No")]
        public string BundleNo { get; set; }
        [DisplayName("Scanned By")]
        public string ScannedBy { get; set; }
        [DisplayName("Total AWB Count")]
        public string AwbCount { get; set; }
        [DisplayName("Total Pieces")]
        public string ItemCount { get; set; }
        public string TransactionType { get; set; }
        [DisplayName("Discrepancy")]
        public string Discrepancy { get; set; }
        public List<ShipmentViewModel> Shipments { get; set; }
        public List<BundleShipmentDiscrepancyViewModel> Discrepancies { get; set; } 
    }
}