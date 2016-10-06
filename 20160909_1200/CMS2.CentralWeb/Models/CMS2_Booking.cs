using System;
using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_Booking
    {
        public int id { get; set; }

        [StringLength(50)]
        public string accountno { get; set; }

        public int? shipperid { get; set; }

        public int? consigneeid { get; set; }

        public DateTime bookingdate { get; set; }

        [StringLength(100)]
        public string status { get; set; }

        public int? truckinfoid { get; set; }

        [StringLength(50)]
        public string action { get; set; }
    }
}
