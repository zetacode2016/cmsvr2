using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
    
    [Table("destination")]
    public partial class destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(20)]
        public string cDestinationCode { get; set; }

        [StringLength(20)]
        public string cDestinationName { get; set; }
    }
}
