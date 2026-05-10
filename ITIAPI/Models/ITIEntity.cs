using Microsoft.EntityFrameworkCore;

namespace ITIAPI.Models
{
    public class ITIEntity : DbContext
    {
        public ITIEntity(DbContextOptions<ITIEntity> options)
          : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

    }
}
