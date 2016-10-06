using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class City : BaseEntity
    {
        [Key]
        public Guid CityId { get; set; }
        [Required]
       [StringLength(3)]
        [DisplayName("City Code")]
        public string CityCode { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("City Name")]
        public string CityName { get; set; }
        [DisplayName("Branch Corp Office")]
        public Guid BranchCorpOfficeId { get; set; }
        [ForeignKey("BranchCorpOfficeId")]
        public virtual BranchCorpOffice BranchCorpOffice { get; set; }
        public List<RevenueUnit> RevenueUnits { get; set; }
    }
}
