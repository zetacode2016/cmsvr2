using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.WebPages;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_Acceptance
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }

        [StringLength(50)]
        public string awb { get; set; }

        [StringLength(50)]
        public string accountno { get; set; }

        [StringLength(50)]
        public string shipper { get; set; }

        [StringLength(50)]
        public string address1 { get; set; }

        [StringLength(50)]
        public string city1 { get; set; }

        [StringLength(50)]
        public string consignee { get; set; }

        [StringLength(50)]
        public string address2 { get; set; }

        [StringLength(50)]
        public string area { get; set; }

        [StringLength(50)]
        public string city2 { get; set; }

        [StringLength(50)]
        public string commodity { get; set; }

        [StringLength(50)]
        public string servicemode { get; set; }

        [StringLength(50)]
        public string paymode { get; set; }

        [StringLength(50)]
        public string applicablerates { get; set; }

        [StringLength(50)]
        public string declaredvalue { get; set; }

        [StringLength(50)]
        public string craftingfee { get; set; }

        [StringLength(50)]
        public string dangerousfee { get; set; }

        [StringLength(50)]
        public string chargablewt { get; set; }

        [StringLength(50)]
        public string weightcharge { get; set; }

        [StringLength(50)]
        public string awbfee { get; set; }

        [StringLength(50)]
        public string valuation { get; set; }

        [StringLength(50)]
        public string delivery { get; set; }

        [StringLength(50)]
        public string freightcollect { get; set; }

        [StringLength(50)]
        public string peracfee { get; set; }

        [StringLength(50)]
        public string fuelsurcharge { get; set; }

        [StringLength(50)]
        public string other { get; set; }

        [StringLength(50)]
        public string discount { get; set; }

        [StringLength(50)]
        public string insurance { get; set; }

        [StringLength(50)]
        public string subtotal { get; set; }

        [StringLength(50)]
        public string evat { get; set; }

        [StringLength(50)]
        public string grandtotal { get; set; }

        [StringLength(50)]
        public string qty1 { get; set; }

        [StringLength(50)]
        public string qty2 { get; set; }

        [StringLength(50)]
        public string qty3 { get; set; }

        [StringLength(50)]
        public string qty4 { get; set; }

        [StringLength(50)]
        public string qty5 { get; set; }

        [StringLength(50)]
        public string qty6 { get; set; }

        [StringLength(50)]
        public string qty7 { get; set; }

        [StringLength(50)]
        public string qty8 { get; set; }

        [StringLength(50)]
        public string l1 { get; set; }

        [StringLength(50)]
        public string l2 { get; set; }

        [StringLength(50)]
        public string l3 { get; set; }

        [StringLength(50)]
        public string l4 { get; set; }

        [StringLength(50)]
        public string l5 { get; set; }

        [StringLength(50)]
        public string l6 { get; set; }

        [StringLength(50)]
        public string l7 { get; set; }

        [StringLength(50)]
        public string l8 { get; set; }

        [StringLength(50)]
        public string w1 { get; set; }

        [StringLength(50)]
        public string w2 { get; set; }

        [StringLength(50)]
        public string w3 { get; set; }

        [StringLength(50)]
        public string w4 { get; set; }

        [StringLength(50)]
        public string w5 { get; set; }

        [StringLength(50)]
        public string w6 { get; set; }

        [StringLength(50)]
        public string w7 { get; set; }

        [StringLength(50)]
        public string w8 { get; set; }

        [StringLength(50)]
        public string h1 { get; set; }

        [StringLength(50)]
        public string h2 { get; set; }

        [StringLength(50)]
        public string h3 { get; set; }

        [StringLength(50)]
        public string h4 { get; set; }

        [StringLength(50)]
        public string h5 { get; set; }

        [StringLength(50)]
        public string h6 { get; set; }

        [StringLength(50)]
        public string h7 { get; set; }

        [StringLength(50)]
        public string h8 { get; set; }

        [StringLength(50)]
        public string agw1 { get; set; }

        [StringLength(50)]
        public string agw2 { get; set; }

        [StringLength(50)]
        public string agw3 { get; set; }

        [StringLength(50)]
        public string agw4 { get; set; }

        [StringLength(50)]
        public string agw5 { get; set; }

        [StringLength(50)]
        public string agw6 { get; set; }

        [StringLength(50)]
        public string agw7 { get; set; }

        [StringLength(50)]
        public string agw8 { get; set; }

        [NotMapped]
        public string subtotalstring { get { return subtotal.AsDecimal().ToString("N"); } }
        [NotMapped]
        public decimal vatamount { get; set; }
        [NotMapped]
        public string vatamountstring { get { return vatamount.ToString("N"); } }
        [NotMapped]
        public string grantotalstring {get { return grandtotal.AsDecimal().ToString("N"); }}
        [NotMapped]
        public string dateacceptedstring { get { return DateTime.Today.ToString("MMM dd, yyyy"); } }
        [NotMapped]
        public string acceptedbystring { get { return "Bernard"; } }
        [NotMapped]
        public CMS2_TruckInfo TruckInfo { get; set; }
        [NotMapped]
        public int Quantity { get; set; }
    }
}
