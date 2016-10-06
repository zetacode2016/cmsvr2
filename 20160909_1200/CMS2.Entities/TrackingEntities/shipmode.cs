using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
    [Table("shipmode")]
    public partial class shipmode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(100)]
        public string cShipmodeCode { get; set; }

        [StringLength(100)]
        public string cShipmodeName { get; set; }
    }
}
