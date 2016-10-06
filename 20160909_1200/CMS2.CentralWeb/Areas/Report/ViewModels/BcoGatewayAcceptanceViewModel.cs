using System;
using CMS2.Common.Constants;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class BcoGatewayAcceptanceViewModel
    {
        public DateTime AcceptanceDate { get; set; }
        public string Gateway { get; set; }
        public string Driver { get; set; }
        public string Truck { get; set; }
        public string ScannedBy { get; set; }
        public string AcceptanceType { get; set; }
    }
}