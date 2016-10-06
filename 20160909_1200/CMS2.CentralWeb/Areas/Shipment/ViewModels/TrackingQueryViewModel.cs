using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CMS2.CentralWeb.Areas.Shipment.ViewModels
{
    public class TrackingQueryViewModel
    {
        [DisplayName("AirwayBill No")]
        public string AirwayBillNo { get; set; }
        public List<TrackingViewModel> TrackingViewModels { get; set; }
    }
}