using System;
using System.Collections.Generic;

namespace CMS2.Entities.TrackingEntities.Models
{
    public class AirwayBillViewModel
    {
        public string AirwayBillNo { get; set; }
        public List<string> CargoNos { get; set; }

        public string Airline { get; set; }

        public string Status { get; set; }

        public DateTime ShipmentDate { get; set; }

        public string ShipmentDateString
        {
            get { return ShipmentDate.ToString("MMM dd, yyyy"); }
        }

        public string ShipperName { get; set; }
        public string ConsigneeName { get; set; }
        public string OriginCity { get; set; }
        public string DestinationCity { get; set; }
        public string ServiceMode { get; set; }
        public string PaymentMode { get; set; }
        public decimal ShipmentTotalCharge { get; set; }

        public string ShipmentTotalChargeString
        {
            get { return ShipmentTotalCharge.ToString("N"); }
        }

        public string DeliveredBy { get; set; }
        public DateTime DeliveredOn { get; set; }
        public string DeliveredOnString {
            get { return DeliveredOn.ToString("MMM dd, yyyy"); }
        }
        public string ReceivedBy { get; set; }
        public DateTime ReceivedOn { get; set; }
        public string ReceivedOnString { get { return ReceivedOn.ToString("MMM dd, yyyy"); } }
        public string Remarks { get; set; }
        public string Notes { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
