using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_Shipper
    {
        public int id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(100)]
        public string company { get; set; }

        [StringLength(500)]
        public string pickupaddress { get; set; }

        [Required]
        [StringLength(50)]
        public string contact { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        public int? area_id { get; set; }
    }
}
