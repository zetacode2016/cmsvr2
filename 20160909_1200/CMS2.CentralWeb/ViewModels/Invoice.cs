
namespace CMS2.CentralWeb.ViewModels
{
    public class Invoice
    {
        public string AirwayBillNo { get; set; }
        public string OriginArea { get; set; }
        public string DestinationArea { get; set; }
        public string Consignee { get; set; }
        public string Shipper { get; set; }
        public string AcceptedBy { get; set; }
        public string DateAccepted { get; set; }
        public string AcceptedArea { get; set; }
        public string Quantity { get; set; }
        public string ActualGrossWeight { get; set; }
        public string ChargeableWeight { get; set; }
        public string WeightCharge { get; set; }
        public string Discount { get; set; }
        public string ServiceMode { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentTerm { get; set; }
        public string HasPerishableGoods { get; set; }
        public string HasAwbfee { get; set; }
        public string HasFreightCollectCharge { get; set; }
        public string IsNonVatable { get; set; }
        public string IsNonVatValuation { get; set; }
        public string HasNonVatInsurance { get; set; }
        public string HasNonVatWeightCharge { get; set; }
        public string DeclaredValue { get; set; }
        public string OtherChargesDesc { get; set; }
        public string OtherChargesAmount { get; set; }
        public string AwbFee { get; set; }
        public string FreigtCollectCharge { get; set; }
        public string FuelSurcharge { get; set; }
        public string PeracFee { get; set; }
        public string Insurance { get; set; }
        public string CratingFee { get; set; }
        public string DangerousFee { get; set; }
        public string DeliveryFee { get; set; }
        public string ValuationAmount { get; set; }
        public string SubTotal { get; set; }
        public string VatAmount { get; set; }
        public string TotalAmount { get; set; }
        public string Branch { get; set; }

    }
}