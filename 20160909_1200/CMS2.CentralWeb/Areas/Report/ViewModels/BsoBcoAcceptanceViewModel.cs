using System;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class BsoBcoAcceptanceViewModel
    {
        public DateTime AcceptanceDate { get; set; }
        public Guid BranchCorpOfficeId { get; set; }
        public string BranchCorpOffice { get; set; }
        public Guid ScannedById { get; set; }
        public string ScannedBy { get; set; }
        public Guid DriverId { get; set; }
        public string Driver { get; set; }
        public Guid AreaId { get; set; }
        public string Area { get; set; }
    }
}