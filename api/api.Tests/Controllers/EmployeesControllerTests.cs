using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Controllers; 
using api.Models; 
using api.DTOs;
using api.Data;

namespace api.Tests.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly EmployeesController _controller;
        private readonly ApplicationDbContext _context;

        public EmployeesControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new EmployeesController(_context);

            _context.Employees.AddRange(
                new Employee 
                { 
                    Id = 1, 
                    Name = "Maycon Vieira", 
                    Email = "maycon@gmail.com", 
                    Position = "Desenvolvedor", 
                    Department = new Department { Name = "TI" } 
                },
                new Employee 
                { Id = 2, 
                    Name = "Yasmin Santos", 
                    Email = "yasmin@gmail.com", 
                    Position = "Recursos Humanos", 
                    Department = new Department { Name = "RH" } 
                }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetEmployees_ReturnsOkResult_WithListOfEmployees()
        {
            var result = await _controller.GetEmployees() as ActionResult<IEnumerable<EmployeeWithDepartmentDto>>;
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var employeeDtos = Assert.IsAssignableFrom<IEnumerable<EmployeeWithDepartmentDto>>(okResult.Value);

            Assert.Equal(2, employeeDtos.Count());
        }

        [Fact]
        public async Task GetEmployee_ReturnsOkResult_WithEmployee()
        {
            var result = await _controller.GetEmployee(1) as ActionResult<EmployeeWithDepartmentDto>;
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var employeeDto = Assert.IsType<EmployeeWithDepartmentDto>(okResult.Value);

            Assert.Equal("Maycon Vieira", employeeDto.Name);
            Assert.Equal("TI", employeeDto.Department.Name);
        }
    }
}
