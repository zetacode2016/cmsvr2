using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.CentralWeb.Models.CMS1
{
   
    [Table("destination")]
    public partial class destination
    {
        [StringLength(25)]
        public string brancharea { get; set; }

        [Key]
        [StringLength(3)]
        public string code { get; set; }

        [StringLength(25)]
        public string desc { get; set; }

        public int? region { get; set; }
    }
}
