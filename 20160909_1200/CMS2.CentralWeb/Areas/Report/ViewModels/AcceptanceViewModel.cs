using System.Collections.Generic;
using CMS2.CentralWeb.Models;

namespace CMS2.CentralWeb.Areas.Report.ViewModels
{
    public class AcceptanceViewModel
    {
        public CMS2_Acceptance Acceptance{get;set;}
        public List<CMS2_Track> Tracks { get; set; }
    }
}