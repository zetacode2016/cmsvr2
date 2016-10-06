using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_Dimension
    {
        public int id { get; set; }

        [StringLength(50)]
        public string awb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal qty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal agw { get; set; }

        [Column(TypeName = "numeric")]
        public decimal l { get; set; }

        [Column(TypeName = "numeric")]
        public decimal w { get; set; }

        [Column(TypeName = "numeric")]
        public decimal h { get; set; }
    }
}
