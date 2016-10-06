using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
    
    [Table("commodity")]
    public partial class commodity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(20)]
        public string cCommodityCode { get; set; }

        [StringLength(20)]
        public string cCommodityName { get; set; }
    }
}
