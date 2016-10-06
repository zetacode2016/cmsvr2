using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Areas.Admin.ViewModels
{
    public class CityViewModel
    {
        public Guid CityId { get; set; }
        [DisplayName("City Code")]
        [Required]
        [MaxLength(3), MinLength(3)]
        public string CityCode { get; set; }
        [DisplayName("City Name")]
        public string CityName { get; set; }
        [DisplayName("BCO")]
        public Guid BranchCorpOfficeId { get; set; }
    }
}