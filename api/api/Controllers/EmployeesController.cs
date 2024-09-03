using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.DTOs;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeWithDepartmentDto>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Department) // Inclui o departamento
                .ToListAsync();

            var employeeDtos = employees.Select(e => new EmployeeWithDepartmentDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Position = e.Position,
                Department = new DepartmentDto
                {
                    Id = e.Department.Id,
                    Name = e.Department.Name
                }
            });

            return Ok(employeeDtos);
        }

        // GET: api/Employees/id
        public async Task<ActionResult<EmployeeWithDepartmentDto>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department) // Inclui o departamento
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeWithDepartmentDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Position = employee.Position,
                Department = new DepartmentDto
                {
                    Id = employee.Department.Id,
                    Name = employee.Department.Name
                }
            };

            return Ok(employeeDto);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Position = employeeDto.Position,
                DepartmentId = employeeDto.DepartmentId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var resultDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Position = employee.Position,
                DepartmentId = employee.DepartmentId
            };

            var response = new
            {
                Message = "Empregado criado com sucesso.",
                Employee = resultDto
            };

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, response);
        }

        // PUT: api/Employees/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            if (!_context.Departments.Any(d => d.Id == employeeDto.DepartmentId))
            {
                ModelState.AddModelError("DepartmentId", "Id inválido.");
                return BadRequest(ModelState);
            }

            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Position = employeeDto.Position;
            employee.DepartmentId = employeeDto.DepartmentId;

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Empregado alterado com sucesso." });
        }

        // DELETE: api/Employees/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(new { message = "Empregado não encontrado." });
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Empregado excluído com sucesso." });
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

    }
}
