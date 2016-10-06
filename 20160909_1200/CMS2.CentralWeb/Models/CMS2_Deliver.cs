using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_Deliver
    {
        public int id { get; set; }

        [StringLength(50)]
        public string awb { get; set; }

        [StringLength(50)]
        public string cargo { get; set; }

        [StringLength(50)]
        public string deliverystatus { get; set; }

        [StringLength(50)]
        public string receivedby { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }
    }
}
