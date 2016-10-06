using System.Collections.Generic;
using System.Drawing;

namespace CMS2.CentralWeb.Areas.Shipment.ViewModels
{
    public class DeliveryDetailsViewModel
    {
        public string AirwayBillNo { get; set; }
        public string DateDelivered { get; set; }
        public string DeliveredBy { get; set; }
        public string DeliveryStatus { get; set; }
        public string DeliveryRemark { get; set; }
        public string Note { get; set; }
        public string ReceivedBy { get; set; }
        public byte[] Signature { get; set; }
        public List<string> PackageNos { get; set; } 
    }
}