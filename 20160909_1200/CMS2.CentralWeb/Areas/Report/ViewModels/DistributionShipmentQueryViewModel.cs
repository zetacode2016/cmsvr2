using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class DistributionShipmentQueryViewModel
    {
        public DateTime DistributionDate { get; set; }
        public string Username { get; set; }
        public string Driver { get; set; }
        public string PlateNo { get; set; }
        public string Checker { get; set; }
        public string Area { get; set; }
        public string BranchCorpOffice { get; set; }
    }
}