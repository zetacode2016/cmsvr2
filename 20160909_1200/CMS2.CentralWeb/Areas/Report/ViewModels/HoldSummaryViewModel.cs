using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class HoldSummaryViewModel
    {
        public HoldSummaryViewModel()
        {
            TransactionStart = DateTime.Now;
            TransactionEnd = DateTime.Now;
            
        }
        [DisplayName("Date From")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime TransactionStart { get; set; }
        [DisplayName("Date To")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime TransactionEnd { get; set; }
        [DisplayName("Location")]
        public string Location { get; set; }
        public string LocationName { get; set; }
        [DisplayName("Total AWB Count")]
        public string AwbCount { get; set; }
        [DisplayName("Total Pieces")]
        public string ItemCount { get; set; }
        public List<HoldShipmentViewModel> HoldShipmentViewModels { get; set; } 
    }
}