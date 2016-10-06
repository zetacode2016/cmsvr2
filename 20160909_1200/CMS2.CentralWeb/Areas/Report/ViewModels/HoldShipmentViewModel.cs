using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class HoldShipmentViewModel
    {
        public string AirwayBillNo { get; set; }
        public string Quantity { get; set; }
        public string TransactionDate { get; set; }
        public string Aging { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public string EndorsedBy { get; set; }
        public string ScannedBy { get; set; }
    }
}