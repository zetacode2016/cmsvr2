using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.CentralWeb.Models.CMS1
{
    [Table("cargo")]
    public partial class cargo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long airwaybill { get; set; }

        public int? branchcode { get; set; }

        [StringLength(50)]
        public string issuedby { get; set; }

        public DateTime? issueddate { get; set; }

        [StringLength(50)]
        public string pickedupby { get; set; }

        [StringLength(3)]
        public string origincode { get; set; }

        [StringLength(3)]
        public string destcode { get; set; }

        [StringLength(50)]
        public string consigneename { get; set; }

        [StringLength(50)]
        public string consigneecompany { get; set; }

        [StringLength(60)]
        public string consigneeaddress { get; set; }

        [StringLength(50)]
        public string consigneetels { get; set; }

        [StringLength(50)]
        public string consigneeemail { get; set; }

        [StringLength(50)]
        public string shippername { get; set; }

        [StringLength(50)]
        public string shippercompany { get; set; }

        [StringLength(60)]
        public string shipperaddress { get; set; }

        [StringLength(50)]
        public string shippertels { get; set; }

        [StringLength(50)]
        public string shipperemail { get; set; }

        [StringLength(50)]
        public string descriptionofgoods { get; set; }

        public DateTime? processingdate { get; set; }

        [StringLength(2)]
        public string servicecode { get; set; }

        [StringLength(2)]
        public string paymentcode { get; set; }

        [Column(TypeName = "money")]
        public decimal? declaredvalue { get; set; }

        [StringLength(50)]
        public string otherchargesdesc { get; set; }

        [Column(TypeName = "money")]
        public decimal? othersamount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? actualweight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? chargeableweight { get; set; }

        [Column(TypeName = "money")]
        public decimal? weightcharge { get; set; }

        [Column(TypeName = "money")]
        public decimal? awbfee { get; set; }

        [Column(TypeName = "money")]
        public decimal? valuation { get; set; }

        [Column(TypeName = "money")]
        public decimal? deliveryfee { get; set; }

        [Column(TypeName = "money")]
        public decimal? peracfee { get; set; }

        [Column(TypeName = "money")]
        public decimal? freightcollect { get; set; }

        [Column(TypeName = "money")]
        public decimal? fuelsurcharge { get; set; }

        [Column(TypeName = "money")]
        public decimal? cratingfee { get; set; }

        [Column(TypeName = "money")]
        public decimal? discount { get; set; }

        [Column(TypeName = "money")]
        public decimal? insurance { get; set; }

        [Column(TypeName = "money")]
        public decimal? subtotal { get; set; }

        [Column(TypeName = "money")]
        public decimal? evat { get; set; }

        [Column(TypeName = "money")]
        public decimal? grandtotal { get; set; }

        public bool? noawb { get; set; }

        public bool? nofcc { get; set; }

        public bool? novat { get; set; }

        public bool? noval { get; set; }

        public bool? noins { get; set; }

        public bool? nowc { get; set; }

        [StringLength(10)]
        public string encodedby { get; set; }

        public DateTime? encodeddate { get; set; }

        public byte? statuscode { get; set; }

        [StringLength(10)]
        public string statusby { get; set; }

        public DateTime? statusdate { get; set; }

        public bool hasrep { get; set; }

        public DateTime? hasrepdate { get; set; }

        [StringLength(30)]
        public string reportid { get; set; }

        [Column(TypeName = "money")]
        public decimal? Others2amount { get; set; }

        [StringLength(50)]
        public string consigneecode { get; set; }

        [StringLength(50)]
        public string shippercode { get; set; }

        [StringLength(50)]
        public string plateno { get; set; }

        [StringLength(50)]
        public string paymentterms { get; set; }

        public bool haspaid { get; set; }

        [StringLength(10)]
        public string lastupdateby { get; set; }

        public int? CPA_AWB { get; set; }

        public bool? RefID_AMP { get; set; }

        public string AccountNo { get; set; }

        [StringLength(10)]
        public string isTranshipment { get; set; }

        [Column(TypeName = "money")]
        public decimal? dangerousfee { get; set; }

        public bool? isTrans { get; set; }
    }
}
