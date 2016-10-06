using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class GatewayTransmittal:BaseEntity
    {    
        [Key]
        public Guid GatewayTransmittalId { get; set; }
        public Guid TransmittalStatusId { get; set; }
        public virtual TransmittalStatus TransmittalStatus { get; set; }
        public DateTime DateScanned { get; set; }
        public Guid ScannedById { get; set; }
        [ForeignKey("ScannedById")]
        public virtual Employee ScannedBy { get; set; }
        public Guid GatewayId { get; set; }
        public virtual Gateway Gateway { get; set; }

        public List<GatewayTransmittalPackageNumber> GatewayTransmittalPackageNumbers { get; set; } 
    }
}
