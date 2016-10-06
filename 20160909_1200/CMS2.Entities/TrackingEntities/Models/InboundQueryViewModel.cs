using System;
using System.ComponentModel;

namespace CMS2.Entities.TrackingEntities.Models
{
    public class InboundQueryViewModel
    {
        [DisplayName("Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Airline")]
        public string cAirlineName { get; set; }
        [DisplayName("Destination Branch")]
        public string DestinationBranch { get; set; } // Destination Branch
        public string CargoNo { get; set; }
        public string User { get; set; }
        [DisplayName("Master AirwayBill")]
        public string MasterAirwayBill { get; set; }



    }
}
