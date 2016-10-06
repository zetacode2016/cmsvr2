using System;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class DistributionViewModel
    {
        public DateTime TransactionDate { get; set; }
        public string Area { get; set; }
        public string Driver { get; set; }
        public string Truck { get; set; }
        ////public Guid ScannedById { get; set; }
        ////public string ScannedBy { get; set; }
        public string Wave { get; set; }
        public string Checker { get; set; }
        public string Username { get; set; }
    }
}