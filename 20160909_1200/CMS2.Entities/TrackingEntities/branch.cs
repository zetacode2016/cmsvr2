using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
    [Table("branch")]
    public partial class branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(20)]
        public string cBranchCode { get; set; }

        [StringLength(20)]
        public string cBranchName { get; set; }
        [StringLength(20)]
        public string cCity { get; set; }
    }
}
