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
    }
}
