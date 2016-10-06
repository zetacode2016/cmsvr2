namespace CMS2.Entities.TrackingEntities.Models
{
    public class InboundAwbViewModel
    {
        public string AirwayBill { get; set; }
        public string DestinationBranchCode { get; set; }
        public string Status { get; set; }
        public int CountOutbound { get; set; } // from Airline Transmittal AWB item count
        public int CountInbound { get; set; }
        public int Difference { get { return CountOutbound - CountInbound; } }
        public decimal ActualWeight { get; set; }
        public decimal TotalAmount { get; set; }
        public string User { get; set; }

        public string CountOutboundString { get { return CountOutbound.ToString("###"); } }
        public string CountInboundString { get { return CountInbound.ToString("###"); } }
        public string DifferenceString { get { return Difference.ToString("###"); } }
        public string ActualWeightString { get { return ActualWeight.ToString("N"); } }
        public string TotalAmountString { get { return TotalAmount.ToString("N"); } }
    }
}
