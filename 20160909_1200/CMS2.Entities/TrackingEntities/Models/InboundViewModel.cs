using System;

namespace CMS2.Entities.TrackingEntities.Models
{
    public class InboundViewModel
    {
        public int Identity { get; set; }
        public string Cargo { get; set; }
        public string DestinationBranch { get; set; } // Destination Branch
        public DateTime TransactionDate { get; set; }
        public string User { get; set; }
        public string Airline { get; set; }
        public string AirwayBill { get; set; }
        public string MasterAirwayBill { get; set; }

    }
}
