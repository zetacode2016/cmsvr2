using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class BranchCorpOffice : BaseEntity
    {
        [Key]
        [DisplayName("BCO")]
        public Guid BranchCorpOfficeId { get; set; }
        [MaxLength(50)]
        [DisplayName("BCO Name")]
        public string BranchCorpOfficeName { get; set; }
        [DisplayName("Region")]
        public Guid RegionId { get; set; }
        public virtual Region Region { get; set; }
        
        [MaxLength(250)]
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
        public List<Cluster> Clusters { get; set; }
    }
}
