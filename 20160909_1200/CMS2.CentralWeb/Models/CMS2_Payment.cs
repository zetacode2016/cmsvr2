using System;
using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_Payment
    {
        public int id { get; set; }

        public DateTime? collectiondate { get; set; }

        [StringLength(50)]
        public string awb { get; set; }

        [StringLength(50)]
        public string soano { get; set; }

        [StringLength(50)]
        public string amountpaid { get; set; }

        [StringLength(50)]
        public string withholdingtax { get; set; }

        [StringLength(50)]
        public string netcollection { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        [StringLength(50)]
        public string form { get; set; }

        [StringLength(50)]
        public string dateofcheck { get; set; }

        [StringLength(50)]
        public string bank { get; set; }

        [StringLength(50)]
        public string checkno { get; set; }
    }
}
