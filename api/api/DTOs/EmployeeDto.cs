using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A posição é obrigatória.")]
        public required string Position { get; set; }

        public int DepartmentId { get; set; } 
    }
}
