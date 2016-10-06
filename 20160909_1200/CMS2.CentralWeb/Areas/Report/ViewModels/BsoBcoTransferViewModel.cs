using System;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class BsoBcoTransferViewModel
    {
        public DateTime TransactionDate { get; set; }
        public string BranchCorpOffice { get; set; }
        public string TransferFrom  { get; set; }
        public string TransferTo { get; set; }
        public string Driver { get; set; }
        public string DriverShortcut { get; set; }
        public string Truck { get; set; }
        public string ScannedBy { get; set; } // this is the actual username as it appears on record
    }
}