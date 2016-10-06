using System;

namespace CMS2.Entities.TrackingEntities.Models
{
    public class TransmittalViewModel
    {
        public int Identity { get; set; }
        public DateTime TransmittalDate { get; set; }
        public string Airline { get; set; }
        public string OriginBranch { get; set; }
        public string DestinationBranchCode { get; set; }
        public string TranmittalStatus { get; set; }
        public string MasterAirwayBill { get; set; }
        public string Cargo { get; set; }
        public string User { get; set; }
        public int TransNo { get; set; }
        public string AirwayBill { get; set; }

        public string TransmittalDateString
        {
            get { return TransmittalDate.ToString("MMM dd, yyyy"); }

        }
    }
}
