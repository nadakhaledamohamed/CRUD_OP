using CRUD_OP.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_OP.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
                
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(e => new { e.Name, e.PhoneNumber }).IsUnique();

            //configures an index on the Employee entity, specifically on the Name and PhoneNumber columns.
            //Indexes in databases help to speed up retrieval times when you query based on those columns.
            //By specifying two properties (Name and PhoneNumber) inside a new anonymous object
        }
    }
}
