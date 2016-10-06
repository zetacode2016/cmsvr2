using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities

{
    
    [Table("area")]
    public partial class area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(20)]
        public string cAreaCode { get; set; }

        [StringLength(20)]
        public string cAreaName { get; set; }
    }
}
