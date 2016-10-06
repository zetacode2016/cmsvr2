using System;
using CMS2.Entities;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class ShipmentByDateBcoViewModel
    {
        public DateTime PickupDate { get; set; }
        public string PickupDateString { get { return PickupDate.ToString("MMM dd, yyyy hh:mm tt"); } }
        public Guid BranchCorpOfficeId { get; set; }
        public string BranchCorpOffice { get; set; }
        public Guid AreaId { get; set; }
        public RevenueUnit Area { get; set; }
        public string Truck { get; set; }
        public string Driver { get; set; }
        public Guid FieldRepId { get; set; }
        public string FieldRep { get; set; }
        public int ShipmentCount { get; set; }
    }
}