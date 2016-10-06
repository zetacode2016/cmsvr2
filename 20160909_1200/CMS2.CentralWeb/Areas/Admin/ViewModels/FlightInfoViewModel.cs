using System;
using System.ComponentModel;
using CMS2.Entities;

namespace CMS2.CentralWeb.Areas.Admin.ViewModels
{
    public class FlightInfoViewModel
    {
        public Guid FlightInfoId { get; set; }
        [DisplayName("Flight No")]
        public string FlightNo { get; set; }
        [DisplayName("ETD")]
        public string ETD { get; set; }
        [DisplayName("ETA")]
        public string ETA { get; set; }
        //[DisplayName("Gateway")]
        //public Guid GatewayId { get; set; }
        //public Gateway Gateway { get; set; }
        [DisplayName("Origin City")]
        public Guid OriginCityId { get; set; }
        public City OriginCity { get; set; }
        [DisplayName("Destination City")]
        public Guid DestinationCityId { get; set; }
        public City DestinationCity { get; set; }
        public Guid OriginBcoId { get; set; }
        public Guid DestinationBcoId { get; set; }
    }
}