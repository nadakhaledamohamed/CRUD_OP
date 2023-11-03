using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_OP.Models
{
    public class Employee
    {
        [Key]
        [Display(Name = "ID")]
        public int? EmployeeId { get; set; }
        public string Name { get; set; } = "";

        public string PhoneNumber { get; set; } = "";
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int pageSize { get; set; }
        public string? Term { get; set; }
        public string? OrderBy { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}
