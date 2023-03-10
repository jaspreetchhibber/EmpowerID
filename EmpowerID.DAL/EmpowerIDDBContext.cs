using EmpowerID.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpowerID.DAL
{
    public class EmpowerIDDBContext : DbContext
    {
        public EmpowerIDDBContext(DbContextOptions<EmpowerIDDBContext> options) : base(options)
        {
        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedDepartments(builder);
        }

        private void SeedDepartments(ModelBuilder builder)
        {
            builder.Entity<Department>().HasData(
                new Department()
                {
                    Id = 1,
                    DepartmentName = "Accounts"
                },
                new Department()
                {
                    Id = 2,
                    DepartmentName = "Sales"
                },
                new Department()
                {
                    Id = 3,
                    DepartmentName = "IT"
                }
                );
        }
    }
}
