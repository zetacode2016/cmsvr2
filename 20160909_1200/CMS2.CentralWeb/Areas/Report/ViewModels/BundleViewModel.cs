using System;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class BundleViewModel
    {
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid BranchCorpOfficeId { get; set; }
        public string Username { get; set; }
        public string BundleNo { get; set; }
        public string AwbCount { get; set; }
        public string OriginDestination { get; set; }
    }
}