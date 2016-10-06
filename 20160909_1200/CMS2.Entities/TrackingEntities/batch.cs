using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
    [Table("batch")]
    public partial class batch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(100)]
        public string cBatchCode { get; set; }

        [StringLength(100)]
        public string cBatchName { get; set; }
    }
}
