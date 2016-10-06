using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
    
    public partial class remarks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(20)]
        public string cRemarkCode { get; set; }

        [StringLength(20)]
        public string cRemarkName { get; set; }
    }
}
