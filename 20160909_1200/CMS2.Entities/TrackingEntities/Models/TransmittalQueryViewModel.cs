using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CMS2.Entities.TrackingEntities.Models
{
    public class TransmittalQueryViewModel
    {
        [DisplayName("Date")]
        public DateTime TransmittalDate { get; set; }
         [DisplayName("Airline")]
        public string cAirlineName { get; set; }
         [DisplayName("Origin")]
        public string OriginBranch { get; set; }
         [DisplayName("Status/Type")]
        public string cStatusName { get; set; }
         [DisplayName("Master AWB")]
        public string MasterAirwayBill { get; set; }
        public List<TransmittalViewModel> TransmittalViewModels { get; set; } 

    }
}
