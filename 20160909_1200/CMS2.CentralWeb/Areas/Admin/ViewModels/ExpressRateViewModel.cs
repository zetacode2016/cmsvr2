using System;
using System.Collections.Generic;

namespace CMS2.CentralWeb.Areas.Admin.ViewModels
{
    /// <summary>
    /// Used in RateMatrix Details Display
    /// </summary>
    public class ExpressRateViewModel
    {
        public Guid RateMatrixId { get; set; }
        public Guid RateOriginCityId { get; set; }
        public string RateOriginCity { get; set; }
        public Guid RateDestinationCityId { get; set; }
        public string RateDestinationCity { get; set; }
        public List<decimal> Costs { get; set; }
    }

    //public class RateCost
    //{
    //    public decimal Cost { get; set; }
    //}
}