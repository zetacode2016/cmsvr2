using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CMS2.Entities.TrackingEntities.Models
{
    public class BundleQueryViewModel
    {
        [DisplayName("Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Origin Branch")]
        public string OriginCity { get; set; }
        public List<BundleViewModel> BundleViewModels { get; set; } 
    }
}
