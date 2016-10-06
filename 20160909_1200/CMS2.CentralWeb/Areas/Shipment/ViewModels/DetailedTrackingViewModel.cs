

using System;

namespace CMS2.CentralWeb.Areas.Shipment.ViewModels
{
    public class DetailedTrackingViewModel
    {
        public DateTime TransactionDate { get; set; }
        public string Weekday {get { return TransactionDate.DayOfWeek.ToString(); }}
        public string Date {get { return TransactionDate.Date.ToString("dd/MM/yyyy"); }}
        public string Time {get { return TransactionDate.ToString("hh:mm tt"); }}
        public string Status { get; set; }
        public string Location { get; set; } //Location
        public string ScannedBy { get; set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
        public string Column4 { get; set; }

    }
}