using System.Collections.Generic;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class DistributionShipmentViewModel
    {
        [DisplayName("Branch Corp Office")]
        public string BranchCorpOffice { get; set; }
        [DisplayName("Date")]
        public string DistributionDate { get; set; }
        [DisplayName("Driver")]
        public string Driver { get; set; }
        [DisplayName("Plate No")]
        public string Truck { get; set; }
        [DisplayName("Scanned By")]
        public string ScannedBy { get; set; }
        [DisplayName("Area")]
        public string Area { get; set; }
        public List<ShipmentViewModel> Shipments { get; set; }
        [DisplayName("Total AWB Count")]
        public string AwbCount { get; set; }
        [DisplayName("Total Weight")]
        public string TotalWeight { get; set; }
        [DisplayName("Total Pieces")]
        public string ItemCount { get; set; }
        [DisplayName("Total Amount")]
        public string TotalAmount { get; set; }
    }
}