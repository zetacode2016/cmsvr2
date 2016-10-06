using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class BundleSummaryViewModel
    {
        public BundleSummaryViewModel()
        {
            BundleViewModels = new List<BundleViewModel>();
        }

        [DisplayName("Bundle/UnBundle")]
        public string TransactionType { get; set; }
        [DisplayName("Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Branch Corp Office")]
        public Guid BranchCorpOfficeId { get; set; } // point of scanning
        public List<BundleViewModel> BundleViewModels { get; set; }

       
    }
}