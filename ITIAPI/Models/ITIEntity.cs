using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITIAPI.Models
{
    public class ITIEntity :IdentityDbContext<ApplicationUser>
    {
        public ITIEntity(DbContextOptions<ITIEntity> options)
          : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

    }
}
