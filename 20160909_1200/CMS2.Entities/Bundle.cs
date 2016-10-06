using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class Bundle : BaseEntity
    {
        [Key]
        public Guid BundleId { get; set; }
        [Required]
        public DateTime BundleDate { get; set; }
        [Required]
        [MaxLength(15)]
        public string BundleNo { get; set; }
        [Required]
        public Guid BundleById { get; set; }
        [ForeignKey("BundleById")]
        public virtual Employee BundledBy { get; set; }
        public virtual List<PackageNumber> PackageNumbers { get; set; } 
        public Guid? ManifestAwbId { get; set; }
        public virtual ManifestAwb ManifestAwb { get; set; }
        public Guid? InboundId { get; set; }
        public virtual Inbound Inbound { get; set; }
        
    }
}
