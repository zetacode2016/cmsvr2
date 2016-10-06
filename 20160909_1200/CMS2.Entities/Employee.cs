using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class Employee : BaseEntity
    {
        [Key]
        public Guid EmployeeId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [MaxLength(30)]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [MaxLength(30)]
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [NotMapped]
        [DisplayName("Fullname")]
        public string FullName { get { return LastName + ", " + FirstName + " " + MiddleName; } }
        [Required]
        [DisplayName("Birthdate")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [MaxLength(15)]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [MaxLength(15)]
        [DisplayName("Mobile No")]
        public string Mobile { get; set; }
        [MaxLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }
        [MaxLength(30)]
        [DisplayName("Dirver's License No")]
        public string DriversLicenseNo { get; set; }
        [DisplayName("Driver's License Expiration")]
        [DataType(DataType.Date)]
        public DateTime? DriversLicenseExpiration { get; set; }
        public virtual List<EmployeePositionMapping> EmployeePositionMappings { get; set; }

        [NotMapped]
        [DisplayName("Birthdate")]
        public string BirthdateString { get { return Birthdate.ToString("MMM dd, yyyy"); } }
    }
}
