using api.Data;
using api.Models;

namespace api.Tests.TestUtilities
{
    public static class TestDatabaseInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Departments.AddRange(
                new Department { Name = "TI" },
                new Department { Name = "RH" }
            );

            context.SaveChanges();
        }
    }
}
