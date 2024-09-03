using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Departamento é obrigatório.")]
        public required string Name { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
