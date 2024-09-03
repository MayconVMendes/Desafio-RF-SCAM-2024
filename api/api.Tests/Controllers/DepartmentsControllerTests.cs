using api.Controllers;
using api.Data;
using api.Models;
using api.Tests.TestUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Tests.Controllers
{
    public class DepartmentsControllerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly DepartmentsController _controller;

        public DepartmentsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            TestDatabaseInitializer.Initialize(_context);
            _controller = new DepartmentsController(_context);
        }

        [Fact]
        public async Task GetDepartments_ReturnsOkResult_WithListOfDepartments()
        {
            var result = await _controller.GetDepartments() as ActionResult<IEnumerable<Department>>;

            var okResult = Assert.IsType<ActionResult<IEnumerable<Department>>>(result);
            var departments = Assert.IsAssignableFrom<IEnumerable<Department>>(okResult.Value);
            Assert.NotEmpty(departments);
        }

        [Fact]
        public async Task GetDepartment_ReturnsOkResult_WithDepartment()
        {
            var department = new Department
            {
                Name = "TI"
            };
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            var result = await _controller.GetDepartment(department.Id) as ActionResult<Department>;

            var okResult = Assert.IsType<ActionResult<Department>>(result);
            var returnedDepartment = Assert.IsType<Department>(okResult.Value);
            Assert.Equal(department.Name, returnedDepartment.Name);
        }
    }
}
