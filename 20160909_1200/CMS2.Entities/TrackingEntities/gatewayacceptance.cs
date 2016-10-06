using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
    [Table("gatewayacceptance")]
    public partial class gatewayacceptance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(20)]
        public string cUser { get; set; }

        [StringLength(20)]
        public string cBranch { get; set; }

        public DateTime? dDateTime { get; set; }

        [StringLength(50)]
        public string cGateway { get; set; }

        [StringLength(20)]
        public string cDriver { get; set; }

        [StringLength(20)]
        public string cRemarks { get; set; }

        [StringLength(20)]
        public string cCargo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nCount { get; set; }

    }
}
