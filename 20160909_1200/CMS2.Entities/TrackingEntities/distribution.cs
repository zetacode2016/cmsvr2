using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
   [Table("distribution")]
    public partial class distribution
    {
        [StringLength(20)]
        public string cPlateNo { get; set; }

        [StringLength(20)]
        public string cDriver { get; set; }

        [StringLength(20)]
        public string cChecker { get; set; }

        [StringLength(20)]
        public string cAwb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal nCount { get; set; }

        public DateTime? dDateTime { get; set; }

        [StringLength(20)]
        public string cUser { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(20)]
        public string cBranch { get; set; }
    }
}
