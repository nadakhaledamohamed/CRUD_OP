namespace CRUD_OP.Models

{
    public class EmployeeViewModel
    {

        public List<Employee>? Employees { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int pageSize { get; set; }
        public string? Term { get; set; }
        public string? OrderBy { get; set; }
    }
}
