using System;
using System.Collections.Generic;
using CMS2.Entities;

namespace CMS2.CentralWeb.Areas.Shipment.ViewModels
{
    public class TrackingEntryViewModel
    {
        public Guid EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }
        public List<TrackingEntryDetailsViewModel> TrackingEntryDetailsViewModels { get; set; } 
    }
}