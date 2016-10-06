using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.CentralWeb.Areas.Shipment.ViewModels
{
    public class TrackingViewModel
    {
        public string Weekday { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public string Location { get; set; } //City of the user
    }
}