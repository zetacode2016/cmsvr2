using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class BcoGatewayTransferSummaryViewModel
    {
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Branch Corp Office")]
        public Guid BranchCorpOfficeId { get; set; }
        public List<BsoBcoTransferViewModel> BsoBcoTransferViewModels { get; set; }
    }
}