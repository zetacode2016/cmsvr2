using System;
using System.ComponentModel.DataAnnotations;

namespace CMS2.CentralWeb.Models
{
    public partial class CMS2_ErrorLogs
    {
        public int ID { get; set; }

        public int? ExceptionNo { get; set; }

        [StringLength(1000)]
        public string ExceptionMessage { get; set; }

        public DateTime? ErrorLogDate { get; set; }
    }
}
