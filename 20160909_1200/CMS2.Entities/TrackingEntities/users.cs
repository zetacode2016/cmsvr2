using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities.TrackingEntities
{
   
    public partial class users
    {
        [StringLength(20)]
        public string cUsername { get; set; }

        [StringLength(20)]
        public string cPassword { get; set; }

        [StringLength(20)]
        public string cBranch { get; set; }

        public bool? lActive { get; set; }
        
        public bool? lAdmin { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal nIdentity { get; set; }

        [StringLength(20)]
        public string cCity { get; set; }

        [StringLength(100)]
        public string cBco { get; set; }
    }
}
