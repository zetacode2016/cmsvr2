using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_Group
    {
        public int id { get; set; }

        [StringLength(250)]
        public string desc { get; set; }

        [StringLength(250)]
        public string address { get; set; }

        [StringLength(250)]
        public string tels { get; set; }

        [StringLength(50)]
        public string fax { get; set; }

        [StringLength(10)]
        public string locationcode { get; set; }
    }
}
