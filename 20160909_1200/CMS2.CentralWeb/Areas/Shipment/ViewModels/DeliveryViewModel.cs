using System;
using System.Collections.Generic;
using System.Drawing;

namespace CMS2.CentralWeb.Areas.Shipment.ViewModels
{
    public class DeliveryViewModel
    {
        public Guid DeliveryId { get; set; }
        public string AirwayBillNo { get; set; }
        public string DateDelivered { get; set; }
        public string DeliveredBy { get; set; }
        public string DeliveryStatus { get; set; }
        
    }
}