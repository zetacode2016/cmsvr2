using System;
using System.ComponentModel;

namespace CMS2.CentralWeb.Areas.Admin.ViewModels
{
    public class RateMatrixCopyCreateViewModel
    {
        [DisplayName("Source Applicable Rate")]
        public Guid FromApplicableRateId { get; set; }
        [DisplayName("Target Applicable Rate")]
        public Guid ToApplicableRateId { get; set; }
    }
}