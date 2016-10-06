using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.CentralWeb.Models.CMS1
{
    [Table("payment")]
    public partial class payment
    {
        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string airwaybill { get; set; }

        [Required]
        [StringLength(100)]
        public string clientname { get; set; }

        [Required]
        [StringLength(3)]
        public string paymentcode { get; set; }

        [Required]
        [StringLength(6)]
        public string iscash { get; set; }

        public DateTime paymentdate { get; set; }

        public DateTime postingdate { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        [StringLength(100)]
        public string orpr { get; set; }

        [StringLength(200)]
        public string remarks { get; set; }

        [StringLength(50)]
        public string branch { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        [StringLength(50)]
        public string postedby { get; set; }
    }
}
