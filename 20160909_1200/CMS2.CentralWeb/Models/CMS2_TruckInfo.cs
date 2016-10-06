using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_TruckInfo
    {
        public int id { get; set; }

        [StringLength(50)]
        public string truckno { get; set; }

        [StringLength(250)]
        public string driver { get; set; }

        public int? group_id { get; set; }

        public int? branch_id { get; set; }

        public int? area_id { get; set; }

        public int? franchise_id { get; set; }

        [StringLength(10)]
        public string plate_no { get; set; }

        [StringLength(20)]
        public string contact_no { get; set; }
    }
}
