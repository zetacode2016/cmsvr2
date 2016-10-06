using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
    [Table("origin")]
    public partial class origin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(100)]
        public string cOriginCode { get; set; }

        [StringLength(100)]
        public string cOriginName { get; set; }
    }
}
