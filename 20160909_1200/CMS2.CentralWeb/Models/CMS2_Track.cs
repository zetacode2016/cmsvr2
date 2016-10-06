using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_Track
    {
        public int id { get; set; }

        [StringLength(50)]
        public string awb { get; set; }

        [StringLength(50)]
        public string cargo { get; set; }
    }
}
