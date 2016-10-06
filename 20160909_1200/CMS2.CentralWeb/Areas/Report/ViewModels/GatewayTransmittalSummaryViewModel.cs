using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class GatewayTransmittalSummaryViewModel
    {
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Origin")]
        public string Origin { get; set; }
        [DisplayName("Gateway")]
        public string Gateway { get; set; }
        [DisplayName("Commodity Type")]
        public string CommodityType { get; set; }
        public List<GatewayTransmittalViewModel> GatewayTransmittalViewModels { get; set; }
    }
}