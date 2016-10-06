using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class EmployeePositionMapping : BaseEntity
    {
        [Key]
        public Guid EmployeePositionMappingId { get; set; }
        [DisplayName("Employee")]
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        [DisplayName("Position")]
        public Guid PositionId { get; set; }
        public virtual Position Position { get; set; }
        [DisplayName("Department")]
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [Required]
        [DisplayName("Date Assigned")]
        [DataType(DataType.Date)]
        public DateTime DateAssigned { get; set; }
        
        [DisplayName("Date Assigned")]
        [NotMapped]
        public string DateAssignedString { get { return DateAssigned.ToString("MMM dd, yyyy"); } }

        public Guid AssignedLocationId { get; set; }
        public virtual dynamic AssignedLocation { get; set; }
        [MaxLength(30)]
        public string LocationAssignment { get; set; }
    }
}
