using System;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class BsoGatewayTransferViewModel
    {
        public DateTime TransactionDate { get; set; }
        public string BranchCorpOffice { get; set; }
        public Guid TransferFromId { get; set; }
        public string TransferFrom  { get; set; }
        public Guid TransferToId { get; set; }
        public string TransferTo { get; set; }
        public Guid DriverId { get; set; }
        public string Driver { get; set; }
        public Guid TruckId { get; set; }
        public string Truck { get; set; }
    }
}