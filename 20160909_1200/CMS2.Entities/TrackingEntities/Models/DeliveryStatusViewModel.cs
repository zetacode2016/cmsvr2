using System;

namespace CMS2.Entities.TrackingEntities.Models
{
    public class DeliveryStatusViewModel
    {
        public DateTime StatusDate { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string User { get; set; }
        public string Airline { get; set; }
        public string Remark { get; set; }
        public string Note { get; set; }
        public string StatusDateString { get { return StatusDate.ToString("MMM dd, yyyy hh:mm:ss tt"); } }
    }
}
