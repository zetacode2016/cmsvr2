using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
   
    [Table("airline")]
    public partial class airline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(20)]
        public string cAirlineCode { get; set; }

        [StringLength(20)]
        public string cAirlineName { get; set; }
    }
}
