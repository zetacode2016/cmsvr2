using System;

namespace CMS2.Entities.TrackingEntities.Models
{
    public class RetrievalViewModel
    {
        public DateTime TransactionDate { get; set; }
        public string OriginBranch { get; set; }
        public string DestinationBranchCode { get; set; }

        public string TransactionDateString { get { return TransactionDate.ToString("MMM dd, yyyy"); } }
    }
}
