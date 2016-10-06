using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
    [Table("flight")]
    public partial class flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(100)]
        public string cFlightCode { get; set; }

        [StringLength(100)]
        public string cFlightNo { get; set; }
    }
}
