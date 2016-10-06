using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class RevenueUnit:BaseEntity
    {
        [Key]
        public Guid RevenueUnitId { get; set; }
        [Required]
        [MaxLength(50)]
        public string RevenueUnitName { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [DisplayName("Revenue Unit Type")]
        public Guid RevenueUnitTypeId { get; set; }
        [ForeignKey("RevenueUnitTypeId")]
        public virtual RevenueUnitType RevenueUnitType { get; set; }
        [DisplayName("Cluster")]
        public Guid ClusterId { get; set; }
        public virtual Cluster Cluster { get; set; }
        [DisplayName("City")]
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
        [MaxLength(300)]
        [DisplayName("Street")]
        public string StreetAddress { get; set; }
        [MaxLength(15)]
        [DisplayName("Contact No1")]
        public string ContactNo1 { get; set; }
        [MaxLength(15)]
        [DisplayName("Contact No2")]
        public string ContactNo2 { get; set; }
        [MaxLength(15)]
        public string Fax { get; set; }
        [MaxLength(5)]
        public string ZipCode { get; set; }
    }
}
