using System.Collections.Generic;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class PickupShipmentViewModel
    {
        [DisplayName("Branch Corp Office")]
        public string BranchCorpOffice { get; set; }
        [DisplayName("Date")]
        public string TransactionDate { get; set; }
        [DisplayName("Area")]
        public string Area { get; set; }
        [DisplayName("Driver")]
        public string Driver { get; set; }
        [DisplayName("Checker")]
        public string Checker { get; set; }
        public List<ShipmentViewModel> Shipments { get; set; }
         [DisplayName("Total AWB Count")]
        public string AwbCount { get; set; }
         [DisplayName("Total Weight")]
        public string TotalWeight { get; set; }
         [DisplayName("Total Pieces")]
        public string ItemCount { get; set; }
    }
}