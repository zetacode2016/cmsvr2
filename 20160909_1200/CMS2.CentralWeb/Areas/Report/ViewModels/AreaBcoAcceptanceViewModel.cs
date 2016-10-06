using System;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class AreaBcoAcceptanceViewModel
    {
        public DateTime AcceptanceDate { get; set; }
        public string ScannedBy { get; set; }
        public string Driver { get; set; }
        public string Checker { get; set; }
        public string CheckerShorcut { get; set; }
        public string Truck { get; set; }
        public string Area { get; set; }
        public string TruckArea { get { return Truck + "-" + Area;} }
        public string AcceptanceType { get; set; }
    }
}