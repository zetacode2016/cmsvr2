using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class GatewayInboundSummaryViewModel
    {
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Branch Corp Office")]
        public string Origin { get; set; }
        [DisplayName("Gateway")]
        public string Gateway { get; set; }
        public List<GatewayInboundViewModel> GatewayInboundViewModels { get; set; }
    }
}