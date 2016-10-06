using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Areas.Admin.ViewModels
{
    public class TranShipmentRouteViewModel
    {
        public Guid TransShipmentRouteId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Route Name")]
        public string TransShipmentRouteName { get; set; }
        [DisplayName("Origin City")]
        public Guid OriginCityId { get; set; }
        [DisplayName("Destination City")]
        public Guid DestinationCityId { get; set; }
        [DisplayName("Leg")]
        public Guid LegId { get; set; }
 
    }
}