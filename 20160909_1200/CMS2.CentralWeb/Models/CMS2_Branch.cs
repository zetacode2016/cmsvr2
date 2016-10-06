using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_Branch
    {
        [Key]
        public int code { get; set; }

        [StringLength(25)]
        public string desc { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        [StringLength(20)]
        public string tels { get; set; }

        [StringLength(20)]
        public string fax { get; set; }

        [StringLength(3)]
        public string locationcode { get; set; }

        public int? group_id { get; set; }
    }
}
