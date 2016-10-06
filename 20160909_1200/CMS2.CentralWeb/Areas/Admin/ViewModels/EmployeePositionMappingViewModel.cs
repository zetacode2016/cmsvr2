using System;
using System.ComponentModel;
using CMS2.Entities;

namespace CMS2.CentralWeb.Areas.Admin.ViewModels
{
    public class EmployeePositionMappingViewModel
    {
        public Guid EmployeePositionMappingId { get; set; }
        [DisplayName("Employee")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [DisplayName("Position")]
        public Guid PositionId { get; set; }
        public Position Position { get; set; }
        [DisplayName("Department")]
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        [DisplayName("Date Assigned")]
        public DateTime DateAssigned { get; set; }
        
        [DisplayName("Date Assigned")]
        public string DateAssignedString { get { return DateAssigned.ToString("MMM dd, yyyy"); } }

        public Guid AssignedLocationId { get; set; }
        public dynamic AssignedLocation { get; set; }
        public string AssignedLocationName { get; set; }
        public string LocationAssignment { get; set; }
    }
}