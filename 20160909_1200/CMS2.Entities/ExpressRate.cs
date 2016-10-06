using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class ExpressRate : BaseEntity
    {
        [Key]
        public Guid ExpressRateId { get; set; }
        public Guid RateMatrixId { get; set; }
        public RateMatrix RateMatrix { get; set; }
        [Required]
        [DefaultValue(0)]
        [DisplayName("Min Weight")]
        public decimal MinimumWeight { get; set; }
        [Required]
        [DefaultValue(0)]
        [DisplayName("Max Weight")]
        public decimal MaximumWeight { get; set; }
        [Required]
        [DefaultValue(0)]
        [DisplayName("Cost")]
        public decimal Cost { get; set; }
        [Required]
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }
         [DisplayName("Origin City")]
        public Guid OriginCityId { get; set; }
        [ForeignKey("OriginCityId")]
        public virtual City OriginCity { get; set; }
        [DisplayName("Destination City")]
        public Guid DestinationCityId { get; set; }
        [ForeignKey("DestinationCityId")]
        public virtual City DestinationCity { get; set; }


        [NotMapped]
        [DisplayName("Min Weight")]
        public string MinimumWeightString { get { return MinimumWeight.ToString("N"); } }
        [NotMapped]
        [DisplayName("Max Weight")]
        public string MaximumWeightString { get { return MaximumWeight.ToString("N"); } }
        [NotMapped]
        [DisplayName("Cost")]
        public string CostString { get { return Cost.ToString("N"); } }
        [NotMapped]
        [DisplayName("Effective Date")]
        public string EffectiveDateString { get { return EffectiveDate.ToString("MMM dd, yyyy"); } }
    }
}
