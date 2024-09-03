namespace api.DTOs
{
public class EmployeeWithDepartmentDto
{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
