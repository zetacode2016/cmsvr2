using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_Area
    {
        public int id { get; set; }

        [StringLength(50)]
        public string desc { get; set; }

        [StringLength(250)]
        public string address { get; set; }

        [StringLength(50)]
        public string tels { get; set; }

        [StringLength(250)]
        public string fax { get; set; }

        [StringLength(10)]
        public string locationcode { get; set; }

        public int? branch_id { get; set; }

        public int? franchse_id { get; set; }
    }
}
