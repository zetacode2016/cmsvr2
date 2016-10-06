using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
    [Table("gateway")]
    public class gateway
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(100)]
        public string cGatewayCode { get; set; }

        [StringLength(100)]
        public string cGatewayName { get; set; }
    }
}
