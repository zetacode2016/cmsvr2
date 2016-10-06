using System;
using System.Collections.Generic;

namespace CMS2.Entities.TrackingEntities.Models
{
    public class BundleViewModel
    {
        public string SackNo { get; set; }
        public string DestinationCityCode { get; set; }
        public string OriginCity { get; set; }
        public decimal Weight { get; set; }
        public List<CargoViewModel> CargoNos { get;set; }
        public DateTime TransactionDate { get; set; }
        public string User { get; set; }
        public string TransactionDateString { get { return TransactionDate.ToString("MMM dd, yyyy"); } }
    }
}
