using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class BcoGatewayAcceptanceSummaryViewModel
    {
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime AcceptanceDate { get; set; }
        [DisplayName("Branch Corp Office")]
        public Guid BranchCorpOfficeId { get; set; }
        [DisplayName("Gateway")]
        public string Gateway { get; set; }
        public List<BcoGatewayAcceptanceViewModel> BcoGatewayAcceptanceViewModels { get; set; }
    }
}