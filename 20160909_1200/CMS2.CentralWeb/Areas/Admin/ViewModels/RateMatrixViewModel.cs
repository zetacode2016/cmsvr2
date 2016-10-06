using System;
using System.Collections.Generic;
using System.ComponentModel;
using CMS2.Entities;

namespace CMS2.CentralWeb.Areas.Admin.ViewModels
{
    public class RateMatrixViewModel
    {
        public Guid RateMatrixId { get; set; }
        [DisplayName("Applicable Rate")]
        public Guid ApplicableRateId { get; set; }
        public ApplicableRate ApplicableRate { get; set; }
        [DisplayName("Commodity Type")]
        public Guid CommodityTypeId { get; set; }
        public CommodityType CommodityType { get; set; }
        public string CommodityTypeName { get; set; }
        [DisplayName("Service Type")]
        public Guid ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public string ServiceTypeName { get; set; }
        [DisplayName("Service Mode")]
        public Guid ServiceModeId { get; set; }
        public ServiceMode ServiceMode { get; set; }
        public string ServiceModeName { get; set; }
        [DisplayName("Origin City")]
        public Guid OriginCityId { get; set; }
        public City OriginCity { get; set; }
        [DisplayName("Destination City")]
        public Guid DestinationCityId { get; set; }
        public City DestinationCity { get; set; }
        public virtual List<ExpressRate> ExpressRates { get; set; }

            // options
        [DisplayName("Has Fuel Surcharge")]
        public bool HasFuelCharge { get; set; }
        [DisplayName("Has Delivery Fee")]
        public bool HasDeliveryFee { get; set; }
        [DisplayName("Has AWB Fee")]
        public bool HasAwbFee { get; set; }
        [DisplayName("Has Insurance")]
        public bool HasInsurance { get; set; }
        [DisplayName("Has Dangerous Fee")]
        public bool HasDangerousFee { get; set; }
        [DisplayName("Has Perishable Fee")]
        public bool HasPerishableFee { get; set; }
        [DisplayName("Is Vatable")]
        public bool IsVatable { get; set; }
        [DisplayName("Apply EVM")]
        public bool ApplyEvm { get; set; }
        [DisplayName("Apply Weight")]
        public bool ApplyWeight { get; set; }
        [DisplayName("Has Valuation Charge")]
        public bool HasValuationCharge { get; set; }
    }
}